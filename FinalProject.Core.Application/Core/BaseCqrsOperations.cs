

using AutoMapper;

namespace FinalProject.Core.Application.Core
{
    public static class BaseCqrsOperations
    {
        //Make this to a builder 
        public static async Task<Result<TReturnValue>> SaveAsync<TSaveModel, TReturnValue, TEntity, TId>(IBaseRepository<TEntity, TId> repository, IMapper mapper, TSaveModel saveModel, string name)
         where TEntity : class
        {
            Result<TReturnValue> result = new();
            try
            {
                TEntity entityToBeSave = mapper.Map<TEntity>(saveModel);

                TEntity entitiSaved = await repository.SaveAsync(entityToBeSave);

                if (entitiSaved == null)
                {
                    result.ISuccess = false;
                    result.Message = $"Error while attempting to save the {name}";
                    return result;
                }

                result.Data = mapper.Map<TReturnValue>(entitiSaved);
                result.Message = $"The {name} was saved successfully";
                return result;
            }
            catch
            {
                result.ISuccess = false;
                result.Message = $"Critical Error while attempting to save the {name}";
                return result;
            }
        }
        public static async Task<Result> DeleteAsync<TEntity, TId>(IBaseRepository<TEntity, TId> repository, TId id, string name)
         where TEntity : class
        {
            Result result = new();
            try
            {
                bool isDeleteOpreationSuccses = await repository.DeleteAsync(id);
                if (!isDeleteOpreationSuccses)
                {
                    result.ISuccess = false;
                    result.Message = $"Error while attempting to delete the {name}";
                    return result;
                }

                result.Message = $"The {name} was deleted successfully";
                return result;

            }
            catch
            {
                result.ISuccess = false;
                result.Message = $"Critical error while attemting to delete the {name}";
                return result;
            }
        }

        public static async Task<Result<List<TModel>>> GetAllAsync<TModel, TEntity, TId>(IBaseCompleteRepository<TEntity, TId> repository, IMapper mapper, string name)
       where TEntity : class
        {
            Result<List<TModel>> result = new();
            try
            {
                List<TEntity> entitesGetted = await repository.GetAllAsync();

                result.Data = mapper.Map<List<TModel>>(entitesGetted);

                result.Message = $"The {name}'s get was a success";
                return result;
            }
            catch
            {
                result.ISuccess = false;
                result.Message = $"Critica error while getting all the {name}'s";
                return result;
            }
        }

        public static async Task<Result<TModel>> GetByIdAsync<TModel, TEntity, TId>(IBaseCompleteRepository<TEntity, TId> repository, IMapper mapper, TId id, string name)
         where TEntity : class
        {
            Result<TModel> result = new();
            try
            {
                TEntity entityGetted = await repository.GetByIdAsync(id);

                if (entityGetted == null)
                {
                    result.ISuccess = false;
                    result.Message = $"Error while getting the {name} by it's Id";
                    return result;
                }

                result.Data = mapper.Map<TModel>(entityGetted);
                result.Message = $"The {name} get was a success";
                return result;
            }
            catch
            {
                result.ISuccess = false;
                result.Message = $"Critica error while getting the {name} by it's id ";
                return result;
            }
        }

        public static async Task<Result<TReturnValue>> UpdateAsync<TSaveModel, TReturnValue, TEntity, TId>(IBaseCompleteRepository<TEntity, TId> repository, IMapper mapper, TId id, TSaveModel updateModel, string name)
         where TEntity : class
        {
            Result<TReturnValue> result = new();
            try
            {
                TEntity entityToBeUpdate = mapper.Map<TEntity>(updateModel);

                TEntity entityUpdated = await repository.UpdateAsync(entityToBeUpdate);

                if (entityUpdated == null)
                {
                    result.ISuccess = false;
                    result.Message = $"Error while attempting to updated the {name}";
                    return result;
                }

                result.Data = mapper.Map<TReturnValue>(entityUpdated);

                result.Message = $"The {name} updated successfully";
                return result;
            }
            catch
            {
                result.ISuccess = false;
                result.Message = $"Critical error while attempting to update the {name}";
                return result;
            }
        }
    }
}


