using BankServices.Admin.Data;
using BankServices.Admin.Models;
using BankServices.Admin.Services;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BankServices.Admin.Queries.Handlers
{
    public class GetAllBankAccountsQueryHandler : IRequestHandler<GetAllBankAccountsQuery, List<BankAccounts>>
    {
        private readonly IAdminService _adminService;

        public GetAllBankAccountsQueryHandler(IAdminService adminService)
        {
            _adminService = adminService;
        }

        public async Task<List<BankAccounts>> Handle(GetAllBankAccountsQuery request, CancellationToken cancellationToken)
        {

            var response = await _adminService.GetAllBankAccounts();

            return response;

            throw new System.NotImplementedException();
        }
    }
}
