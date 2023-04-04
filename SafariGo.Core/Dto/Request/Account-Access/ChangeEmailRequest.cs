using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafariGo.Core.Dto.Request
{
    public class ChangeEmailRequest
    {
        [EmailAddress,Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
