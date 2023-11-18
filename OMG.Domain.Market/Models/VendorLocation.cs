using OMG.Domain.Market.ValueObjects;

namespace OMG.Domain.Market.Models
{
    public class VendorLocation :Location
    {
        public bool IsAvailable { get; set; }
        public Vendor? AssignedVendor { get; set; }
    }
}
