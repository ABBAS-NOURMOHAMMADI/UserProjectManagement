using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.ServiceConfiguration
{
    public class DatabaseConfigurationProvider : ConfigurationProvider
    {
        public DatabaseConfigurationProvider(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public override void Load()
        {
            try
            {
                using var context = new ApplicationDbContext(CreateContextOptions());
            }
            catch (System.Exception)
            {
            }

        }

        private DbContextOptions<ApplicationDbContext> CreateContextOptions()
        {
            var connectionString = Configuration
                                   .GetConnectionString("DefaultConnection");

            return new DbContextOptionsBuilder<ApplicationDbContext>()
                       .UseSqlServer(connectionString)
                       .Options;
        }
    }
}
