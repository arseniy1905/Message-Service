using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageService.Common.Configuration
{
    public class ConfigKey
    {
        protected ConfigKey() 
        { 
        }
        private static readonly ConfigKey _sendTime = new ConfigKey() { Key = nameof(SEND_TIME) };
        private static readonly ConfigKey _messageTimeLimit = new ConfigKey() { Key = nameof(MESSAGE_TIME_LIMIT) };
        private static readonly ConfigKey _messagesLifeSpan = new ConfigKey() { Key = nameof(MESSAGES_LIFE_SPAN) };
        private static readonly ConfigKey _messageCount = new ConfigKey() { Key = nameof(MESSAGE_COUNT) };
        private static readonly ConfigKey _messageLifeSpanStart = new ConfigKey() { Key = nameof(MESSAGE_LIFE_SPAN_START) };
        public static ConfigKey SEND_TIME => _sendTime;
        public static ConfigKey MESSAGE_TIME_LIMIT => _messageTimeLimit;
        public static ConfigKey MESSAGES_LIFE_SPAN => _messagesLifeSpan;
        public static ConfigKey MESSAGE_COUNT => _messageCount;
        public static ConfigKey MESSAGE_LIFE_SPAN_START => _messageLifeSpanStart;
        public string Key { get; protected set; }
    }
}
