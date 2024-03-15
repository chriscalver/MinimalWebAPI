global using Microsoft.EntityFrameworkCore;
using MinimalWebAPI;

namespace MinimalAPITutorial
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<Restaurant> Restaurants => Set<Restaurant>();
    }
}