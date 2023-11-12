using OMG.Domain.Market.ValueObjects;
using OMG.SharedKernel.Entities.Base;

namespace OMG.Domain.Market.Models
{
    public class Market:BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string State { get; set; }
        public CoordinateData Location { get; set; }
        public Boundary MarketBoundary { get; set; }
        public List<VendorLocation> VendorLocations { get; set; }
        public List<ThreeDModelEntity> ThreeDModelEntities { get; set; }
        public Market(string name, string description, string state)
        {
            Name = name;
            Description = description;
            State = state;
        }
        //public Market(string name, string description,string state, Location location, Boundary boundaries)
        //{
        //    Name = name;
        //    Description = description;
        //    State = state;
        //    Location = location;
        //    Boundaries = boundaries;
        //    VendorLocation = new List<VendorLocation>();
        //}
    }
}
