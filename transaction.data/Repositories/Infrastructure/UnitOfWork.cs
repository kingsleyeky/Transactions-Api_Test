﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Transaction.Data.Repositories.Infrastructure
{
    public class UnitOfWork<TContext> : IUnitOfWork<TContext>, IUnitOfWork where TContext : DbContext
    {

        private Dictionary<Type, object> _repositories;

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
                

        /// <summary>
        /// Dynamically build a repository of <typeparamref name="TEntity"/>
        /// </summary>
        /// <typeparam name="TEntity">The repository entity class</typeparam>
        /// <returns></returns>
        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : class
        {
            if (_repositories == null) _repositories = new Dictionary<Type, object>();

            var type = typeof(TEntity);
            if (!_repositories.ContainsKey(type)) _repositories[type] = new RepositoryBase<TEntity>(dbContext);
            return (IRepository<TEntity>)_repositories[type];
        }
    }
}
