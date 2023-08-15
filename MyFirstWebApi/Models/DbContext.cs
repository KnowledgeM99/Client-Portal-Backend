using Microsoft.EntityFrameworkCore;

namespace MyFirstWebApi.Models
{
    public class AppDbContext : DbContext
    {
        public DbSet<Client> Clients { get; set; }
        public DbSet<Policy> Policies { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>()
                .HasKey(c => c.IdNumber);

            modelBuilder.Entity<Policy>()
                .HasKey(p => p.PolicyNumber);

            modelBuilder.Entity<Policy>()
                .HasOne(p => p.Client)
                .WithMany(c => c.Policies)
                .HasForeignKey(p => p.ClientId);
        }
    }
}
