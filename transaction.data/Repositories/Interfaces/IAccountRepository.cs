using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Transaction.Models.Core;

namespace Transaction.Data.Repositories.Interfaces
{
    public interface IAccountRepository : IBaseRepository<Account>
    {
        Task<Account> GetAccountByPersonIDAsync(Guid id);
        Task<List<Account>> GetAccountsByPersonIDAsync(Guid id);
    }
}
