using OMG.Domain.Vendor.ValueObjects;
using OMG.SharedKernel.Entities.Base;

namespace OMG.Domain.Vendor.Models
{
    public class Vendor:BaseEntity
    {
        public string Name { get; set; }
        public string PrimaryContactName { get; set; }
        public string PrimaryContactTitle { get; set; }
        public List<string> Categories { get; set; } = new List<string>();
        public ContactInfo ContactInfo { get; set; }
        public string? BillboardImageUrl { get; set; }
        public double? BillboardScale { get; set; }
        public List<Product> Products { get; set; } = new List<Product>();
        public List<VendorUser> Users { get; set; } = new List<VendorUser>();
        public List<Review> Reviews { get; set; } = new List<Review>();

    }
}
