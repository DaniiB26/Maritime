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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Voyage>()
                .HasOne(v => v.DeparturePort)
                .WithMany()
                .HasForeignKey(v => v.DeparturePortId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Voyage>()
                .HasOne(v => v.ArrivalPort)
                .WithMany()
                .HasForeignKey(v => v.ArrivalPortId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Voyage>()
                .HasOne(v => v.Ship)
                .WithMany()
                .HasForeignKey(v => v.ShipId)
                .OnDelete(DeleteBehavior.Cascade);
        }

    }

}