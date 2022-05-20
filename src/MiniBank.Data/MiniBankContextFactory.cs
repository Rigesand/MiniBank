using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace MiniBank.Data
{
    public class MiniBankContextFactory : IDesignTimeDbContextFactory<MiniBankDbContext>
    {
        public MiniBankDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<MiniBankDbContext>();
            optionsBuilder.UseNpgsql(connectionString:"Host=storage;Port=5432;Database=MiniBankDB;Username=postgres;Password=1234567")
                .UseSnakeCaseNamingConvention();

            return new MiniBankDbContext(optionsBuilder.Options);
        }
    }
}