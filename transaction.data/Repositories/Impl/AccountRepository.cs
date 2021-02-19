using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Transaction.Data.Repositories.Infrastructure;
using Transaction.Data.Repositories.Interfaces;
using Transaction.Models.Core;

namespace Transaction.Data.Repositories.Impl
{
    public class AccountRepository : RepositoryBase<Account>, IAccountRepository
    {
        public AccountRepository(TContext context)
            : base(context)
        {            
        }

        public async Task<Account> GetAccountByPersonIDAsync(Guid id)
        {
            return await GetFirstOrDefaultAsync(predicate: e => e.PersonID == id);
        }

        public async Task<List<Account>> GetAccountsByPersonIDAsync(Guid id)
        {
            return await GetListAsync(predicate: e => e.PersonID == id);
        }
    }
}
