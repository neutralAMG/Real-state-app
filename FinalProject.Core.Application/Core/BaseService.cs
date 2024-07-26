



using AutoMapper;

namespace FinalProject.Core.Application.Core
{
    public class BaseService<TModel, TSaveModel, TEntity, TId> : IBaseService<TModel, TSaveModel, TEntity, TId>
        where TModel : class
        where TSaveModel : class
        where TEntity : class
    {
        private readonly IBaseRepository<TEntity, TId> _baseRepository;
        private readonly IMapper _mapper;
        private readonly string _entityName;

        public BaseService(IBaseRepository<TEntity, TId> baseRepository, IMapper mapper, string name)
        {
            _baseRepository = baseRepository;
            _mapper = mapper;
            _entityName = name;
        }
        public async Task<Result<TSaveModel>> SaveAsync(TSaveModel saveModel)
        {
            Result<TSaveModel> result = new();
            try
            {
                TEntity entityToBeSave = _mapper.Map<TEntity>(saveModel);

                TEntity entitiSaved = await _baseRepository.SaveAsync(entityToBeSave);

                if (entitiSaved == null) {
                    result.ISuccess = false;
                    result.Message = $"Error while attempting to save the {_entityName}";
                    return result;
                }

                result.Data = _mapper.Map<TSaveModel>(entitiSaved);
                result.Message = $"The {_entityName} was saved successfully";
                return result;
            }
            catch
            {
                result.ISuccess = false;
                result.Message = $"Critical Error while attempting to save the {_entityName}";
                return result;
            }
        }  
        public async Task<Result> DeleteAsync(TId id)
        {
            Result result = new();
            try
            {
                bool isDeleteOpreationSuccses = await _baseRepository.DeleteAsync(id);
                if (!isDeleteOpreationSuccses)
                {
                    result.ISuccess = false;
                    result.Message = $"Error while attempting to delete the {_entityName}";
                    return result;
                }

                result.Message = $"The {_entityName} was deleted successfully";
                return result;

            }
            catch
            {
                result.ISuccess=false;
                result.Message = $"Critical error while attemting to delete the {_entityName}";
                return result;
            }
        }

    }
    public class BaseCompleteService<TModel, TSaveModel, TEntity, TId> : BaseService<TModel, TSaveModel, TEntity, TId>, IBaseCompleteService<TModel, TSaveModel, TEntity, TId>
        where TModel : class
        where TSaveModel : class
        where TEntity : class
    {
        private readonly IBaseCompleteRepository<TEntity, TId> _baseRepository;
        private readonly IMapper _mapper;
        private readonly string _entityName;

        public BaseCompleteService(IBaseCompleteRepository<TEntity, TId> baseRepository, IMapper mapper, string name) : base(baseRepository, mapper, name)
        {
            _baseRepository = baseRepository;
            _mapper = mapper;
            _entityName = name;
        }

        public async Task<Result<List<TModel>>> GetAllAsync()
        {
            Result<List<TModel>> result = new();
            try
            {
                List<TEntity> entitesGetted = await _baseRepository.GetAllAsync();

                result.Data = _mapper.Map<List<TModel>>(entitesGetted);

                result.Message = $"The {_entityName}'s get was a success";
                return result;
            }
            catch
            {
                result.ISuccess = false;
                result.Message = $"Critica error while getting all the {_entityName}'s";
                return result;
            }
        }

        public async Task<Result<TModel>> GetByIdAsync(TId id)
        {
            Result<TModel> result = new();
            try
            {
                TEntity entityGetted = await _baseRepository.GetByIdAsync(id);

                if (entityGetted == null) { 
                    result.ISuccess = false;    
                    result.Message = $"Error while getting the {_entityName} by it's Id";
                    return result;
                }

                result.Data = _mapper.Map<TModel>(entityGetted);
                result.Message = $"The {_entityName} get was a success";
                return result;
            }
            catch
            {
                result.ISuccess= false;
                result.Message = $"Critica error while getting the {_entityName} by it's id ";
                return result;
            }
        }

        public async Task<Result<TSaveModel>> UpdateAsync(TId id, TSaveModel updateModel)
        {
            Result<TSaveModel> result = new();
            try
            {
                TEntity entityToBeUpdate = _mapper.Map<TEntity>(updateModel);

                TEntity entityUpdated = await _baseRepository.UpdateAsync(entityToBeUpdate);

                if (entityUpdated == null) { 
                    result.ISuccess = false;
                    result.Message = $"Error while attempting to updated the {_entityName}";
                    return result;
                }

                result.Data = _mapper.Map<TSaveModel>(entityUpdated);

                result.Message = $"The {_entityName} updated successfully";
                return result;
            }
            catch
            {
                result.ISuccess = false;
                result.Message = $"Critical error while attempting to update the {_entityName}";
                return result;  
            }
        }
    }
}
