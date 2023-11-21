using OMG.Domain.Market.ValueObjects;

namespace OMG.Domain.Market.Models
{
    public class Vendor
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string PrimaryContactName { get; set; }
        public string PrimaryContactTitle { get; set; }
        public List<string> Categories { get; set; }
        public ContactInfo ContactInfo { get; set; }
        public List<Product> Products { get; set; }
        public string? BillboardImageUrl { get; set; }
        public double? BillboardScale { get; set; }

    }
}
