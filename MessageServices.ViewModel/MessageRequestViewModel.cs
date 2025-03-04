using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageService.ViewModel
{
    public class MessageRequestViewModel
    {
        public string MessageContent { get; set; }
        public string PhoneNumberFrom { get; set; }
        public string PhoneNumberTo { get; set; }
        public int PhoneLimit { get; set; }
        public int TotalLimit { get; set; }
    }
}
