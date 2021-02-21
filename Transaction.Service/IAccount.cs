using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Transaction.Entity;

namespace Transaction.Service
{
    public interface IAccount
    {
        Task GetAccountID(int accountid);
        Task GetAccountNumber(int accountnos);
        Task GetAccountName(string accountname);
        Task<object> AddAccount(Account account);
    }
}
