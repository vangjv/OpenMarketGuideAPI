using OMG.Domain.Market.ValueObjects;

namespace OMG.Domain.Market.Models
{
    public class Vendor
    {
        public Guid VendorId { get; private set; }
        public string Name { get; private set; }
        public string Category { get; private set; }
        public ContactInfo ContactInfo { get; private set; }

        public Vendor(Guid vendorId, string name, string category, ContactInfo contactInfo)
        {
            VendorId = vendorId;
            Name = name;
            Category = category;
            ContactInfo = contactInfo;
        }

        // Additional methods and behaviors
    }
}
