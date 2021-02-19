using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;


namespace Transaction.Business.Services
{
    public interface ITransactionService
    {
        Task<List<Models.Core.Transaction>> Get();
        Task<Models.Core.Transaction> Get(Guid id);
        Task<Models.Core.Transaction> Create(Models.Core.Transaction transaction);
        Task<List<Models.Core.Transaction>> GetTransactionsByPersonIDAsync(Guid id);
    }
}