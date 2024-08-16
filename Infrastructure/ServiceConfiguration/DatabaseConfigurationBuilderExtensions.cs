using Microsoft.Extensions.Configuration;

namespace Infrastructure.ServiceConfiguration
{
    public static class DatabaseConfigurationBuilderExtensions
    {
        public static IConfigurationBuilder AddDatabaseConfiguration(
            this IConfigurationBuilder builder)
        {
            try
            {
                var tempConfig = builder.Build();
                return builder.Add(new DatabaseConfigurationSource(tempConfig));
            }
            catch (Exception ex)
            {
                return builder;
            }
        }
    }
}
