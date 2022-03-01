namespace MiniBank.Core.Interfaces
{
    public interface ICurrencyCode
    {
        public int GetExchangeRate(string currencyCode);
    }
}