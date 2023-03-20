using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafariGo.Core.Dto.Response
{
    public class AccountAccessResponse
    {
        public string Message { get; set; }
        public bool Status { get; set; }
        public object Errors { get; set; }
    }
}
