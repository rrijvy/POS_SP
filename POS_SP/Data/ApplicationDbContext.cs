using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using POS_SP.Models;

namespace POS_SP.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }

        public DbSet<POS_SP.Models.Brand> Brand { get; set; }

        public DbSet<POS_SP.Models.Category> Category { get; set; }

        public DbSet<POS_SP.Models.Product> Product { get; set; }

        public DbSet<POS_SP.Models.SubCategory> SubCategory { get; set; }

        public DbSet<POS_SP.Models.Sale> Sale { get; set; }

        public DbSet<POS_SP.Models.Purchase> Purchase { get; set; }

        public DbSet<POS_SP.Models.Supplier> Supplier { get; set; }
    }
}
