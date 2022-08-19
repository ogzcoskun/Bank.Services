using Authentication.Models.RegisterModels;
using MediatR;

namespace Authentication.Commands
{
    public class LoginCommand : IRequest<ResponseViewModel>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
