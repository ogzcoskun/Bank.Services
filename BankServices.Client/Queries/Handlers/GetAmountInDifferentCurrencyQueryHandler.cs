using BankServices.Client.Models;
using BankServices.Client.Services;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BankServices.Client.Queries.Handlers
{
    public class GetAmountInDifferentCurrencyQueryHandler : IRequestHandler<GetAmountInDifferentCurrencyQuery, ForeignCurrencyModel>
    {
        private readonly IMoneyTransactionService _service;

        public GetAmountInDifferentCurrencyQueryHandler(IMoneyTransactionService service)
        {
            _service = service;
        }
        public async Task<ForeignCurrencyModel> Handle(GetAmountInDifferentCurrencyQuery request, CancellationToken cancellationToken)
        {

            var response = await _service.GetAmountInDifferentCurrency(request.Id, request.Currency);

            return response;
        }
    }
}
