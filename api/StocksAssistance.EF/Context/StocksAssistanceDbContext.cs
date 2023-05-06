using Microsoft.EntityFrameworkCore;
using StocksAssistance.EF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StocksAssistance.EF.Context
{
    public class StocksAssistanceDbContext : DbContext
    {
        public StocksAssistanceDbContext(DbContextOptions<StocksAssistanceDbContext> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Company>(entity => entity.HasMany(c => c.Attributes).
            WithOne(t => t.Company).HasForeignKey(t => t.CompanyId));

            modelBuilder.Entity<Company>(entity => entity.HasMany(c => c.Tags).
            WithOne(t => t.Company).HasForeignKey(t => t.CompanyId));
        }

        public DbSet<Company> Companies { get; set; }
        public DbSet<CompanyAttribute> CompanyAttributes { get; set; }
        public DbSet<CompanyTag> CompanyTags { get; set; }
    }
}
