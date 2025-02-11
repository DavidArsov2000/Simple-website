using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.DatabaseContext
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Oseba> Osebe { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
    }
}
