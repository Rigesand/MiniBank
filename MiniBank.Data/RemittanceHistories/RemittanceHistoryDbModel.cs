using System;

namespace MiniBank.Data.RemittanceHistories
{
    public class RemittanceHistoryDbModel
    {
        public Guid Id { get; set; }
        public decimal Sum { get; set; }
        public string Currency { get; set; }
        public Guid FromAccountId { get; set; }
        public Guid ToAccountId { get; set; }
    }
}