using System;
using MiniBank.Core.Interfaces;

namespace MiniBank.Data.Services
{
    public class CurrencyCodeService: ICurrencyCode
    {
        public int GetExchangeRate(string currencyCode)
        {
            return new Random().Next(-100,100);
        }
    }
}