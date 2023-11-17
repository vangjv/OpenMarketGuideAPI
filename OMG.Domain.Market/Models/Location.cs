using OMG.Domain.Market.ValueObjects;

namespace OMG.Domain.Market.Models
{
    public abstract class Location
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Boundary Boundary { get; set; }
    }
}
