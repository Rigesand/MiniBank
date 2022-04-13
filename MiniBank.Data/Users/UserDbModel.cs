using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiniBank.Data.Users
{
    public class UserDbModel
    {
        [Column("id")]
        public Guid Id { get; set; }
        [Column("login")]
        public string Login { get; set; }
        [Column("email")]
        public string Email { get; set; }
    }
}