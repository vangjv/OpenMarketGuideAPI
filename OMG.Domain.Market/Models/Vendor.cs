using OMG.Domain.Market.ValueObjects;

namespace OMG.Domain.Market.Models
{
    public class Vendor
    {
        public string VendorId { get; set; }
        public string Name { get; set; }
        public string PrimaryContactName { get; set; }
        public string PrimaryContactTitle { get; set; }
        public string Category { get; set; }
        public ContactInfo ContactInfo { get; set; }

        public Vendor(string vendorId, string name, string category, ContactInfo contactInfo)
        {
            VendorId = vendorId;
            Name = name;
            Category = category;
            ContactInfo = contactInfo;
        }

    }
}
