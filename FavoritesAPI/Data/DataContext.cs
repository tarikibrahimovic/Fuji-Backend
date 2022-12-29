using FavoritesAPI.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace FavoritesAPI.Data
{
    public class DataContext : DbContext
    {
        protected readonly IConfiguration Configuration;

        public DataContext(DbContextOptions<DataContext> options, IConfiguration configuration) : base(options)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"), builder =>
            {
                builder.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
            });
            base.OnConfiguring(optionsBuilder);
        }


        public DbSet<Favorites> Favorites => Set<Favorites>();
        public DbSet<User_Favorites> User_Favorites => Set<User_Favorites>();
    }
}
