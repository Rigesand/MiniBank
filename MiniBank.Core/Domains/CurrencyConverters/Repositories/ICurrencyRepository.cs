namespace MiniBank.Core.Domains.CurrencyConverters.Repositories
{
    public interface ICurrencyRepository
    {
        public decimal GetExchangeRate(string currencyCode);
    }
}