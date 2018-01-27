using System;

namespace CallsCRM.Models
{
    public class Call
    {
        public int CallId { get; set; }
        public CallTime Time { get; set; } = new CallTime();
        public int CustomerId { get; set; }
        public Customer Callee { get; set; }
        public int CallerId { get; set; }
        public Caller Caller { get; set; }

        public DateTime NextCall
        {
            get
            {
                const short DAYS_IN_WEEKEND = 2, DAYS_IN_WEEK = 7;
                var time = Time.StartTime.AddDaysAfterNow();
                if (time.IsWeekend())
                    time = time.AddDays(DAYS_IN_WEEKEND);
                else if (time.IsMorning())
                    time = time.AddHours(DAYS_IN_WEEK);
                else
                    time = time.AddHours(-DAYS_IN_WEEK).AddDaysToSaturday();

                return time;
            }
        }
    }
}