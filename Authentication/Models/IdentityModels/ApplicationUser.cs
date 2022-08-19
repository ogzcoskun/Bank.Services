using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Authentication.Models.IdentityModels
{
    public class ApplicationUser :  IdentityUser
    {
        [Required]
        [StringLength(60)]
        public string FullName { get; set; }
    }
}
