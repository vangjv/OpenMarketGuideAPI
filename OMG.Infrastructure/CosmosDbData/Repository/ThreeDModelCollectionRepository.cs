﻿using OMG.Infrastructure.CosmosDbData.Interfaces;
using Microsoft.Azure.Cosmos;
using OMG.Domain.Market.Models;
using OMG.Infrastructure.CosmosDbData.DatabaseModel;

namespace OMG.Infrastructure.CosmosDbData.Repository
{
    public class ThreeDModelCollectionRepository : CosmosDbRepository<ThreeDModelCollectionDM>, IThreeDModelCollectionRepository
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
        //public override string GenerateId(ThreeDModelInfo entity) => $"{entity.MarketId}:{entity.StartDate.ToString("ddMMyyyyHHmmss")}";
        public override string GenerateId(ThreeDModelCollectionDM entity) => entity.Owner;
        /// <summary>
        ///     Returns the value of the partition key
        /// </summary>
        /// <param name="entityId"></param>
        /// <returns></returns>
        public override PartitionKey ResolvePartitionKey(string entityId) => new PartitionKey("ThreeDModelCollection");

        public ThreeDModelCollectionRepository(ICosmosDbContainerFactory factory) : base(factory)
        { }

    }
}
