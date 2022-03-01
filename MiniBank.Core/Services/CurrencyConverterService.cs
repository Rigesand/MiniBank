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
        public int Convert(int sum, string сurrencyСode)
        {
            if (sum < 0)
                throw new System.Exception();
            var exchangeRate = _currencyCode.GetExchangeRate(сurrencyСode);
            if (exchangeRate<0)
                throw new UserFriendlyException();
            return sum / exchangeRate;
        }
    }
}