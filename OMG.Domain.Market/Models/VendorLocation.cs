using OMG.Domain.Market.ValueObjects;

namespace OMG.Domain.Market.Models
{
    public class VendorLocation
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsAvailable { get; set; }
        public Boundary Boundary { get; set; }
    }
}
