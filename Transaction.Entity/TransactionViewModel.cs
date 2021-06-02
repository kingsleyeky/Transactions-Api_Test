using System;
using System.Collections.Generic;
using System.Text;

namespace Transaction.Entity
{
    public class TransactionViewModel
    {
        public string Surname { get; set; }
        public string Firstname { get; set; }
        public string AccountName { get; set; }
        public string AccountNumber { get; set; }
        public decimal TransactionAmount { get; set; }
        public int TransactionType { get; set; }
        public decimal AccountBalance { get; set; }
        public OffsetAccountViewModel OffsetAccount { get; set; }
        public DateTime TransactionDateTime { get; set; }

    }

    public class OffsetAccountViewModel
    {
        public string AccountName { get; set; }
        public string AccountNumber { get; set; }
    }

}
