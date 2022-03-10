using System;
using System.Threading;
using MiniBank.Core.Interfaces;

namespace MiniBank.Data.Services
{
    public class CurrencyCodeService: ICurrencyCode
    {
        private static int _seed = Environment.TickCount;
     
        private static ThreadLocal<Random> _random = new (() =>
            new Random(Interlocked.Increment(ref _seed))
        );
        public decimal GetExchangeRate(string currencyCode)
        {
            return _random.Value.Next(1,200);
        }
    }
}