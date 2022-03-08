namespace MiniBank.Core.Interfaces
{
    public interface ICurrencyCode
    {
        public decimal GetExchangeRate(string currencyCode);
    }
}