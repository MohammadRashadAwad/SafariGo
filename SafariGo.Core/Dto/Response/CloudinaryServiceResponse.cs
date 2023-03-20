using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafariGo.Core.Dto.Response
{
    public class CloudinaryServiceResponse
    {
        public bool Status { get; set; }
        public string Url { get; set; }
        public string Message { get; set; }
    }
}
