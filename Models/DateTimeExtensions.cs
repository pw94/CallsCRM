using System;

namespace CallsCRM.Models
{
    public static class DateTimeExtensions
    {
        public static DateTime TrimMilliseconds(this DateTime dt)
        {
            return new DateTime(dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, dt.Second, 0);
        }

        public static bool IsWeekend(this DateTime dt)
        {
            return dt.DayOfWeek == DayOfWeek.Saturday
                || dt.DayOfWeek == DayOfWeek.Sunday;
        }

        public static bool IsMorning(this DateTime dt)
        {
            return dt.Hour < 15;
        }

        public static DateTime AddDaysAfterNow(this DateTime dt)
        {
            var diff = Math.Floor((DateTime.Now - dt).TotalDays);
            var date = dt.AddDays(diff+1);
            if (dt.IsWeekend())
                date = date.AddDaysToSaturday();
            else
                date = date.AddDays(2);
            
            return date;
        }

        public static DateTime AddDaysToSaturday(this DateTime dt)
        {
            var diff = DayOfWeek.Saturday - dt.DayOfWeek;
            return dt.AddDays(diff);
        }
    }
}