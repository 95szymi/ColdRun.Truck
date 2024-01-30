using Microsoft.EntityFrameworkCore;

namespace ColdRun.Truck.Infrastructure
{
    public class TruckDbContext : DbContext
    {
        public TruckDbContext() : this(
            new DbContextOptionsBuilder<TruckDbContext>()
                .UseInMemoryDatabase("InMemoryDbForTesting")
                .Options)
        {
        }

        public TruckDbContext(DbContextOptions<TruckDbContext> options)
            : base(options)
        {
        }

        public DbSet<Domain.Truck> Trucks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Domain.Truck>()
                        .Property(t => t.TruckStatus)
                        .HasConversion<string>();
        }
    }
}