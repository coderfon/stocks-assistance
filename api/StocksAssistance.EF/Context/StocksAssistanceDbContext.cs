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
            modelBuilder.Entity<Company>()
                .HasMany(c => c.Attributes)
                .WithMany(t => t.Companies)
                .UsingEntity("CompaniesToAttributes");

            modelBuilder.Entity<Company>()
                .HasMany(c => c.Tags)
                .WithMany(t => t.Companies)
                .UsingEntity("CompaniesToTags");

            modelBuilder.Entity<Company>()
                .HasMany(c => c.Logs)
                .WithMany(t => t.Companies)
                .UsingEntity("CompaniesToTags");
        }

        public DbSet<Company> Companies { get; set; }
        public DbSet<CompanyAttribute> CompanyAttributes { get; set; }
        public DbSet<CompanyTag> CompanyTags { get; set; }
        public DbSet<CompanyLog> CompanyLogs { get; set; }
    }
}
