using BankServices.Client.Models;
using BankServices.Client.Services;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BankServices.Client.Commands.Handlers
{
    public class SendMoneyCommandHandler : IRequestHandler<SendMoneyCommand, ServiceResponse>
    {
        private readonly IMoneyTransactionService _moneyTransactionService;

        public SendMoneyCommandHandler(IMoneyTransactionService moneyTransactionService)
        {
            _moneyTransactionService = moneyTransactionService;
        }
        public async Task<ServiceResponse> Handle(SendMoneyCommand request, CancellationToken cancellationToken)
        {

            try
            {
                var model = new MoneyTransactionModel() 
                {
                    UserId = request.UserId,
                    Amount = request.Amount,
                    ToSendId = request.ToSendId,
                };

                var response = await _moneyTransactionService.SendMoney(model);

                return response;

            }catch(Exception ex)
            {
                //_logger.LogError(ex.Message);
                return new ServiceResponse() { Message = ex.Message, Success = false};
            }



            throw new System.NotImplementedException();
        }
    }
}
