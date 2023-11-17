using OMG.Domain.Market.ValueObjects;

namespace OMG.Domain.Market.Models
{
    public class ThreeDModelEntity
    {
        public bool Show { get; set; }
        public string Name { get; set; }
        public string Id { get; set; }
        public string Type { get; set; }
        public ThreeDModel Model { get; set; }
        public CoordinateData Position { get; set; }
        public Orientation Orientation { get; set; }

    }
}
