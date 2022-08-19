using BankServices.Admin.Data;
using BankServices.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankServices.Admin.Services
{
    public class AdminService : IAdminService
    {
        private readonly masterContext _context;

        public AdminService(masterContext context)
        {
            _context = context;
        }

        public async Task<List<BankAccounts>> GetAllBankAccounts()
        {
            var bankAccounts = _context.BankAccounts.ToList();

            return bankAccounts;
        }

        public async Task<ServiceResponse> AddMoney(Guid id, double amount)
        {

            var account = _context.BankAccounts.FirstOrDefault(x => x.Id == id);
            account.Amount += amount;          

            await _context.SaveChangesAsync();


            return new ServiceResponse()
            {
                Success = true,
                Message = "Amount added to given Id!"
            };
        }

        public async Task<List<UserModel>> SearchUser(string key)
        {

            var userList = _context.BankAccounts.Where(s => s.FullName.Contains(key)).ToList();


            // Normalde UserModel oluşturmak yerine diğer esas modeli döndürmek daha mantıklı ve doğru ama bir foreach de kullanmayalım mı.
            var listToSend = new List<UserModel>();
            foreach (var user in userList)
            {
                listToSend.Add(new UserModel()
                {
                    Amount = user.Amount,
                    Id = user.Id.ToString(),
                    Name = user.FullName

                });
            }

            return listToSend;
        }
    }
}
