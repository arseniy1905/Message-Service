using MessageService.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageService.ViewModel
{
    public class MessageResponseViewModel
    {
        public string CanSend { get; set; }
        public string MessageContent { get; set; }
        public string PhoneNumberFrom { get; set; }
        public string PhoneNumberTo { get; set; }
        
    }
}
