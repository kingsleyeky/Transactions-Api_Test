using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transaction.Data.Repositories.Infrastructure;
using Transaction.Data.Repositories.Interfaces;

namespace Transaction.Data.Repositories.Impl
{
    public class TransactionRepository : RepositoryBase<Models.Core.Transaction>, ITransactionRepository
    {
        public TransactionRepository(TContext context)
            : base(context)
        {
        }

        public async Task<List<Models.Core.Transaction>> GetTransactionsByAccountId(Guid id)
        {
            return await GetListAsync(predicate: e => e.CrAccountID == id || e.DrAccountID == id);
        }

        public async Task<List<Models.Core.Transaction>> GetTransactionsByAccountIds(IEnumerable<Guid> id)
        {
            return await GetListAsync(predicate: e => id.Contains(e.CrAccountID) || id.Contains(e.DrAccountID));
        }
    }
}
