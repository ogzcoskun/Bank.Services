using BankServices.Admin.Data;
using BankServices.Admin.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BankServices.Admin.Services
{
    public interface IAdminService
    {
        Task<List<BankAccounts>> GetAllBankAccounts();
        Task<ServiceResponse> AddMoney(Guid id, double amount);
        Task<List<UserModel>> SearchUser(string key);
    }
}
