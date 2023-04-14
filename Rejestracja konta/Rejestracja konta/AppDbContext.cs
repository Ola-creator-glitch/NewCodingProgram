using Microsoft.EntityFrameworkCore;
using Rejestracja_konta.Models;


namespace Rejestracja_konta.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<PublicUser> PublicUsers { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }
    }

    public class PublicUser
    {
    }
}

