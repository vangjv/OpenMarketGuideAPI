namespace OMG.API.Models
{
    public class AssignVendorRequest
    {
        public string VendorLocationId { get; set; }
        public string Name { get; set; }
        public List<string> Categories { get; set; }
    }
}
