﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafariGo.Core.Dto.Request.Posts
{
    public class CommentRequest
    {
        [Required]
        public string UserId { get; set; }
        [Required]
        public string PostId { get; set; }
        public string? description { get; set; }
        public IFormFile? Image { get; set; }
    }
}