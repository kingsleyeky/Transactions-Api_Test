using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Transaction.API;
using Transaction.Entity;

namespace Transaction.Service.Implementation
{
    public class Accountservice : IAccount
    {
        private readonly TContext _tcontex;

        

        public Accountservice(TContext context)
        {
            _tcontex = context;
        }

        ServiceResponse res = new ServiceResponse();

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

        public async Task<object> AddAccount(Account account)
        {
            try
            {

                var data = new Account
                {
                    Balance = account.Balance,
                    Name = account.Name,
                    Number = account.Number,
                    PersonID = account.PersonID

                };


                await _tcontex.AddAsync(data);
               
                int result = _tcontex.SaveChanges();
                if (result > 0)
                {

                    res.Data = result;
                    res.Success = true;
                    res.Message = "Account Created";
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
    }
}
