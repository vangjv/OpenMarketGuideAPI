namespace OMG.Domain.Market.Models
{
    public class VendorLocation :Location
    {
        public bool IsAvailable { get; set; } = true;
        public string AssignedVendorId { get; set; }
    }
}
