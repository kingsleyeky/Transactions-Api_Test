using System;
using System.Collections.Generic;
using System.Text;

namespace Transaction.Entity
{
    public class ServiceResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public Object Data { get; set; }
    }
}
