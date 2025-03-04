using AutoMapper;
using MessageService.Common.Enums;
using MessageService.IRepository.Unit;
using MessageService.IService;
using MessageService.ViewModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Text.RegularExpressions;
using AutoMapper.Extensions.ExpressionMapping;
using MessageService.Common.Extensions;
using MessageService.DataModel;
using MessageService.Common.Constants;
using MessageService.Service.Common;
using MessageService.Common.Configuration;
using ClosedXML;

namespace MessageService.Service
{
    public class MessageService : AbstractDataService<IMainUnit>, IMessageService
    {
        public MessageService(IConfiguration configuration, IMainUnit mainUnit) : base(mainUnit)
        {
        }

        protected override Action<IMapperConfigurationExpression> MapperConfig => cfg =>
        {
            cfg.AddExpressionMapping();
            cfg.CreateMap<MessageRequestViewModel, MessageResponseViewModel>().AfterMap(fromMessageRequest2MessageResponse);
            
        };

        private void fromMessageRequest2MessageResponse(MessageRequestViewModel messageRequestViewModel, MessageResponseViewModel messageResponseViewModel)
        {
            messageResponseViewModel.CanSend=MessageConstants.SEND_RESULT_MESSAGES[SendResultEnum.Success];
        }

        public async Task<MessageResponseViewModel> CanSend(MessageRequestViewModel messageRequestViewModel)
        {
            var result = messageRequestViewModel.MapTo<MessageResponseViewModel>(_mapper);
            if (string.IsNullOrEmpty(messageRequestViewModel.MessageContent))
            {
                result.CanSend = MessageConstants.SEND_RESULT_MESSAGES[SendResultEnum.InvalidMessageContent];
                return result;
            }
            
            if (string.IsNullOrEmpty(messageRequestViewModel.PhoneNumberFrom))
            {
                result.CanSend = MessageConstants.SEND_RESULT_MESSAGES[SendResultEnum.InvalidPhoneFrom];
                return result;
            }
            if (string.IsNullOrEmpty(messageRequestViewModel.PhoneNumberTo))
            {
                result.CanSend = MessageConstants.SEND_RESULT_MESSAGES[SendResultEnum.InvalidPhoneTo];
                return result;
            }
            
            // Find all matches
            var matches = Regex.Matches(result.PhoneNumberFrom, MessageConstants.DIGIT_PATTERN);

            // Concatenate all digits into one string
            string digitsOnly = string.Join(string.Empty, matches);

            long phoneNumberFrom = 0;
            if (!long.TryParse(digitsOnly, out phoneNumberFrom))
            {
                result.CanSend = MessageConstants.SEND_RESULT_MESSAGES[SendResultEnum.InvalidPhoneFrom];
                return result;
            }
            
            matches = Regex.Matches(result.PhoneNumberTo, MessageConstants.DIGIT_PATTERN);

            // Concatenate all digits into one string
            digitsOnly = string.Join(string.Empty, matches);

            long phoneNumberTo = 0;
            if (!long.TryParse(digitsOnly, out phoneNumberTo))
            {
                result.CanSend = MessageConstants.SEND_RESULT_MESSAGES[SendResultEnum.InvalidPhoneTo];
                return result;
            }
            


            var sendTimeSettings = await _unit.Settings.AsNoTracking().FirstOrDefaultAsync(settings => settings.KeyName == ConfigKey.SEND_TIME.Key);
            long sendTime = 0;
            var timeNow = DateTime.Now.Ticks;
            if (sendTimeSettings == null)
            {
                sendTime = timeNow;
               
            }
            else
            {
                sendTime = long.Parse(sendTimeSettings.Value);
            }
            
            var sendLimitSettigs= await _unit.Settings.FirstOrDefaultAsync(setting=>setting.KeyName==ConfigKey.MESSAGE_TIME_LIMIT.Key);

            var sendLimit = long.Parse(sendLimitSettigs.Value);

            var actualMessage =await _unit.Messages.AsNoTracking().SingleOrDefaultAsync(m => m.PhoneNumber == phoneNumberFrom);

            var messageCountPerPhone = 0;
            var totalMessageCountSettings = await _unit.Settings.AsNoTracking().SingleAsync(setting => setting.KeyName == ConfigKey.MESSAGE_COUNT.Key);
            var totalMessageCount = long.Parse(totalMessageCountSettings.Value);
            
            if (timeNow - sendTime <= sendLimit)
            {
                if (actualMessage != null)
                {
                    messageCountPerPhone = actualMessage.MessageCount;
                }
                if (messageCountPerPhone > messageRequestViewModel.PhoneLimit)
                {
                    if (actualMessage != null)
                    {
                        actualMessage.MessageCount = 0;
                        _unit.Messages.UpdateEntity(actualMessage);
                    }
                    result.CanSend = MessageConstants.SEND_RESULT_MESSAGES[SendResultEnum.OutOfPhoneLimit];
                    return result;
                }
                if (totalMessageCount > messageRequestViewModel.TotalLimit)
                {
                    totalMessageCountSettings.Value=0.ToString();
                    _unit.Settings.UpdateEntity(totalMessageCountSettings);
                    result.CanSend = MessageConstants.SEND_RESULT_MESSAGES[SendResultEnum.OutOfTotalLimit];
                    return result;
                }
                
                      
            }
            else
            {
                
                if(sendTimeSettings != null)
                {
                    sendTimeSettings.Value = timeNow.ToString();
                    _unit.Settings.UpdateEntity(sendTimeSettings);
                }
                if (actualMessage != null) 
                {
                    actualMessage.MessageCount++;
                    _unit.Messages.UpdateEntity(actualMessage);
                }
                totalMessageCount++;
                totalMessageCountSettings.Value = totalMessageCount.ToString();
                _unit.Settings.UpdateEntity(totalMessageCountSettings);
            }
            if (sendTimeSettings == null)
            {
                sendTime = timeNow;
                //TODO-REPLACE WITH ASYNC
                _unit.Settings.AddEntity(new Setting() { KeyName = ConfigKey.SEND_TIME.Key, Value = sendTime.ToString() });

            }
            if (actualMessage==null) 
            { 
                actualMessage=new Message() 
                { 
                    MessageCount = messageCountPerPhone+1,
                    PhoneNumber=phoneNumberFrom
                };
                _unit.Messages.AddEntity(actualMessage);
            }
            
            var messageLifeSpanSetting = await _unit.Settings.SingleOrDefaultAsync(setting => setting.KeyName == ConfigKey.MESSAGES_LIFE_SPAN.Key);
            var messageLifeSpan = long.Parse(messageLifeSpanSetting.Value);
            var messageLifeStartSetting = await _unit.Settings.AsNoTracking().SingleOrDefaultAsync(setting => setting.KeyName == ConfigKey.MESSAGE_LIFE_SPAN_START.Key);
            if (messageLifeStartSetting == null) 
            {
                messageLifeStartSetting = new Setting()
                {
                    KeyName = ConfigKey.MESSAGE_LIFE_SPAN_START.Key,
                    Value = timeNow.ToString()
                };
                _unit.Settings.AddEntity(messageLifeStartSetting);
            }
            long messageLifeStart = long.Parse(messageLifeStartSetting.Value);

            if (messageLifeStartSetting != null)
            {
                messageLifeStart = long.Parse(messageLifeStartSetting.Value);
                if (timeNow - messageLifeStart > messageLifeSpan)
                {
                    messageLifeStartSetting.Value = timeNow.ToString();
                    _unit.Settings.UpdateEntity(messageLifeStartSetting);
                    _unit.Messages.RemoveRange();
                }
            }
            
            await _unit.SaveChangesAsync();
            return result;
        }
    }
}
