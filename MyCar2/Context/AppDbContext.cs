using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MyCarApi.Models;
using System.IO;

namespace MyCarApi.Context
{
    public class AppDbContext : DbContext
    {
        public DbSet<Car> Cars { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false, true)
                .Build();

            optionsBuilder.UseSqlServer(configuration.GetConnectionString("ServerConnection"));
        }
    }
}
