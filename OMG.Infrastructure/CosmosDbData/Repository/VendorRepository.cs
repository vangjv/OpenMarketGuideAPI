using OMG.Infrastructure.CosmosDbData.Interfaces;
using Microsoft.Azure.Cosmos;
using OMG.Infrastructure.CosmosDbData.DatabaseModel;
using OMG.Domain.Market.Models;

namespace OMG.Infrastructure.CosmosDbData.Repository
{
    public class VendorRepository : CosmosDbRepository<VendorDM>, IVendorRepository
    {
        /// <summary>
        ///     CosmosDB container name
        /// </summary>
        public override string ContainerName { get; } = "Metadata";

        /// <summary>
        ///     Generate Id.
        ///     e.g. "783dfe25-7ece-4f0b-885e-c0ea72135942"
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override string GenerateId(VendorDM entity) => Guid.NewGuid().ToString();
        /// <summary>
        ///     Returns the value of the partition key
        /// </summary>
        /// <param name="entityId"></param>
        /// <returns></returns>
        public override PartitionKey ResolvePartitionKey(string entityId) => new PartitionKey("Vendor");

        public async Task<IEnumerable<VendorDM>> GetMyVendorsAsync(string userId)
        {
            string query = @$"SELECT * FROM c WHERE c.Type = 'Vendor' AND ARRAY_CONTAINS(c.Users, {{'Id':  @userId, 'Role':'Owner'}}, true)";

            QueryDefinition queryDefinition = new QueryDefinition(query)
                                                    .WithParameter("@userId", userId);
            string queryString = queryDefinition.QueryText;

            IEnumerable<VendorDM> entities = await this.GetItemsAsync(queryString);

            return entities;
        }

        public VendorRepository(ICosmosDbContainerFactory factory) : base(factory)
        { }

    }
}
