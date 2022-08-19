using System;

namespace Authentication.Models.IdentityModels
{
    public class TokenInfo 
    {
        public string Token { get; set; }
        public DateTime ExpireDate { get; set; }
    }
}
