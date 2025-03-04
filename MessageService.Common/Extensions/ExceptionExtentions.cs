using System;
using System.Collections.Generic;
using System.Text;

namespace MessageService.Common.Extensions
{
    public static class ExceptionExtentions
    {
        public static string BuildErrorMessage(this Exception ex,StringBuilder builder=null)
        {
            
            if (builder == null)
            {
                builder = new StringBuilder();
            }
            builder.AppendFormat("Ex:{0}   Src:{1}   ST:{2}\r\n", ex.Message, ex.Source, ex.StackTrace);
            if (ex.InnerException != null)
            {
                builder.Append("Inner Exception:\r\n");
                builder.Append(BuildErrorMessage(ex.InnerException, builder));
            }
            return builder.ToString();
        }
    }
}
