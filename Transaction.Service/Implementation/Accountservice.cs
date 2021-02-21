using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Transaction.API;

namespace Transaction.Service.Implementation
{
    public class Accountservice : IAccount
    {
        private readonly TContext _tcontex;

        public Accountservice(TContext context)
        {
            _tcontex = context;
        }
        public Task GetAccountID(int accountid)
        {
            throw new NotImplementedException();
        }

        public Task GetAccountName(string accountname)
        {
            throw new NotImplementedException();
        }

        public Task GetAccountNumber(int accountnos)
        {
            throw new NotImplementedException();
        }
    }
}
