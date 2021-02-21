using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Transaction.Service
{
    public interface IAccount
    {
        Task GetAccountID(int accountid);
        Task GetAccountNumber(int accountnos);
        Task GetAccountName(string accountname);
    }
}
