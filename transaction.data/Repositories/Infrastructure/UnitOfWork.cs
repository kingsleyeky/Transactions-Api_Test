using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Transaction.Data.Repositories.Interfaces;

namespace Transaction.Data.Repositories.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        public TContext dbContext { get; }
        private IDbContextTransaction transaction;

        public UnitOfWork(TContext context)
        {
            dbContext = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// <summary>
        /// Initiate Database transaction on the current context
        /// </summary>
        public void BeginTransaction()
        {
            transaction = dbContext.Database.BeginTransaction();
        }

        /// <summary>
        /// Saves the underlying changes to the database asynchronously
        /// </summary>
        public async Task<int> SaveChangesAsync()
        {
            return await dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Rollback the current transaction
        /// </summary>
        public void Rollback()
        {
            if (transaction != null)
                transaction.Rollback();
            transaction = null;
        }

        /// <summary>
        /// Commits the current transaction
        /// </summary>
        public void Commit()
        {
            if (transaction != null)
                transaction.Commit();
            transaction = null;
        }        

    }
}