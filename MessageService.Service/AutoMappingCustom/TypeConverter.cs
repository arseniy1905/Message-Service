using AutoMapper;
using System;

namespace MessageService.Service.AutoMappingCustom
{
    public class TypeConverter
    {
        public class TimeSpanTypeConverter : ITypeConverter<TimeSpan, string>
        {
            public string Convert(TimeSpan source, string destination, ResolutionContext context)
            {
                return source.ToString(@"hh\:mm");
            }
        }
    }
}
