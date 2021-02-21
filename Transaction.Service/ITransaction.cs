using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Transaction.Entity;

namespace Transaction.Service
{
    public interface ITransaction
    {
        Task GetTransactionID(int transID);
        Task GetTransactionType(string transtype);
        Task GetTransactionAmount(string transAmount);
        Task GetTransactionBalance(string transBalance);
        Task<object> GetTransBypersonID(Guid personID);
        Task<object> AddTransaction(Transactions transactions);
    }
}
