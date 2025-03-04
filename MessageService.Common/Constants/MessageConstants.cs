using MessageService.Common.Enums;

namespace MessageService.Common.Constants
{
    public class MessageConstants
    {
        public const string DIGIT_PATTERN = @"\d+";
        public static Dictionary<SendResultEnum, string> SEND_RESULT_MESSAGES= new Dictionary<SendResultEnum, string>() 
        {
            {SendResultEnum.Success,"Message can be sent" },
            {SendResultEnum.InvalidMessageContent,"Invalid message content" },
            {SendResultEnum.InvalidPhoneTo,"Message cannot be sent. The receiver phone number is invalid" },
            {SendResultEnum.InvalidPhoneFrom,"Message cannot be sent. The sender phone number is invalid" },
            {SendResultEnum.OutOfTotalLimit,"Number of messages exceeded the total limit" },
            {SendResultEnum.OutOfPhoneLimit,"Number of messages exceeded the phone limit" }

        };
    }
}
