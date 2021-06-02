using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Transaction.Entity;

namespace Transaction.Service
{
    public interface IAccount
    {
        Task<object> AddAccount(Account account);
        Task<object> GetallAccount();
        Task<object> UpdateAccount(Account account, Guid Id);
        Task<object> GetAccountById(Guid Id);
        Task<object> GetAccountByPersonId(Guid PersonId);
    }
}
