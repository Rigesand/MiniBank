namespace MiniBank.Core.Interfaces
{
    public interface ICurrencyConverter
    {
        public decimal Convert(decimal sum, string currencyCode);
    }
}