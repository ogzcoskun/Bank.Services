using System.Threading.Tasks;

namespace BankServices.Client.Services
{
    public interface IUserService
    {
        Task<string> GetUserId(string accessToken);
    }
}
