using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafariGo.Core.Dto.Request.Authentication
{
    public class RegisterRequest
    {
        [Required,StringLength(25, MinimumLength = 2)]
        public string FirstName { get; set; }
        [Required,StringLength(25, MinimumLength = 2)]
        public string LastName { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [RegularExpression(@"^07[789]\d{7}$", ErrorMessage = "Invalid phone number")]
        public string Phone { get; set; }
        [Required]
        public string Password { get; set; }
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}
