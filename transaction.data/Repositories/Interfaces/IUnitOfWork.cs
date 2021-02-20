using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Transaction.Data.Repositories.Interfaces
{
    public interface IUnitOfWork
    {
        /// <summary>
        /// Saves the underlying changes to the database asynchronously
        /// </summary>
        Task<int> SaveChangesAsync();
        /// <summary>
        /// Initiate Database transaction on the current context
        /// </summary>
        void BeginTransaction();
        /// <summary>
        /// Commits the current transaction
        /// </summary>
        void Commit();
        /// <summary>
        /// Rollback the current transaction
        /// </summary>
        void Rollback();
    }

}
