using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafariGo.Core.Dto.Request.Posts
{
    public class UpdatePostRequest
    {
     
        public string ?Description { get; set; }
        public IFormFile ? Poster { get; set; }
    }
}
