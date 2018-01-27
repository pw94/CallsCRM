using System;

namespace CallsCRM.Strategy
{
    public class CallStrategy : ICallStrategy
    {
        public DateTime Call()
        {
            const int TOTAL_PERCENTAGES = 100, MINIMUM_PERCENTAGE = 20;
            var rnd = new Random().Next(TOTAL_PERCENTAGES);
            return rnd < MINIMUM_PERCENTAGE ? DateTime.Now : DateTime.Now.AddSeconds(rnd * 0.6);
        }
    }
}