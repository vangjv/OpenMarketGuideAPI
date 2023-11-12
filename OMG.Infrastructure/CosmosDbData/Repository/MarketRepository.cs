using OMG.Infrastructure.CosmosDbData.Interfaces;
using Microsoft.Azure.Cosmos;
using OMG.Domain.Market.Models;

namespace OMG.Infrastructure.CosmosDbData.Repository
{
    public class MarketRepository : CosmosDbRepository<Market>, IMarketRepository
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
        //public override string GenerateId(Market entity) => $"{entity.State}:{Guid.NewGuid()}";
        public override string GenerateId(Market entity) => $"{Guid.NewGuid()}";
        /// <summary>
        ///     Returns the value of the partition key
        /// </summary>
        /// <param name="entityId"></param>
        /// <returns></returns>
        public override PartitionKey ResolvePartitionKey(string entityId) => new PartitionKey(entityId.Split(':')[0]);

        public MarketRepository(ICosmosDbContainerFactory factory) : base(factory)
        { }

        // Use Cosmos DB Parameterized Query to avoid SQL Injection.
        // Get by Category is also an example of single partition read, where get by title will be a cross partition read
        public async Task<IEnumerable<Market>> GetItemsAsyncByState(string state)
        {
            List<Market> results = new List<Market>();
            string query = @$"SELECT * FROM c WHERE c.state = @state";

            QueryDefinition queryDefinition = new QueryDefinition(query)
                                                    .WithParameter("@state", state);
            string queryString = queryDefinition.QueryText;

            IEnumerable<Market> entities = await this.GetItemsAsync(queryString);

            return results;
        }

    }
}
