

namespace FinalProject.Core.Application.Core
{
    public interface IBaseService<TModel, TSaveModel, TEntity, TId>
        where TModel : class
        where TSaveModel : class
        where TEntity : class
    {
        Task<Result<TSaveModel>> SaveAsync(TSaveModel saveModel);
        Task<Result> DeleteAsync(TId id);
    }
    public interface IBaseCompleteService<TModel, TSaveModel, TEntity, TId> : IBaseService<TModel, TSaveModel, TEntity, TId>
        where TModel : class
        where TSaveModel : class
        where TEntity : class
    {
        Task<Result<List<TModel>>> GetAllAsync();
        Task<Result<TModel>> GetByIdAsync(TId id);
        Task<Result<TSaveModel>> UpdateAsync(TId id,TSaveModel updateModel);

    }
}
