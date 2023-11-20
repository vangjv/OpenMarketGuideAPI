namespace OMG.Domain.Vendor.Models
{
    public class Product
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<ProductImage> Images { get; set; } = new List<ProductImage>();
        public List<string> Categories { get; set; } = new List<string>();
        public double? Price { get; set; }
        public double? TaxPercentage { get; set; }
        public string Unit { get; set; }
        public bool Availability { get; set; }
        public List<Review> Reviews { get; set; } = new List<Review>();
    }
}
