namespace OMG.Domain.Market.ValueObjects
{
    public class Boundary
    {
        public List<CoordinateData> BoundaryPositions{ get; set; }
        public bool Outline { get; set; }
        public Color Color { get; set; }
        public Color OutlineColor { get; set; }
        public double OutlineWidth { get; set; }

        public Boundary(List<CoordinateData> boundaryPositions)
        {
            BoundaryPositions = boundaryPositions;
        }
    }
}
