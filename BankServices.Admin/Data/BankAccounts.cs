using System;

namespace BankServices.Admin.Data
{
    public class BankAccounts
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public double Amount { get; set; }
    }
}
