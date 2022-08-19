using Authentication.Models.RegisterModels;
using Authentication.Services;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Authentication.Commands.Handlers
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, ResponseViewModel>
    {
        private readonly IAuthService _authService;

        public RegisterCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<ResponseViewModel> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {

            var user = new RegisterViewModel()
            {
                FullName = request.FullName,
                Email = request.Email,
                Password = request.Password,
                ConfirmPassword = request.ConfirmPassword,
            };

            var userExist = await _authService.UserExist(user);

            if (userExist.IsSuccess)
            {
                return new ResponseViewModel()
                {
                    IsSuccess = false,
                    Message = userExist.Message
                };
            }

            var createUser = await _authService.CreateUser(user);

            return new ResponseViewModel()
            {
                IsSuccess = createUser.IsSuccess,
                Message = createUser.Message
            };
        
        }
    }
}
