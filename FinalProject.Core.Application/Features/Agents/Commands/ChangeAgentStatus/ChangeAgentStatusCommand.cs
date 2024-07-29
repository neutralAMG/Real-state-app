

using AutoMapper;
using FinalProject.Core.Application.Core;
using FinalProject.Core.Application.Dtos.Identity.User;
using FinalProject.Core.Application.Interfaces.Repositories.Identity;
using MediatR;

namespace FinalProject.Core.Application.Features.Agents.Commands.ChangeAgentStatus
{
    public class ChangeAgentStatusCommand : IRequest<Result> 
    {
        public string Id { get; set; }
        public bool status { get; set; }
    }

    public class ChangeAgentStatusCommandHandler : IRequestHandler<ChangeAgentStatusCommand, Result>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public ChangeAgentStatusCommandHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
        public async Task<Result> Handle(ChangeAgentStatusCommand request, CancellationToken cancellationToken)
        {
            return await HandleUserActivationStateAsync(request);
        }
        private async Task<Result> HandleUserActivationStateAsync(ChangeAgentStatusCommand request)
        {
            Result result = new();

            string operation = request.status == true ? "activativated" : "deactivated";

            try
            {

                UserOperationResponce responce = await _userRepository.HandleUserActivationStateAsync(request.Id, request.status);

                if (responce.HasError)
                {
                    result.ISuccess = false;
                    result.Message = responce.ErrorMessage;
                    return result;
                }

                result.Message = $"The user was {operation} successfully";

                return result;
            }
            catch
            {
                result.ISuccess = false;
                result.Message = $"Critical error while attempting to {operation} the user ";
                return result;
            }
        }

    }
}
