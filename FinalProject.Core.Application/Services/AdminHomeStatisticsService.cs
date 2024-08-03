using AutoMapper;
using FinalProject.Core.Application.Core;
using FinalProject.Core.Application.Dtos.Identity.User;
using FinalProject.Core.Application.Interfaces.Contracts;
using FinalProject.Core.Application.Interfaces.Repositories.Identity;
using FinalProject.Core.Application.Interfaces.Repositories.Persistance;
using FinalProject.Core.Application.Models;


namespace FinalProject.Core.Application.Services
{
    public class AdminHomeStatisticsService : IHomeStatisticsService
    {
        private readonly IPropertyRepository _propertyRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public AdminHomeStatisticsService(IPropertyRepository propertyRepository, IUserRepository userRepository, IMapper mapper)
        {
            _propertyRepository = propertyRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }
        public async Task<Result<HomeViewStatisticsModel>> GetAdminStatisticsAsync()
        {
            Result<HomeViewStatisticsModel> result = new();
            try
            {
                GetUserStatisticDto userData = await _userRepository.GetUserStatistics();
                 result.Data = _mapper.Map<HomeViewStatisticsModel>(userData);
                 result.Data.AmountOfProperties = await _propertyRepository.GetAmountOfPropertiesAsync();

                result.Message = "The stats where getted succesfully";
                return result;
            }
            catch
            {
                result.ISuccess = false;
                result.Message = "Critical error getting the statstics";
                return result;
            }
        }
    }
}
