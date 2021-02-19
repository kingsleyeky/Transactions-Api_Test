using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Transaction.Models.Core;

namespace Transaction.Business.Services
{
    public interface IAccountService
    {
        Task<Account> Create(Account account);
        Task Update(Account account);
        Task<List<Account>> Get();
        Task<Account> Get(Guid id);
        Task<Account> GetAccountByPersonID(Guid id);
    }
}
