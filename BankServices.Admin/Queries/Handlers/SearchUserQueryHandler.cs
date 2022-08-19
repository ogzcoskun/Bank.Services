using BankServices.Admin.Models;
using BankServices.Admin.Services;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BankServices.Admin.Queries.Handlers
{
    public class SearchUserQueryHandler : IRequestHandler<SearchUserQuery, List<UserModel>>
    {
        private readonly IAdminService _adminService;

        public SearchUserQueryHandler(IAdminService adminService)
        {
            _adminService = adminService;
        }
        public async Task<List<UserModel>> Handle(SearchUserQuery request, CancellationToken cancellationToken)
        {
            var response = await _adminService.SearchUser(request.Key);


            return response;
        }
    }
}
