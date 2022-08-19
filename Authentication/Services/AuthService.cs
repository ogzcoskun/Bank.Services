using Authentication.Data;
using Authentication.Library;
using Authentication.Models.IdentityModels;
using Authentication.Models.RegisterModels;
using BankServices.Authentication.OutModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.Services
{
    public class AuthService : IAuthService
    {


        private readonly SignInManager<ApplicationUser> _signInManager;

        private readonly UserManager<ApplicationUser> _userManager;

        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ClientContext _clientContext;
        private readonly IConfiguration _config;
        private readonly APIDbContext _context;
        

        public AuthService(APIDbContext context, IConfiguration config, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, ClientContext clientContext)
        {
            _config = config;
            _context = context;
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
            _clientContext = clientContext;
        }

        public async Task<ResponseViewModel> UserExist(RegisterViewModel model)
        {

            ApplicationUser existsUser = await _userManager.FindByNameAsync(model.Email);

            var response = new ResponseViewModel();

            if (existsUser != null)
            {
                response.IsSuccess = false;
                response.Message = "User Already Exist!!!";

                return response;
            }


            return new ResponseViewModel();
        }

        public async Task<ResponseViewModel> CreateUser(RegisterViewModel model)
        {
            ApplicationUser user = new ApplicationUser()
            {
                FullName = model.FullName,
                Email = model.Email.Trim(),
                UserName = model.Email.Trim()
            };

            var response = new ResponseViewModel();

            IdentityResult result = await _userManager.CreateAsync(user, model.Password.Trim());


            if (result.Succeeded)
            {
                bool roleExists = await _roleManager.RoleExistsAsync(_config["Roles:User"]);

                if (!roleExists)
                {
                    IdentityRole role = new IdentityRole(_config["Roles:User"]);
                    role.NormalizedName = _config["Roles:User"];

                    _roleManager.CreateAsync(role).Wait();
                }

                //Kullanıcıya ilgili rol ataması yapılır.
                _userManager.AddToRoleAsync(user, _config["Roles:User"]).Wait();

                response.IsSuccess = true;
                response.Message = "User Created Successfully.";
            }
            else
            {
                response.IsSuccess = false;
                response.Message = string.Format("An Error occured while trying to create User!!! ....", result.Errors.FirstOrDefault().Description);
            }

            var userOut = await _userManager.FindByNameAsync(model.Email);

            var account = new BankAccounts()
            {
                Id = Guid.Parse(userOut.Id),
                Amount = 0,
                FullName = userOut.FullName,
            };
            _clientContext.Add(account);
            _clientContext.SaveChanges();

            return response;
        }

        public async Task<ResponseViewModel> Login(LoginViewModel model)
        {
            var response = new ResponseViewModel();

            ApplicationUser user = await _userManager.FindByNameAsync(model.Email);
            if (user == null)
            {
                response.IsSuccess = false;
                response.Message = "Given email doesn't exist in database!!";
                return response;
            }

            SignInResult signInResult = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);
            if (signInResult.Succeeded == false)
            {
                response.IsSuccess = false;
                response.Message = "Given credentials are wrong!!!";
                return response;
            }

            ApplicationUser applicationUser = _context.Users.FirstOrDefault(x => x.Id == user.Id);

            AccessTokenGenerator accessTokenGenerator = new AccessTokenGenerator(_context, _config, applicationUser);

            ApplicationUserTokens userTokens = accessTokenGenerator.GetToken();

            response.IsSuccess = true;
            response.Message = "Kullanıcı giriş yaptı.";
            response.TokenInfo = new TokenInfo
            {
                Token = userTokens.Value,
                ExpireDate = userTokens.ExpireDate
            };

            return response;
        }
    }
}
