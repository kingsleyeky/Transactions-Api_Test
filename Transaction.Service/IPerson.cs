using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Transaction.Entity;

namespace Transaction.Service
{
   public interface IPerson
    {
        Task<object> GetPersonById(Guid Id);
        Task<object> AddPerson(Person person);
        Task<object> GetallPerson();
        Task<object> UpdatePerson(Person person, Guid Id);
    }
}
