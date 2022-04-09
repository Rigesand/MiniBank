using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiniBank.Data.RemittanceHistories
{
    public class RemittanceHistoryDbModel
    {
        [Column("id")]
        public Guid Id { get; set; }
        [Column("sum")]
        public decimal Sum { get; set; }
        [Column("currency")]
        public string Currency { get; set; }
        [Column("from_account_id")]
        public Guid FromAccountId { get; set; }
        [Column("to_account_id")]
        public Guid ToAccountId { get; set; }
    }
}