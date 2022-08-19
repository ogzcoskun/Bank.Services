using Authentication.Models.RegisterModels;
using MediatR;

namespace Authentication.Commands
{
    public class RegisterCommand : IRequest<ResponseViewModel>
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
