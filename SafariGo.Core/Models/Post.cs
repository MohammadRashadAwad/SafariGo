using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafariGo.Core.Models
{
    public class Post
    {
        public string Id { get; set; }
        public string ? description { get; set; }
        public string ? Poster { get; set; }
        public DateTime CreateAt { get; set; }
        // Navigation property
        public ApplicationUser Users { get; set; }
        public string UserId { get; set; }
        [ForeignKey("PostId")]
        public virtual ICollection<Comment> Comments { get; set; }
        [ForeignKey("PostId")]
        public virtual ICollection<Like> Likes { get; set; } 
        public Post()
        {
            Id = Guid.NewGuid().ToString();
            CreateAt = DateTime.UtcNow;
        }
    }
}
