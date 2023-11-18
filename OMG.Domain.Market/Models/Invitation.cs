using Newtonsoft.Json;

namespace OMG.Domain.Market.Models
{
    public class Invitation
    {
        public string Id { get; set; }

        public string MarketInstanceId { get; set; }

        public string VendorEmail { get; set; }

        public string VendorLocationName { get; set; }

        public string Status { get; set; }

        public DateTime ExpirationDate { get; set; }

        public string Token { get; set; }
    }
}
