using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace ShadiWebApplication.Helpers
{
    public static class DateTimeExtensions
    {
        public static string ToPersianCalndar(this DateTime dateTime)
        {
            var persianCal = new PersianCalendar();
            var strDateTime = persianCal.GetYear(dateTime) + "/" + persianCal.GetMonth(dateTime) + "/" +
                                      persianCal.GetDayOfMonth(dateTime) + " - "
                                      + dateTime.ToString("HH:mm");
            return strDateTime;
        }
        public static string ToPersianCalndar(this DateTime? dateTime)
        {
            var persianCal = new PersianCalendar();
            return !dateTime.HasValue
                ? string.Empty
                : ToPersianCalndar(dateTime.Value);
        }
    }
}
