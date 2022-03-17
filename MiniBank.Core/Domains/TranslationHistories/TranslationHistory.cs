namespace MiniBank.Core.Domains.TranslationHistories
{
    public class TranslationHistory
    {
        public string Id { get; set; }
        public decimal Sum { get; set; }
        public string Currency { get; set; }
        public string FromAccountId { get; set; }
        public string ToAccountId { get; set; }
    }
}