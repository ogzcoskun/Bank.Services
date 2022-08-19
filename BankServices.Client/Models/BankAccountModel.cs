using System;

namespace BankServices.Client.Models
{
    public class BankAccountModel
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public double Amount { get; set; }

    }
}
