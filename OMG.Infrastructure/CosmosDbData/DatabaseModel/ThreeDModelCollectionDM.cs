using OMG.Domain.Market.Models;
using OMG.Infrastructure.Interfaces.Persistence;

namespace OMG.Infrastructure.CosmosDbData.DatabaseModel
{
    public class ThreeDModelCollectionDM : ThreeDModelCollection, IMetadata
    {
        public string Type { get; } = "ThreeDModelCollection";
        public ThreeDModelCollection ToThreeDModelCollection()
        {
            var threeDModelCollection = new ThreeDModelCollection();
            threeDModelCollection.Owner = Owner;
            threeDModelCollection.Models = Models;
            threeDModelCollection.Id = Id;
            return threeDModelCollection;
        }
    }
}
