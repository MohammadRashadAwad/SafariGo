using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafariGo.Core.Dto.Request
{
    public class UpdateNameRequest
    {
        [StringLength(25,MinimumLength =2)]
        public string ? FirstName { get; set; }
        [StringLength(25, MinimumLength = 2)]
        public string ? LastName { get; set; }
       
    }
}
