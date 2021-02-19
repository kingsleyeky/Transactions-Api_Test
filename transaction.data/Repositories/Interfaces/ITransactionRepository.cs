using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Transaction.Data.Repositories.Interfaces
{
    public interface ITransactionRepository : IBaseRepository<Models.Core.Transaction>
    {
        Task<List<Models.Core.Transaction>> GetTransactionsByAccountId(Guid id);
        Task<List<Models.Core.Transaction>> GetTransactionsByAccountIds(IEnumerable<Guid> id);
    }
}
