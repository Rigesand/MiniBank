namespace MiniBank.Data.TranslationHistories
{
    public class TranslationHistoryDbModel
    {
        public string Id { get; set; }
        public decimal Sum { get; set; }
        public string Currency { get; set; }
        public string FromAccountId { get; set; }
        public string ToAccountId { get; set; }
    }
}