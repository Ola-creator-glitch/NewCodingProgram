using Microsoft.EntityFrameworkCore;
using Rejestracja_konta.Models;


namespace Rejestracja_konta.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<RegisteredUser> PublicUsers { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RegisteredUser>()
                .HasKey(u => u.Id);

            modelBuilder.Entity<RegisteredUser>()
                .Property(u => u.FirstName)
                .IsRequired();

            modelBuilder.Entity<RegisteredUser>()
                .Property(u => u.LastName)
                .IsRequired();

            modelBuilder.Entity<RegisteredUser>()
                .Property(u => u.PasswordHash)
                .IsRequired();

            modelBuilder.Entity<RegisteredUser>()
                .Property(u => u.PESEL)
                .IsRequired();

            modelBuilder.Entity<RegisteredUser>()
                .Property(u => u.Email)
                .IsRequired();

            modelBuilder.Entity<RegisteredUser>()
                .Property(u => u.PhoneNumber)
                .IsRequired();

            modelBuilder.Entity<RegisteredUser>()
                .Property(u => u.Salt)
                .IsRequired();

            modelBuilder.Entity<RegisteredUser>()
                .Property(u => u.Age)
                .IsRequired(false);

            modelBuilder.Entity<RegisteredUser>()
                .Property(u => u.AverageElectricityUsage)
                .IsRequired(false);
        }
    }

    public class RegisteredUser
    {
        internal object FirstName;
        internal object LastName;
        internal object PasswordHash;
        internal object PESEL;
        internal object Email;
        internal object PhoneNumber;
        internal object Salt;
        internal object Age;
        internal object AverageElectricityUsage;

        public object? Id { get; internal set; }
    }
}
