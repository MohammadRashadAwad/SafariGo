using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafariGo.Core.Models
{
    public class ApplicationUser:IdentityUser
    {
        [MaxLength(25),MinLength(2)]
        public string FirstName { get; set; }

        [MaxLength(25), MinLength(2)]
        public string LastName { get; set; }

        public string ? CoverPic { get; set; }
        public string ? ProfilePic { get; set; }
        [MaxLength(256)]
        public string ? Bio { get; set; }

        // Navigation property
        [ForeignKey("UserId")]
        public virtual ICollection<Post> Posts { get; set; }
        [ForeignKey("UserId")]
        public virtual ICollection<Comment> Comments { get; set; }
    }
}
