using OMG.Domain.Market.Enums;
using OMG.Domain.Market.ValueObjects;
using OMG.SharedKernel.Entities.Base;

namespace OMG.Domain.Market.Models
{
    public class MarketInstance:BaseEntity
    {
        [System.Text.Json.Serialization.JsonConverter(typeof(System.Text.Json.Serialization.JsonStringEnumConverter))]
        public MarketEntityType MarketEntityType { get; } = MarketEntityType.Template;
        public string MarketId { get; set; }
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }
        public CoordinateData Location { get; set; }
        public Boundary MarketBoundary { get; set; }
        public List<VendorLocation> VendorLocations { get; set; }
        public List<ThreeDModelEntity> ThreeDModelEntities { get; set; }
        public List<Vendor> Vendors { get; set; }

    }
}
