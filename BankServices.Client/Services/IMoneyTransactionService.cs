using BankServices.Client.Models;
using System.Threading.Tasks;

namespace BankServices.Client.Services
{
    public interface IMoneyTransactionService
    {
        Task<ServiceResponse> SendMoney(MoneyTransactionModel model);
        Task<ServiceResponse> GetAmount(string id);
        Task<ForeignCurrencyModel> GetAmountInDifferentCurrency(string id, string currency);
    }
}
