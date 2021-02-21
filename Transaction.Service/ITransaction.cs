using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Transaction.Entity;

namespace Transaction.Service
{
    public interface ITransaction
    {
        Task GetTransactionType(string transtype);
        Task<object> GetTransBypersonID(Guid personID);
        Task<object> AddTransaction(Transactions transactions);
        Task<object> GetTransaction();
    }
}
