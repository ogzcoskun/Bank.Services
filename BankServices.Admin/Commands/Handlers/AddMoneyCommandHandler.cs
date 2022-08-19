using BankServices.Admin.Models;
using BankServices.Admin.Services;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BankServices.Admin.Commands.Handlers
{
    public class AddMoneyCommandHandler : IRequestHandler<AddMoneyCommand, ServiceResponse>
    {
        private readonly IAdminService _adminService;

        public AddMoneyCommandHandler(IAdminService adminService)
        {
            _adminService = adminService;
        }
        public async  Task<ServiceResponse> Handle(AddMoneyCommand request, CancellationToken cancellationToken)
        {

            var response = await _adminService.AddMoney(request.Id, request.Amount);

            return response;
        }
    }
}
