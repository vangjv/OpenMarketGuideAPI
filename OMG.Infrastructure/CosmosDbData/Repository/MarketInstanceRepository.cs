using OMG.Infrastructure.CosmosDbData.Interfaces;
using Microsoft.Azure.Cosmos;
using OMG.Domain.Market.Models;

namespace OMG.Infrastructure.CosmosDbData.Repository
{
    public class MarketInstanceRepository : CosmosDbRepository<MarketInstance>, IMarketInstanceRepository
    {
        /// <summary>
        ///     CosmosDB container name
        /// </summary>
        public override string ContainerName { get; } = "Markets";

        /// <summary>
        ///     Generate Id.
        ///     e.g. "783dfe25-7ece-4f0b-885e-c0ea72135942"
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override string GenerateId(MarketInstance entity) => $"{entity.MarketId}:{entity.StartDate.ToString("ddMMyyyyHHmm")}";
        //public override string GenerateId(Market entity) => $"{Guid.NewGuid()}";
        /// <summary>
        ///     Returns the value of the partition key
        /// </summary>
        /// <param name="entityId"></param>
        /// <returns></returns>
        public override PartitionKey ResolvePartitionKey(string entityId) => new PartitionKey(entityId.Split(':')[0]);

        public MarketInstanceRepository(ICosmosDbContainerFactory factory) : base(factory)
        { }

        // Use Cosmos DB Parameterized Query to avoid SQL Injection.
        // Get by Category is also an example of single partition read, where get by title will be a cross partition read
        public async Task<IEnumerable<MarketInstance>> GetMarketInstancesByMarketIdAsync(string marketId)
        {
            string query = @$"SELECT * FROM c WHERE c.marketId = @marketId AND c.marketEntityType = 'Instance'";

            QueryDefinition queryDefinition = new QueryDefinition(query)
                                                    .WithParameter("@marketId", marketId);
            string queryString = queryDefinition.QueryText;

            IEnumerable<MarketInstance> entities = await this.GetItemsAsync(queryString);

            return entities;
        }

    }
}
