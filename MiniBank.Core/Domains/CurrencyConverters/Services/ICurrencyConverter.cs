namespace MiniBank.Core.Domains.CurrencyConverters.Services
{
    public interface ICurrencyConverter
    {
        public decimal Convert(decimal amount,string fromCurrency,string toCurrency);
    }
}