using BankServices.Client.Models;
using MediatR;

namespace BankServices.Client.Queries
{
    public class GetAmountQuery : IRequest<ServiceResponse>
    {
        public string Id { get; set; }
    }
}
