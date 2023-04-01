using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafariGo.Core.Models
{
    public class CategoryItem
    {
        
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Cover { get; set; }
        public DateTime CreateAt { get; set; }
        public string CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public CategoryItem()
        {
            Id = Guid.NewGuid().ToString();
            CreateAt = DateTime.Now;
        }
    }
}
