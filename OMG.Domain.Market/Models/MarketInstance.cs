using OMG.Domain.Market.Enums;
using OMG.Domain.Market.SchemaVersioning;
using OMG.Domain.Market.ValueObjects;
using OMG.SharedKernel.Entities.Base;

namespace OMG.Domain.Market.Models
{
    public class MarketInstance:Market
    {
        public MarketEntityType MarketEntityType { get; set; } = MarketEntityType.Template;
        public string MarketId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public new int SchemaVersion = SchemaVersionConfig.MarketInstanceCurrentSchemaVersion;

    }
}
