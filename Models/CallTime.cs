using System;
using System.ComponentModel.DataAnnotations;

namespace CallsCRM.Models
{
    public class CallTime
    {
        public CallTime()
        {
            this.StartTime = DateTime.Now;
            this.EndTime = DateTime.Now;
        }
        
        public CallTime(DateTime StartTime, DateTime EndTime)
        {
            this.StartTime = StartTime;
            this.EndTime = EndTime;
        }

        public DateTime StartTime { get; protected set; }

        public DateTime EndTime { get; protected set; }
        
        [DisplayFormat(DataFormatString = "{0:F2}")]
        public double Duration => EndTime.Subtract(StartTime).TotalSeconds;
        
        public bool Success => StartTime.TrimMilliseconds() != EndTime.TrimMilliseconds();
        
        public override bool Equals(object obj)
        {
            var callTime = obj as CallTime;

            if (callTime == null)
            {
                return false;
            }

            return StartTime.Equals(callTime.StartTime) && EndTime.Equals(callTime.EndTime);
        }
        
        public override int GetHashCode()
        {
            return StartTime.GetHashCode() ^ EndTime.GetHashCode();
        }
    }
}