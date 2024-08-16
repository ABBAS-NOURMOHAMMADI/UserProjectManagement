using Microsoft.Extensions.Configuration;

namespace Infrastructure.ServiceConfiguration
{
    public class DatabaseConfigurationSource : IConfigurationSource
    {
        public DatabaseConfigurationSource(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            return new DatabaseConfigurationProvider(Configuration);
        }
    }
}
