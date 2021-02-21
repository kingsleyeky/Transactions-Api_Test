using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Transaction.Entity
{
    public class Transactions
    {
        [Key]
        public Guid ID { get; set; }
        public decimal Amount { get; set; }
        public TransactionType TransactionType { get; set; }
        public Guid DrAccountID { get; set; }
        public Guid CrAccountID { get; set; }
        public DateTime DateCreated { get; set; }
    }

    public enum TransactionType
    {
        CR = 1,
        DR = 2
    }
}
