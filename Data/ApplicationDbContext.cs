using InsurancePolicyAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace InsurancePolicyAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Policy> Policies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Policy>()
                .Property(p => p.CoverageAmount)
                .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<Policy>()
                .Property(p => p.Premium)
                .HasColumnType("decimal(18, 2)");
            modelBuilder.Entity<Policy>()
                .Property(p => p.Id)
                .ValueGeneratedOnAdd();
        }
    }
}
