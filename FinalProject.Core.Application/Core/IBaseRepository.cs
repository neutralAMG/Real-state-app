
namespace FinalProject.Core.Application.Core
{
    public interface IBaseRepository<TEntity, TId> 
        where TEntity : class
    {
        Task<bool> ExistsAsync(Func<TEntity, bool> predicate);
        Task<IList<TEntity>> GetAllAsync();
        Task<TEntity> GetByIdAsync(TId id);
        Task<TEntity> SaveAsync(TEntity entity);
        Task<TEntity> UpdateAsync(TEntity entity);
        Task<bool> DeleteAsync(TEntity entity);

    }
}
