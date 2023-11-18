namespace OMG.API.Models
{
    public class MarketInstanceRequest
    {
        public string MarketId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
