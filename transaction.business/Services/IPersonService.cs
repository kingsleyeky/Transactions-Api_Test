using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Transaction.Models.Core;

namespace Transaction.Business.Services
{
    public interface IPersonService
    {
        Task<Person> Create(Person  person);
        Task Update(Person  person);
        Task<List<Person>> Get();
        Task<Person> Get(Guid id);
    }
}
