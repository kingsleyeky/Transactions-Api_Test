using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Transaction.Data.Repositories.Interfaces
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        Task<TEntity> AddAsync(TEntity entity);
        void Update(TEntity entity);
        Task<TEntity> FindByIdAsync(params object[] keyValues);
        Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate = null);
        Task<TEntity> GetFirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate = null);

    }
}
