namespace OMG.Domain.Vendor.Models
{
    public class Review
    {
        public string ReviewID { get; set; }
        public string VendorID { get; set; }
        public string? ReviewerID { get; set; } // Nullable if not tracking reviewers
        public int Rating { get; set; }
        public string ReviewText { get; set; }
        public DateTime ReviewDate { get; set; }
    }
}
