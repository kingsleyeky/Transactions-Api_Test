using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Transaction.Entity
{
   public class Account
    {
        [Key]
        public Guid ID { get; set; }
        public string Number { get; set; }
        public string Name { get; set; }
        public decimal Balance { get; set; }
        public Guid PersonID { get; set; }
    }
}
