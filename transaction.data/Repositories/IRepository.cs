using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Transaction.Data.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task AddAsync(TEntity entity);
        Task AddAsync(IEnumerable<TEntity> entities);
        void Update(TEntity entity);
        Task<TEntity> FindByIdAsync(params object[] keyValues);

        Task<IEnumerable<TEntity>> ToListAsync(Expression<Func<TEntity, bool>> predicate = null);

    }
}
