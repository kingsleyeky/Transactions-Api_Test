using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Transaction.Data.Repositories.Interfaces;

namespace Transaction.Data.Repositories.Infrastructure
{
    public class RepositoryBase<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        protected readonly DbContext _dbContext;
        protected readonly DbSet<TEntity> _dbSet;

        public RepositoryBase(DbContext context)
        {
            _dbContext = context ?? throw new ArgumentException(nameof(context));
            _dbSet = _dbContext.Set<TEntity>();
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
            return entity;
        }

        public async Task<TEntity> FindByIdAsync(params object[] keyValues)
        {
            return await _dbSet.FindAsync(keyValues);
        }

        public async Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate = null)
        {
            IQueryable<TEntity> query = _dbSet;

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            return await query.ToListAsync();
        }

        public async Task<TEntity> GetFirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate = null)
        {
            IQueryable<TEntity> query = _dbSet;

            return await query.FirstOrDefaultAsync(predicate);
        }

        public void Update(TEntity entity)
        {
            _dbSet.Update(entity);
        }
    }
}