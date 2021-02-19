using System;

namespace Transaction.Models.Core
{
    public class Account
    {
        public Guid ID { get; set; }
        public string Number { get; set; }
        public string Name { get; set; }
        public decimal Balance { get; set; }
        public Guid PersonID { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
