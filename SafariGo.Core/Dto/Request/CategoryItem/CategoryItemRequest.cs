using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafariGo.Core.Dto.Request.CategoryItem
{
    public class CategoryItemRequest
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public IFormFile Cover { get; set; }
        public string CategoryId { get; set; }
    }
}
