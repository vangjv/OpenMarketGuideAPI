using OMG.Infrastructure.AppSettings;
using OMG.Infrastructure.CosmosDbData.Interfaces;
using OMG.Infrastructure.CosmosDbData.Repository;
using OMG.Infrastructure.Extensions;

namespace OMG.API.Config
{
    public static class DatabaseConfig
    {
        /// <summary>
        ///     Setup Cosmos DB
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void SetupCosmosDb(this IServiceCollection services, IConfiguration configuration)
        {
            // Bind database-related bindings
            CosmosDbSettings cosmosDbConfig = configuration.GetSection("OMGDatabase").Get<CosmosDbSettings>();

            // register CosmosDB client and data repositories
            services.AddCosmosDb(cosmosDbConfig.EndpointUrl,
                                 cosmosDbConfig.PrimaryKey,
                                 cosmosDbConfig.DatabaseName,
                                 cosmosDbConfig.Containers);

            services.AddScoped<IMarketRepository, MarketRepository>();
        }
    }
}
