using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafariGo.Core.Models
{
    public class Like
    {
        public string LikeId { get; set; }
        public string UserId { get; set; }
        public string PostId { get; set; }
        public ApplicationUser Users { get; set; }

        public Like()
        {
            LikeId = Guid.NewGuid().ToString();
        }
    }
}
