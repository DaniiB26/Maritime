using MaritimeAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace MaritimeAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Ship> Ships { get; set; }
        public DbSet<Voyage> Voyages { get; set; }
        public DbSet<Port> Ports { get; set; }
        public DbSet<Country> Countries { get; set; }
    }

}