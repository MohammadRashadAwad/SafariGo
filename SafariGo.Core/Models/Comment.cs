using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafariGo.Core.Models
{
    public class Comment
    {
        public string Id { get; set; }

        public string ? description { get; set; }
        public string ? Image { get; set; }
        public DateTime CrateAt { get; set; }
        // navigation property

        public ApplicationUser Users { get; set; }
        public string UserId { get; set; }

        public Post Post { get; set; }
        public string PostId { get; set; }
        public Comment()
        {
            Id = Guid.NewGuid().ToString();
            CrateAt = DateTime.UtcNow;
        }

    }
}
