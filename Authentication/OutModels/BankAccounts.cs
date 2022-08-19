using System;
using System.Collections.Generic;

namespace BankServices.Authentication.OutModels
{
    public partial class BankAccounts
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public double Amount { get; set; }
    }
}
