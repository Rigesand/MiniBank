using System;

namespace MiniBank.Core.Domains.BankAccounts
{
    public class Account
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public decimal Sum { get; set; }
        public string Currency { get; set; }
        public bool IsActive { get; set; }
        public DateTime OpeningDate { get; set; }
        public DateTime ClosingDate { get; set; }
    }
}