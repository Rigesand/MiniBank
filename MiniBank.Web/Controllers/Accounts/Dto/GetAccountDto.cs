using System;

namespace MiniBank.Web.Controllers.Accounts.Dto
{
    public class GetAccountDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public decimal Sum { get; set; }
        public string Currency { get; set; }
        public bool IsActive { get; set; }
        public DateTime OpeningDate { get; set; }
        public DateTime ClosingDate { get; set; }
    }
}