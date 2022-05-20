using System;
using MiniBank.Core.Domains.CurrencyConverters.Repositories;
using MiniBank.Core.Exception;

namespace MiniBank.Core.Domains.CurrencyConverters.Services
{
    public class CurrencyConverterService: ICurrencyConverter
    {
        private readonly ICurrencyRepository _currencyRepository;
        private const string Rub = "RUB";

        public CurrencyConverterService(ICurrencyRepository currencyRepository)
        {
            _currencyRepository = currencyRepository;
        }
        public decimal Convert(decimal amount,string fromCurrency,string toCurrency)
        {
            if (amount < 0)
                throw new ValidationException("Сумма не может быть отрицательной");
            if (amount == 0)
                return 0;
            if (string.IsNullOrEmpty(fromCurrency)||string.IsNullOrEmpty(toCurrency))
                throw new ValidationException("Валюта не может быть пустой или null");
            if (string.Equals(fromCurrency,toCurrency))
                return amount;

            decimal fromCurrencyCourse = 0;
            decimal toCurrencyCourse = 0;
            
            if (fromCurrency == Rub)
            {
                toCurrencyCourse = _currencyRepository.GetExchangeRate(toCurrency);
                return decimal.Round(amount / toCurrencyCourse,2,MidpointRounding.ToEven);
            }

            if (toCurrency==Rub)
            {
                fromCurrencyCourse = _currencyRepository.GetExchangeRate(fromCurrency);
                return decimal.Round(amount*fromCurrencyCourse,2,MidpointRounding.ToEven);
            }
            
            toCurrencyCourse = _currencyRepository.GetExchangeRate(toCurrency);

            fromCurrencyCourse = _currencyRepository.GetExchangeRate(fromCurrency);

            return decimal.Round(amount*fromCurrencyCourse/toCurrencyCourse,2,MidpointRounding.ToEven);
        }
    }
}