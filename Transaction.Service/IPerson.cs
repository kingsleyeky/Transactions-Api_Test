using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Transaction.Service
{
   public interface IPerson
    {
        Task GetPerson(int Id);
        Task GetPersonName(string Name);
        Task GetPersonEmail(string Email);
    }
}
