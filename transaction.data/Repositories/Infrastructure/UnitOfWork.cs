using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Transaction.Data.Repositories.Interfaces;

namespace Transaction.Data.Repositories.Infrastructure
{
    public class UnitOfWork<TContext> : IUnitOfWork<TContext>, IUnitOfWork where TContext : DbContext
    {      
        public TContext dbContext { get; }

        public UnitOfWork(TContext context)
        {
            dbContext = context ?? throw new ArgumentNullException(nameof(context));
        }

       
        /// <summary>
        /// Saves the underlying changes to the database asynchronously
        /// </summary>
        public async Task<int> SaveChangesAsync()
        {
            return await dbContext.SaveChangesAsync();
        }
                       
    }
}
