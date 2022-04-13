using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiniBank.Data.Accounts
{
    public class AccountDbModel
    {
        [Column("id")]
        public Guid Id { get; set; }
        
        [Column("user_id")]
        public Guid UserId { get; set; }
        
        [Column("sum")]
        public decimal Sum { get; set; }
        
        [Column("currency")]
        public string Currency { get; set; }
        
        [Column("is_active")]
        public bool IsActive { get; set; }
        
        [Column("opening_date")]
        public DateTime OpeningDate { get; set; }
        
        [Column("closing_date")]
        public DateTime ClosingDate { get; set; }
    }
}