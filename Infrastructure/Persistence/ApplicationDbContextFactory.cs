using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace Infrastructure.Persistence
{
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        private readonly IConfiguration configuration;

        public ApplicationDbContextFactory()
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.Development.json", optional: true, reloadOnChange: true);

            builder.AddEnvironmentVariables();
            configuration = builder.Build();
        }

        public ApplicationDbContextFactory(IHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true,
                             reloadOnChange: true);

            builder.AddEnvironmentVariables();
            configuration = builder.Build();
        }

        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var connection = configuration.GetConnectionString("UserProject");
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseSqlServer(connection, o =>
            {
                o.UseNetTopologySuite();
            });

            return new(optionsBuilder.Options);
        }

        public void Migrate()
        {
            var context = CreateDbContext(null);
            context.Database.Migrate();
        }
    }
}
