using Microsoft.EntityFrameworkCore;
using StocksAssistance.EF.Models;
using Attribute = StocksAssistance.EF.Models.Attribute;

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
                .WithOne(a => a.Company).HasForeignKey(a => a.CompanyId);

            modelBuilder.Entity<Company>()
                .HasMany(c => c.Tags)
                .WithMany(t => t.Companies)
                .UsingEntity("CompaniesTags");

            modelBuilder.Entity<Company>()
                .HasMany(c => c.Logs)
                .WithOne(l => l.Company).HasForeignKey(l => l.CompanyId);

            modelBuilder.Entity<Sector>()
                .HasMany(s => s.Companies)
                .WithOne(c => c.Sector).HasForeignKey(c => c.SectorId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Sector>()
                .HasMany(s => s.Industries)
                .WithOne(c => c.Sector).HasForeignKey(c => c.SectorId);

            modelBuilder.Entity<Industry>()
                .HasMany(s => s.Companies)
                .WithOne(c => c.Industry).HasForeignKey(c => c.IndustryId)
                .OnDelete(DeleteBehavior.Restrict);
        }

        public DbSet<Company> Companies { get; set; }
        public DbSet<Attribute> Attributes { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Log> Logs { get; set; }
        public DbSet<Industry> Industries { get; set; }
        public DbSet<Sector> Sectors { get; set; }
    }
}
