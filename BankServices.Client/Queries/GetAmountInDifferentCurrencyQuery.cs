using BankServices.Client.Models;
using MediatR;

namespace BankServices.Client.Queries
{
    public class GetAmountInDifferentCurrencyQuery :  IRequest<ForeignCurrencyModel>
    {
        public string Id { get; set; }
        public string Currency { get; set; }
    }
}
