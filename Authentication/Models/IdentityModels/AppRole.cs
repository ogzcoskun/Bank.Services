using Microsoft.AspNetCore.Identity;
using System;

namespace BankServices.Authentication.Models.IdentityModels
{
    public class AppRole : IdentityRole<int>
    {
        public DateTime OlusturulmaTarihi { get; set; }
    }
}
