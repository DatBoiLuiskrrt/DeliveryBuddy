using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Models
{
    public class LocationContext : DbContext
    {
        public LocationContext(DbContextOptions<LocationContext> options)
            : base(options)
        {
        }

        public DbSet<Location> Location { get; set; } = null!;

    }
}
