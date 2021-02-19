using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transaction.Data.Repositories.Interfaces;
using Transaction.Models.Core;

namespace Transaction.Business.Services.Impl
{
    public class PersonService : IPersonService
    {
        private IUnitOfWork _unitOfWork;
        private IPersonRepository _personRepository;

        public PersonService(IPersonRepository personRepository, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _personRepository = personRepository;
        }

        public async Task<Person> Create(Person account)
        {
            var acct = await _personRepository.AddAsync(account);
            await _unitOfWork.SaveChangesAsync();
            return acct; // confirm everything is saved successfully
        }

        public async Task Update(Person account)
        {
            _personRepository.Update(account);
            await _unitOfWork.SaveChangesAsync();
        }


        public async Task<List<Person>> Get()
        {
            return await _personRepository.GetListAsync();
        }

        public async Task<Person> Get(Guid id)
        {
            return await _personRepository.FindByIdAsync(id);
        }
    }
}