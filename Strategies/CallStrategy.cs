using System;

namespace CallsCRM.Strategy
{
    public class CallStrategy : ICallStrategy
    {
        public DateTime Call()
        {
            var rnd = new Random().Next(100);
            return rnd < 20 ? DateTime.Now : DateTime.Now.AddSeconds(rnd * 0.6);
        }
    }
}