using Microsoft.EntityFrameworkCore;
using Rejestracja_konta.Data;


namespace Rejestracja_konta.Data
{
    public class MyAppDbContext : DbContext
    {
        // deklaracje DbSet<T> dla Twoich encji
        public object ApplicationUsers { get; internal set; }
    }
}

