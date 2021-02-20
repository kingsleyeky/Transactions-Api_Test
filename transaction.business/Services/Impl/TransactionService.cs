using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transaction.Data.DTOs;
using Transaction.Data.Repositories.Interfaces;
using Transaction.Models.Core;

namespace Transaction.Business.Services.Impl
{
    public class TransactionService : ITransactionService
    {
        private IUnitOfWork _unitOfWork;
        private ITransactionRepository _transactionRepository;
        private IAccountRepository _accountRepository;
        private IPersonRepository _personRepository;

        public TransactionService(ITransactionRepository transactionRepository, IAccountRepository accountRepository,
           IPersonRepository personRepository, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _transactionRepository = transactionRepository;
            _accountRepository = accountRepository;
            _personRepository = personRepository;
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

        public async Task<List<PersonTransactionDTO>> GetTransactionsByPersonIDAsync(Guid id)
        {
            // since there was no relationship specified in the SQL scripts between Transaction=>Account=>Persion,
            // we used multiple queries to get the needed record. Relationship would have provided lazy loading here.

            var accountList = await _accountRepository.GetAccountsByPersonIDAsync(id);
            // let load affected persons once to avoid performance issues on round trip
            var persons = await _personRepository.GetListAsync(predicate: p => accountList.Select(a => a.PersonID).Contains(p.ID));

            var accountTransactions = await _transactionRepository.GetTransactionsByAccountIds(accountList.Select(a => a.ID));
            var vm = accountTransactions.ConvertAll(at =>
           {
               Account drAccount = accountList.FirstOrDefault(a => a.ID == at.DrAccountID),
               crAccount = accountList.FirstOrDefault(a => a.ID == at.CrAccountID);

               Person drPersion = persons.FirstOrDefault(p => p.ID == drAccount?.PersonID),
               crPersion = persons.FirstOrDefault(p => p.ID == crAccount?.PersonID);

               bool isCreditTrans = at.TransactionType == TransactionType.CR;
               return new PersonTransactionDTO
               {
                   AccountBalance = (isCreditTrans ? drAccount?.Balance : crAccount?.Balance) ?? 0,
                   AccountName = isCreditTrans ? drAccount?.Name : crAccount?.Name,
                   AccountNumber = isCreditTrans ? drAccount?.Number : crAccount?.Number,
                   Firstname = isCreditTrans ? drPersion?.Firstname : crPersion?.Firstname,
                   Surname = isCreditTrans ? drPersion?.Surname : crPersion?.Surname,
                   OffsetAccount = isCreditTrans ? $"{crAccount?.Name} {crAccount.Number}" : $"{drAccount?.Name} {drAccount.Number}",
                   TransactionAmount = at.Amount,
                   TransactionType = at.TransactionType,
                   TransactionDateTime = at.DateCreated.ToString("ddd/MM/yyyy h:mm:ss tt")
               };
           });

            return vm;
        }
    }
}
