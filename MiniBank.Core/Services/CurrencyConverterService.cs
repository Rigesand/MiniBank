using System;
using MiniBank.Core.Exception;
using MiniBank.Core.Interfaces;

namespace MiniBank.Core.Services
{
    public class CurrencyConverterService: ICurrencyConverter
    {
        private readonly ICurrencyCode _currencyCode;

        public CurrencyConverterService(ICurrencyCode currencyCode)
        {
            _currencyCode = currencyCode;
        }
        public decimal Convert(decimal sum, string currencyCode)
        {
            if (sum < 0)
                throw new UserFriendlyException();
            if (sum == 0)
                return 0;
            var exchangeRate = _currencyCode.GetExchangeRate(currencyCode);
            if (exchangeRate<0)
                throw new UserFriendlyException();
            return decimal.Round(sum / exchangeRate,5,MidpointRounding.ToEven);
        }
    }
}