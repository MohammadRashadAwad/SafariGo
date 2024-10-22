﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafariGo.Core.Dto.Request.CategoryItem
{
    public class CategoryItemRequest
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public IFormFile Cover { get; set; }
        [Required]
        public string CategoryId { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string Map { get; set; }
    }
}
