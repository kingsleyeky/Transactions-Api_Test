using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transaction.Data.Repositories.Interfaces;
using Transaction.Models.Core;

namespace Transaction.Business.Services.Impl
{
    public class AccountService : IAccountService
    {
        private IUnitOfWork _unitOfWork;
        private IAccountRepository _accountRepository;

        public AccountService(IAccountRepository accountRepository, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _accountRepository = accountRepository;
        }

        public async Task<Account> Create(Account account)
        {
            var acct = await _accountRepository.AddAsync(account);
            await _unitOfWork.SaveChangesAsync();
            return acct; // confirm everything is saved successfully
        }

        public async Task Update(Account account)
        {
            _accountRepository.Update(account);
            await _unitOfWork.SaveChangesAsync();
        }


        public async Task<List<Account>> Get()
        {
            return await _accountRepository.GetListAsync();
        }

        public async Task<Account> Get(Guid id)
        {
            return await _accountRepository.FindByIdAsync(id);
        }

        public async Task<Account> GetAccountByPersonID(Guid id)
        {
            var account = await _accountRepository.GetAccountByPersonIDAsync(id);
            return account;
        }
    }
}