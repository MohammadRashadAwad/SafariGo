using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafariGo.Core.Dto.Response
{
    public class AuthResponse
    {
        public bool Status { get; set; }
        public object Errors { get; set; }
        public Data Data { get; set; }
     //  public IEnumerable<string> Errors { get; set; }
    }

   public class Data
    {
        public string Token { get; set; }
        public string UserId { get; set; }
        public bool IsAdmin { get; set; }
    }

    

}
