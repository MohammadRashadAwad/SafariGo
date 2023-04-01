using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SafariGo.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafariGo.DataAccess
{
    public class ApplicationDbContext:IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<ApplicationUser>().ToTable("Users","Identity");
            builder.Entity<IdentityRole>().ToTable("Roles","Identity");
            builder.Entity<IdentityUserRole<string>>().ToTable("UserRoles","Identity");
            builder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims","Identity");
            builder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims","Identity");
            builder.Entity<IdentityUserLogin<string>>().ToTable("UserLogin","Identity");
            builder.Entity<IdentityUserToken<string>>().ToTable("UserToken","Identity");
            
            
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CategoryItem> CategoryItems { get; set; }
    }
}
