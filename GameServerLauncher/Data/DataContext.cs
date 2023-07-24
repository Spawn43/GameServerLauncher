using Microsoft.EntityFrameworkCore;
using GameServerLauncher.Models;

namespace GameServerLauncher.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<ServerStatistic> ServerStatistics { get; set; } 

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ServerStatistic>()
                .HasKey(pc => new { pc.Time });
        }

    }
}
