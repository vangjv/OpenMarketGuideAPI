namespace OMG.Domain.Market.Models
{
    public class Product
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<string> Category { get; set; }
        public double Price { get; set; }
        public double TaxPercentage { get; set; }
        public string Unit { get; set; }
        public bool Availability { get; set; }
    }
}
