using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafariGo.Core.Dto.Request
{
    public class ChangePhoneRequest
    {
        [RegularExpression(@"^07[789]\d{7}$", ErrorMessage = "Invalid phone number")]
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
    }
}
