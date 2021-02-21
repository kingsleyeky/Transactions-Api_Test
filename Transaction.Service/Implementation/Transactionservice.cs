using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Transaction.API;

namespace Transaction.Service.Implementation
{
    public class Transactionservice : ITransaction
    {

        private readonly TContext _tcontex;
        private readonly IPerson _person;

        public Transactionservice(TContext context,IPerson person)
        {
            _tcontex = context;
            _person = person;
        }

       // public TContext Context { get; }

        public Task GetTransactionAmount(string transAmount)
        {
            throw new NotImplementedException();
        }

        public Task GetTransactionBalance(string transBalance)
        {
            throw new NotImplementedException();
        }

        public Task GetTransactionID(int transID)
        {
            throw new NotImplementedException();
        }

        public Task GetTransactionType(string transtype)
        {
            throw new NotImplementedException();
        }

        public Task GetTransBypersonID(int personID)
        {
            //var cause = (from s in _tcontex.People
            //             join c in _tcontex.Accounts on s
            //             where s.OrderId == orderId
            //             select c).FirstOrDefault();

            return null;
        }
    }
}
