namespace OMG.Domain.Market.Models
{
    public class MarketInstance
    {
        public Guid MarketInstanceId { get; private set; }
        public DateTime Date { get; private set; }
        public Guid MarketId { get; private set; }
        public List<Vendor> Vendors { get; private set; }

        public MarketInstance(Guid marketInstanceId, DateTime date, Guid marketId)
        {
            MarketInstanceId = marketInstanceId;
            Date = date;
            MarketId = marketId;
            Vendors = new List<Vendor>();
        }
    }
}
