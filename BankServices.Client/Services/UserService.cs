using BankServices.Client.OutModels;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace BankServices.Client.Services
{
    public class UserService : IUserService
    {
        private readonly AuthContext _context;
        public UserService(AuthContext context)
        {
            _context = context;
        }
        public async Task<string> GetUserId(string accessToken)
        {
            accessToken = accessToken.Split(" ")[1]; // Gives the token without "Bearer"
            var userId = await _context.AspNetUserTokens.FirstOrDefaultAsync(x => x.Value == accessToken);

            return userId.UserId;
        }
    }
}
