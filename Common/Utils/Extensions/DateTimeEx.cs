using System;

namespace Utils.Extensions
{
    public static class DateTimeEx
    {
        public static DateTime LastDays(this DateTime dateTime, int days)
        {
            if (days < 1)
                return dateTime;

            days = Math.Min((dateTime - DateTime.MinValue).Days, days);

            return dateTime - TimeSpan.FromDays(days);
        }

        public static DateTime NextDays(this DateTime dateTime, int days)
        {
            if (days < 1)
                return dateTime;

            days = Math.Min((DateTime.MaxValue - dateTime).Days, days);

            return dateTime + TimeSpan.FromDays(days);
        }

        public static DateTime LastMonth(this DateTime dateTime, DateTime minValue)
        {
            var year = dateTime.Year;
            var month = dateTime.Month;
            var day = dateTime.Day;

            if (year == minValue.Year && month == minValue.Month)
                day = minValue.Day;
            else
            {
                month -= 1;
                if (month < 1)
                {
                    month = 12;
                    year--;
                }
            }

            day = Math.Min(day, DateTime.DaysInMonth(year, month));

            return new DateTime(year, month, day, dateTime.Hour, dateTime.Minute, dateTime.Second, dateTime.Millisecond);
        }

        public static DateTime NextMonth(this DateTime dateTime, DateTime maxValue)
        {
            var year = dateTime.Year;
            var month = dateTime.Month;
            var day = dateTime.Day;

            if (year == maxValue.Year && month == maxValue.Month)
                day = maxValue.Day;
            else
            {
                month += 1;
                if (month > 12)
                {
                    month = 1;
                    year++;
                }
            }

            day = Math.Min(day, DateTime.DaysInMonth(year, month));

            return new DateTime(year, month, day, dateTime.Hour, dateTime.Minute, dateTime.Second, dateTime.Millisecond);
        }
    }
}
