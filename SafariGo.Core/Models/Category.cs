using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafariGo.Core.Models
{
    public class Category
    {
       
        public string Id { get; set; }
        [MaxLength(50),MinLength(2)]
        public string Name { get; set; }
        public DateTime CreateAt { get; set; }
        public virtual ICollection<CategoryItem> ? CategoryItems { get; set; }

        public Category()
        {
            Id = Guid.NewGuid().ToString();
            CreateAt = DateTime.Now;
        }
    }
}
