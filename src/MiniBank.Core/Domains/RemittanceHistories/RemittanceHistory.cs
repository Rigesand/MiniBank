using System;

namespace MiniBank.Core.Domains.RemittanceHistories
{
    public class RemittanceHistory
    {
        public Guid Id { get; set; }
        public decimal Sum { get; set; }
        public string Currency { get; set; }
        public Guid FromAccountId { get; set; }
        public Guid ToAccountId { get; set; }
    }
}