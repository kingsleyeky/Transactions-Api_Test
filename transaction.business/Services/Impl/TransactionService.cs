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
            try
            {
                if (transaction.ID == Guid.Empty)
                    transaction.ID = Guid.NewGuid();

                _unitOfWork.BeginTransaction();
                // save the transaction
                var trans = await _transactionRepository.AddAsync(transaction);
                await _unitOfWork.SaveChangesAsync();
                // credit the person account
                var crAccount = await _accountRepository.FindByIdAsync(trans.CrAccountID);
                if (crAccount == null) throw new ApplicationException("Invalid Cr Account.");
                crAccount.Balance += trans.Amount;
                _accountRepository.Update(crAccount);

                // debit the person account
                var drAccount = await _accountRepository.FindByIdAsync(trans.DrAccountID);
                if (drAccount == null) throw new ApplicationException("Invalid Dr Account.");
                drAccount.Balance -= trans.Amount;
                _accountRepository.Update(drAccount);

                await _unitOfWork.SaveChangesAsync();

                _unitOfWork.Commit();
                return trans; 
            }
            catch
            {
                _unitOfWork.Rollback();
                throw;
            }
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

            var personAccount = await _accountRepository.GetAccountByPersonIDAsync(id);
            // get all the transactions for this person
            var transactions = await _transactionRepository.GetTransactionsByAccountId(personAccount.ID);
            // get the accountIds involved in the transaction
            var accountList = transactions.Select(a => a.CrAccountID).Union(transactions.Select(a => a.DrAccountID));
            // get all the accountinfo involved in the transaction
            var transactionAccounts = await _accountRepository.GetListAsync(predicate: a => accountList.Contains(a.ID));
            // get all the persons tie to the accounts above
            var persons = await _personRepository.GetListAsync(predicate: p => transactionAccounts.Select(a => a.PersonID).Contains(p.ID));

            var vm = transactions.ConvertAll(at =>
           {
               Account drAccount = transactionAccounts.FirstOrDefault(a => a.ID == at.DrAccountID),
               crAccount = transactionAccounts.FirstOrDefault(a => a.ID == at.CrAccountID);

               Person drPersion = persons.FirstOrDefault(p => p.ID == drAccount?.PersonID),
               crPersion = persons.FirstOrDefault(p => p.ID == crAccount?.PersonID);

               bool isCreditTrans = at.TransactionType == TransactionType.CR;
               return new PersonTransactionDTO
               {
                   // this logic below is as was clarified on the email:
                   /*
                        The question is, if the transaction is debit, and "Offset Account" carries the name & number related to the "DrAccountID", 
                        will the "AccountName" and "AccountNumber" mentioned next to "Firstname" come from account related to "CrAccountID" and vice versa?
                   */
                   AccountBalance = (isCreditTrans ? drAccount?.Balance : crAccount?.Balance) ?? 0,
                   AccountName = isCreditTrans ? drAccount?.Name : crAccount?.Name,
                   AccountNumber = isCreditTrans ? drAccount?.Number : crAccount?.Number,
                   Firstname = isCreditTrans ? drPersion?.Firstname : crPersion?.Firstname,
                   Surname = isCreditTrans ? drPersion?.Surname : crPersion?.Surname,

                   OffsetAccount = isCreditTrans ? $"{crAccount?.Name} {crAccount.Number}" : $"{drAccount?.Name} {drAccount.Number}",
                   TransactionAmount = at.Amount,
                   TransactionType = at.TransactionType,
                   TransactionDateTime = at.DateCreated.ToString("dd/MM/yyyy h:mm:ss tt")
               };
           });

            return vm;
        }
    }
}
