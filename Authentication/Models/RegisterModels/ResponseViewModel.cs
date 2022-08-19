using Authentication.Models.IdentityModels;

namespace Authentication.Models.RegisterModels
{
    public class ResponseViewModel
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public TokenInfo TokenInfo { get; set; }
    }
}
