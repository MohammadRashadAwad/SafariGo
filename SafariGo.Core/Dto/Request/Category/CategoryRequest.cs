using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafariGo.Core.Dto.Request.Category
{
    public class CategoryRequest
    {
        [StringLength(50,MinimumLength =2)]
        public string Name { get; set; }
    }
}
