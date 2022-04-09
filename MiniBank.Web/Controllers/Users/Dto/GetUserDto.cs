using System;

namespace MiniBank.Web.Controllers.Users.Dto
{
    public class GetUserDto
    {
        public Guid Id { get; set; }
        
        public string Login { get; set; }
        
        public string Email { get; set; }
    }
}