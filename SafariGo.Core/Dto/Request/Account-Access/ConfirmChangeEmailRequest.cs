using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SafariGo.Core.Dto.Request.Authentication;

namespace SafariGo.Core.Dto.Request
{
    public class ConfirmChangeEmailRequest:ConfirmEmailRequest
    {
        public string Email { get; set; }
       

    }
}
