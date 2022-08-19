using BankServices.Client.Models;
using BankServices.Client.Services;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BankServices.Client.Queries.Handlers
{
    public class GetAmountQueryHandler : IRequestHandler<GetAmountQuery, ServiceResponse>
    {
        private readonly IMoneyTransactionService _moneyService;

        public GetAmountQueryHandler(IMoneyTransactionService moneyService)
        {
            _moneyService = moneyService;
        }

        public async Task<ServiceResponse> Handle(GetAmountQuery request, CancellationToken cancellationToken)
        {
            var response = await _moneyService.GetAmount(request.Id);

            return response;
        }
    }
}
