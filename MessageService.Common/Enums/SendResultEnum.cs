using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageService.Common.Enums
{
    public enum SendResultEnum
    {
        Success=0,
        OutOfTotalLimit=1,
        OutOfPhoneLimit=2,
        InvalidPhoneFrom=3, 
        InvalidPhoneTo=4,
        InvalidMessageContent=5

    }
}
