using OMG.Infrastructure.CosmosDbData.Interfaces;
using Microsoft.Azure.Cosmos;
using OMG.Infrastructure.CosmosDbData.DatabaseModel;

namespace OMG.Infrastructure.CosmosDbData.Repository
{
    public class VendorRepository : CosmosDbRepository<VendorDM>, IVendorRepository
    {
        /// <summary>
        ///     CosmosDB container name
        /// </summary>
        public override string ContainerName { get; } = "Vendors";

        /// <summary>
        ///     Generate Id.
        ///     e.g. "783dfe25-7ece-4f0b-885e-c0ea72135942"
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override string GenerateId(VendorDM entity) => $"Guid.NewGuid()";
        /// <summary>
        ///     Returns the value of the partition key
        /// </summary>
        /// <param name="entityId"></param>
        /// <returns></returns>
        public override PartitionKey ResolvePartitionKey(string entityId) => new PartitionKey("Vendor");

        public VendorRepository(ICosmosDbContainerFactory factory) : base(factory)
        { }

    }
}
