using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Transaction.Data.DTOs;

namespace Transaction.Business.Services
{
    public interface ITransactionService
    {
        Task<List<Models.Core.Transaction>> Get();
        Task<Models.Core.Transaction> Get(Guid id);
        Task<Models.Core.Transaction> Create(Models.Core.Transaction transaction);
        Task<List<PersonTransactionDTO>> GetTransactionsByPersonIDAsync(Guid id);
    }
}