using BankServices.Client.Data;
using BankServices.Client.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankServices.Client.Services
{
    public class MoneyTransactionService : IMoneyTransactionService
    {
        private readonly APIDbContext _context;
        private readonly IConfiguration _config;

        public MoneyTransactionService(APIDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        public async Task<ServiceResponse> SendMoney(MoneyTransactionModel model)
        {
            var response = new ServiceResponse();
            var userSend =  await _context.BankAccounts.FirstOrDefaultAsync(x => x.Id.ToString() == model.UserId);
            var userGet = await _context.BankAccounts.FirstOrDefaultAsync(x => x.Id.ToString() == model.ToSendId);

            if(!(userSend == null || userGet == null))
            {
                if(userSend.Amount >= model.Amount)
                {
                    userSend.Amount = userSend.Amount - model.Amount;
                    userGet.Amount = userGet.Amount + model.Amount;
                }
                else
                {
                    response.Success = false;
                    response.Message = "Not enough amount in bank account!!!";
                    return response;
                }
            }
            else
            {
                response.Success = false;
                response.Message = "One of the given users doesn't exist";
                return response;
            }
            _context.SaveChanges();

            response.Success = true;
            response.Message = "Money Transaction Completed";

            return response;
        }

        public async Task<ServiceResponse> GetAmount(string id)
        {
            var account = await _context.BankAccounts.FirstOrDefaultAsync(x => x.Id.ToString() == id);
            var amount = account.Amount;

            return new ServiceResponse()
            {
                Success = true,
                Message = amount.ToString()
            };
        }

        public async Task<ForeignCurrencyModel> GetAmountInDifferentCurrency(string id, string currency)
        {
            var amount = (await GetAmount(id)).Message;

            var client = new RestClient($"https://api.apilayer.com/fixer/convert?to={currency}&from=TRY&amount={amount}");
            client.Timeout = -1;

            var test = _config["APIKey"];

            var request = new RestRequest(Method.GET);
            request.AddHeader("apikey", _config["APIKey"]); // This is my free API key

            var response = client.Execute(request);

            
            var dsJsonResponse= (JsonConvert.DeserializeObject<Dictionary<string, object>>(response.Content));


            return new ForeignCurrencyModel()
            {
                AmountInTry = Double.Parse(amount),
                Currency = currency,
                AmountInForeignCurrency = Double.Parse((dsJsonResponse["result"]).ToString())

            };
        }
    }
}
