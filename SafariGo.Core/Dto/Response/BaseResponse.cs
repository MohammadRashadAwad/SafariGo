using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafariGo.Core.Dto.Response
{
    public class BaseResponse
    {
        public bool Status { get; set; }
        public object Error { get; set; }
        public object Data { get; set; }
        public string Message { get; set; }
    }
}
