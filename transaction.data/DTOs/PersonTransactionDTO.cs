using System;
using System.Collections.Generic;
using System.Text;
using Transaction.Models.Core;

namespace Transaction.Data.DTOs
{
    public class PersonTransactionDTO
    {
        public string Surname { get; set; }

        public string Firstname { get; set; }

        public string AccountName { get; set; }
        public string AccountNumber { get; set; }
        public TransactionType TransactionType { get; set; }

        public decimal TransactionAmount { get; set; }
        public decimal AccountBalance { get; set; }
        /// <summary>
        /// Offset Account is the account Name and Number of the DrAccountID or CrAccountID depending on the TransactionType.
        /// </summary>
        public string OffsetAccount { get; set; }
        public string TransactionDateTime { get; set; }

    }
}