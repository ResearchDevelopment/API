using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace ShadiWebApplication.Helpers
{
    public static class CalendarHelper
    {
        public static NameValue ToShamsi(this DateTime dateTime)
        {
            try
            {

                PersianCalendar pc = new PersianCalendar();
                int pcYear = pc.GetYear(dateTime);
                int pcMonth = pc.GetMonth(dateTime);

                int pcDay = pc.GetDayOfMonth(dateTime);
                string convertedDate = string.Format("{0}/{1}/{2}", pcYear, pcMonth < 10 ? "0" + pcMonth.ToString() : pcMonth.ToString(), pcDay < 10 ? "0" + pcDay.ToString() : pcDay.ToString());

                return new NameValue(GetPersianDate(pc.GetDayOfWeek(dateTime)), convertedDate);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DateTime ToGregorian(this string strPersianDate)
        {
            try
            {
                var gregorianDate = DateTime.MinValue;
                var dateParts = strPersianDate?.Trim().Split('/');
                var pc = new PersianCalendar() { };
                if (dateParts.Length == 3)
                {
                    gregorianDate = pc.ToDateTime
                        (Convert.ToInt16(dateParts[0]?.Trim()), 
                        Convert.ToInt16(dateParts[1]?.Trim()),
                       Convert.ToInt16(dateParts[2]?.Trim()), 0, 0, 0, 0);
                }
                return gregorianDate;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private static string GetPersianDate(DayOfWeek dayOfWeek)
        {
            switch (dayOfWeek)
            {
                case DayOfWeek.Friday:
                    return "جمعه";

                case DayOfWeek.Saturday:
                    return "شنبه";

                case DayOfWeek.Sunday:
                    return "یکشنبه";

                case DayOfWeek.Monday:
                    return "دوشنبه";

                case DayOfWeek.Tuesday:
                    return "سه شنبه";

                case DayOfWeek.Wednesday:
                    return "چهارشنبه";
                case DayOfWeek.Thursday:
                    return "پنجشنبه";

            }

            return "";
        }

        public static string GetTwoDatesIntervals(DateTime fromDate, DateTime toDate)
        {
            try
            {
                NameValue from = ToShamsi(fromDate);
                NameValue to = ToShamsi(toDate);
                return string.Format("از {0} {1} تا {2} {3}", from.Name, from.Value, to.Name, to.Value);
            }
            catch (Exception ex)
            {
                return "";
            }


        }


    }

    public class NameValue
    {
        private string _name;
        private string _value;

        public NameValue(string name, string value)
        {
            _name = name;
            _value = value;
        }

        public NameValue(string name, int value)
        {
            _name = name;
            _value = value.ToString();
        }

        public NameValue(int name, int value)
        {
            _name = name.ToString();
            _value = value.ToString();
        }
        public NameValue(int name, string value)
        {
            _name = name.ToString();
            _value = value;
        }
        public NameValue(int name, int? value)
        {
            _name = name.ToString();
            _value = value.ToString();
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public string Value
        {
            get { return _value; }
            set { _value = value; }
        }
    }
}
