using System;

namespace MiniBank.Web.Controllers.Accounts.Dto
{
    public class GetAccountDto
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