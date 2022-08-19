using Microsoft.AspNetCore.Identity;
using System;

namespace Authentication.Models.IdentityModels
{
    public class ApplicationUserTokens : IdentityUserToken<string>
    {
        public DateTime ExpireDate { get; set; }
    }
}
