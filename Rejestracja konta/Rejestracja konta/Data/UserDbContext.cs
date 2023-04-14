using Microsoft.EntityFrameworkCore;
using Rejestracja_konta.Models;
using Rejestracja_konta.Services;

namespace Rejestracja_konta.Data
{
    public class ApplicationUserDbContext : DbContext
    {
        public ApplicationUserDbContext(DbContextOptions<ApplicationUserDbContext> options) : base(options)
        {

        }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApplicationUser>()
                .HasIndex(u => u.Email)
                .IsUnique();
        }
    }
}
