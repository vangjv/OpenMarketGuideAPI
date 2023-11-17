using OMG.Infrastructure.CosmosDbData.Interfaces;
using Microsoft.Azure.Cosmos;
using OMG.Domain.Market.Models;

namespace OMG.Infrastructure.CosmosDbData.Repository
{
    public class ThreeDModelCollectionRepository : CosmosDbRepository<ThreeDModelCollection>, IThreeDModelCollectionRepository
    {
        /// <summary>
        ///     CosmosDB container name
        /// </summary>
        public override string ContainerName { get; } = "Models";

        /// <summary>
        ///     Generate Id.
        ///     e.g. "783dfe25-7ece-4f0b-885e-c0ea72135942"
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        //public override string GenerateId(ThreeDModelInfo entity) => $"{entity.MarketId}:{entity.StartDate.ToString("ddMMyyyyHHmmss")}";
        public override string GenerateId(ThreeDModelCollection entity) => entity.Owner;
        /// <summary>
        ///     Returns the value of the partition key
        /// </summary>
        /// <param name="entityId"></param>
        /// <returns></returns>
        public override PartitionKey ResolvePartitionKey(string entityId) => new PartitionKey(entityId);

        public ThreeDModelCollectionRepository(ICosmosDbContainerFactory factory) : base(factory)
        { }

    }
}
