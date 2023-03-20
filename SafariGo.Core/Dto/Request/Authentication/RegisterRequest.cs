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
        [StringLength(25, MinimumLength = 2)]
        public string FirstName { get; set; }
        [StringLength(25, MinimumLength = 2)]
        public string LastName { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [RegularExpression(@"^07[789]\d{7}$", ErrorMessage = "Invalid phone number")]
        public string Phone { get; set; }
        public string Password { get; set; }
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}
