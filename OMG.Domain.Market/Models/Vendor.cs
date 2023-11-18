using OMG.Domain.Market.ValueObjects;

namespace OMG.Domain.Market.Models
{
    public class Vendor
    {
        public string VendorId { get; set; }
        public string Name { get; set; }
        public string PrimaryContactName { get; set; }
        public string PrimaryContactTitle { get; set; }
        public List<string> Category { get; set; }
        public ContactInfo ContactInfo { get; set; }
        public List<Product> Products { get; set; }

    }
}
