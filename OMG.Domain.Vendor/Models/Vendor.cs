using OMG.Domain.Vendor.ValueObjects;
using OMG.SharedKernel.Entities.Base;

namespace OMG.Domain.Vendor.Models
{
    public class Vendor:BaseEntity
    {
        public string Name { get; set; }
        public string PrimaryContactName { get; set; }
        public string PrimaryContactTitle { get; set; }
        public List<string> Categories { get; set; }
        public ContactInfo ContactInfo { get; set; }
        public List<Product> Products { get; set; }

    }
}
