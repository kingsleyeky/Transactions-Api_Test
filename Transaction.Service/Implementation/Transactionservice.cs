using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Transaction.API;
using Transaction.Entity;

namespace Transaction.Service.Implementation
{
    public class Transactionservice : ITransaction
    {

        private readonly TContext _tcontex;
        //private readonly IPerson _person;

        public Transactionservice(TContext context)
        {
            _tcontex = context;
            //_person = person;
        }

        // public TContext Context { get; }
        ServiceResponse res = new ServiceResponse();

        public Task GetTransactionType(string transtype)
        {
            throw new NotImplementedException();
        }

        public async Task<object> GetTransBypersonID(Guid personID)
        {
            var trac = _tcontex.People.Where(x => x.ID == personID);

            var debitTransations = (from p in _tcontex.People
                               join a in _tcontex.Accounts on p.ID equals a.PersonID
                               join d in _tcontex.Transactions on a.ID equals d.DrAccountID
                               //join c in _tcontex.Transactions on a.ID equals c.CrAccountID
                               where p.ID == personID
                               select new TransactionViewModel
                               {
                                   Firstname = p.Firstname,
                                   Surname = p.Surname,
                                   AccountName = a.Name,
                                   AccountNumber = a.Number,
                                   TransactionType = (int)d.TransactionType,
                                   TransactionAmount = d.Amount,
                                   AccountBalance = a.Balance,
                                   TransactionDateTime = d.DateCreated,
                                   OffsetAccount = (from ac in _tcontex.Accounts
                                                    where ac.ID == d.CrAccountID
                                                    select new OffsetAccountViewModel
                                                    {
                                                        AccountName = ac.Name,
                                                        AccountNumber = ac.Number
                                                    }).FirstOrDefault()
                               }).ToList();

            var creditTransations = (from p in _tcontex.People
                                    join a in _tcontex.Accounts on p.ID equals a.PersonID
                                    join d in _tcontex.Transactions on a.ID equals d.CrAccountID
                                    //join c in _tcontex.Transactions on a.ID equals c.CrAccountID
                                    where p.ID == personID
                                    select new TransactionViewModel
                                    {
                                        Firstname = p.Firstname,
                                        Surname = p.Surname,
                                        AccountName = a.Name,
                                        AccountNumber = a.Number,
                                        TransactionType = (int)d.TransactionType,
                                        TransactionAmount = d.Amount,
                                        AccountBalance = a.Balance,
                                        TransactionDateTime = d.DateCreated,
                                        OffsetAccount = (from ac in _tcontex.Accounts
                                                         where ac.ID == d.DrAccountID
                                                         select new OffsetAccountViewModel
                                                         {
                                                             AccountName = ac.Name,
                                                             AccountNumber = ac.Number
                                                         }).FirstOrDefault()
                                    }).ToList();

            var transactions = debitTransations.Union(creditTransations);

            //if (trac != null)
            //{
            //    var check = _tcontex.Transactions.Where(x => x.CrAccountID == personID || x.DrAccountID == personID).ToList();

            //}
            return transactions;
        }

        public async Task<object> AddTransaction(Transactions transactions)
        {
            try
            {

                var data = new Transactions
                {

                   Amount = transactions.Amount,
                   CrAccountID = transactions.CrAccountID,
                   DrAccountID = transactions.DrAccountID,
                   DateCreated = DateTime.Now,
                   TransactionType = transactions.TransactionType

                };

                await _tcontex.AddAsync(data);

                int result = _tcontex.SaveChanges();
                if (result > 0)
                {

                    res.Data = result;
                    res.Success = true;
                    res.Message = "Transaction Created";
                    return res;
                }
                else
                {

                    res.Data = result;
                    res.Success = false;
                    res.Message = "Db Error";
                    return res;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<object> GetTransaction()
        {

           
           
            try
            {
                var result = await _tcontex.Transactions.ToListAsync();

                if (result == null)
                {
                    res.Data = null;
                    res.Message = "List not Available";
                    res.Success = false; ;
                    return res;
                }
                else
                {
                    res.Data = result;
                    res.Message = "List of Trasactions";
                    res.Success = true; ;
                    return res;
                }
            }
            catch (Exception ex)
            {
                res.Success = false;
                res.Message = ex.Message + ":" + ex.StackTrace;
                res.Data = null;
                return res;
            }
        }
    }
}
