using System;
using System.Globalization;

namespace MessageService.Common.Extensions
{
    public static class DateTimeExtensions
    {
        public static string ToDisplayFormatString(this DateTime? dt)
        {
            if (dt == null)
            {
                return string.Empty;
            }

            return dt.Value.ToString("yyyy-MM-dd HH:mm:ss");
        }
        public static string ToDisplayFormatString(this DateTime dt)
        {
            return dt.ToString("yyyy-MM-dd HH:mm:ss");
        }
        public static string ToDisplayDateTimeString(this DateTime dt)
        {
            return dt.ToString("yyyy-MM-dd hh:mm tt");
        }
        public static string ToDisplayDate(this DateTime dt)
        {
            return dt.ToString("yyyy-MM-dd");
        }
        public static string ToDisplayDateFormat(this DateTime dt)
        {
            return dt.ToString("MM/dd/yyyy");
        }
        public static string ToDisplayTimeWithoutSeconds(this DateTime dt)
        {
            return dt.ToString("hh:mm tt");
        }
        public static string ToDisplayTimeWithoutSeconds(this TimeSpan time)
        {
            return (DateTime.Now.Date + time).ToString("hh:mm tt");
        }
        public static DateTime NextDate(this DayOfWeek day)
        {
            DateTime today = DateTime.Today;
            // The (... + 7) % 7 ensures we end up with a value in the range [0, 6]
            int daysUntilTuesday = ((int)day - (int)today.DayOfWeek + 7) % 7;
            DateTime nextTuesday = today.AddDays(daysUntilTuesday);
            return nextTuesday;
        }
        public static int GetIso8601WeekOfYear(this DateTime time)
        {
            // Seriously cheat.  If its Monday, Tuesday or Wednesday, then it'll 
            // be the same week# as whatever Thursday, Friday or Saturday are,
            // and we always get those right
            DayOfWeek day = CultureInfo.InvariantCulture.Calendar.GetDayOfWeek(time);
            if (day >= DayOfWeek.Monday && day <= DayOfWeek.Wednesday)
            {
                time = time.AddDays(3);
            }

            // Return the week of our adjusted day
            return CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(time, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
        }
        public static DateTime FromExcelSerialDate(this int serialDate)
        {
            if (serialDate > 59) serialDate -= 1; //Excel/Lotus 2/29/1900 bug   
            return new DateTime(1899, 12, 31).AddDays(serialDate);
        }

        public static DateTime? ToNullableDate(this String dateString)
        {
            if (String.IsNullOrEmpty((dateString ?? "").Trim()))
                return null;

            DateTime resultDate;
            if (DateTime.TryParse(dateString, out resultDate))
                return resultDate;

            return null;
        }

        public static bool IsWithinCurrentWeek(this DateTime dateTime)
        {
            var firstWeekDay = CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek;
            var startDateOfWeek = DateTime.Now.Date;
            while (startDateOfWeek.DayOfWeek != firstWeekDay)
            {
                startDateOfWeek = startDateOfWeek.AddDays(-1d);
            }
            DateTime endDateOfWeek = startDateOfWeek.AddDays(7d);
            return dateTime >= startDateOfWeek && dateTime <= endDateOfWeek;
        }
    }
}
