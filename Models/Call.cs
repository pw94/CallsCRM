using System;

namespace CallsCRM.Models
{
    public class Call
    {
        public int CallId { get; set; }
        public CallTime Time { get; set; }
        public int CustomerId { get; set; }
        public Customer Callee { get; set; }
        public int CallerId { get; set; }
        public Caller Caller { get; set; }

        public DateTime PredictNext()
        {
            var time = Time.StartTime.AddDaysAfterNow();
            if (time.IsWeekend())
                time = time.AddDays(2);
            else if (time.IsMorning())
                time = time.AddHours(7);
            else
                time = time.AddHours(-7).AddDaysToSaturday();

            return time;
        }
    }
}