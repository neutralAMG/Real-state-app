
using System.Linq.Expressions;

namespace FinalProject.Core.Application.Core
{
    public interface IBaseRepository<TEntity, TId> 
        where TEntity : class
    {
        Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate);
   
        Task<TEntity> SaveAsync(TEntity entity);
        Task<TEntity> UpdateAsync(TEntity entity);
        Task<bool> DeleteAsync(TEntity entity);

    }

    public interface IBaseCompleteRepository<TEntity, TId> : IBaseRepository<TEntity, TId>
      where TEntity : class
    {
        Task<IList<TEntity>> GetAllAsync();
        Task<TEntity> GetByIdAsync(TId id);
    }
}
