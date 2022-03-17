namespace MiniBank.Web.Controllers.Accounts.Dto
{
    public class CreateAccountDto
    {
        public string UserId { get; set; }
        public string Currency { get; set; }
        public decimal Sum { get; set; }
    }
}