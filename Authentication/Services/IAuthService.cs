using Authentication.Models.IdentityModels;
using Authentication.Models.RegisterModels;
using System.Threading.Tasks;

namespace Authentication.Services
{
    public interface IAuthService
    {  
        Task<ResponseViewModel> UserExist(RegisterViewModel model);
        Task<ResponseViewModel> CreateUser(RegisterViewModel model);
        Task<ResponseViewModel> Login(LoginViewModel model);
    }
}
