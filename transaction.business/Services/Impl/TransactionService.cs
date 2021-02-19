using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transaction.Data.Repositories.Interfaces;

namespace Transaction.Business.Services.Impl
{
    public class TransactionService : ITransactionService
    {
        private IUnitOfWork _unitOfWork;
        private ITransactionRepository _transactionRepository;
        private IAccountRepository _accountRepository;

        public TransactionService(ITransactionRepository transactionRepository, IAccountRepository accountRepository, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _transactionRepository = transactionRepository;
            _accountRepository = accountRepository;
        }

        public async Task<Models.Core.Transaction> Create(Models.Core.Transaction transaction)
        {
            var trans = await _transactionRepository.AddAsync(transaction);
            await _unitOfWork.SaveChangesAsync();
            return trans; // confirm everything is saved successfully
        }

        public async Task<List<Models.Core.Transaction>> Get()
        {
            return await _transactionRepository.GetListAsync();
        }

        public async Task<Models.Core.Transaction> Get(Guid id)
        {
            return await _transactionRepository.FindByIdAsync(id);
        }

        public async Task<List<Models.Core.Transaction>> GetTransactionsByPersonIDAsync(Guid id)
        {
            // since there was no relationship specified in the SQL scripts between Transaction=>Account=>Persion,
            // we used multiple queries to get the needed record. Relationship would have provided lazy loading here.

            // first we get list of account for the person
            var accountList = await _accountRepository.GetAccountsByPersonIDAsync(id);
            var accountTransactions = await _transactionRepository.GetTransactionsByAccountIds(accountList.Select(a => a.ID));
            return accountTransactions;
        }
    }
}
