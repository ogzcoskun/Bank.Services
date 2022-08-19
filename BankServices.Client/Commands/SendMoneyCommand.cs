using BankServices.Client.Models;
using MediatR;

namespace BankServices.Client.Commands
{
    public class SendMoneyCommand : IRequest<ServiceResponse>
    {
        public string UserId { get; set; }
        public string ToSendId { get; set; }
        public double Amount { get; set; }
    }
}
