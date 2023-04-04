using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafariGo.Core.Dto.Request.Profile_Setting
{
    public class UpdateBioRequest
    {
        [Required,StringLength(256,MinimumLength =1)]
        public string Bio { get; set; }
    }
}
