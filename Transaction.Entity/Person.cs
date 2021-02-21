using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Transaction.Entity
{
   public class Person
    {
        [Key]
        public Guid ID { get; set; }
        public string Surname { get; set; }
        public string Firstname { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
