namespace OMG.Domain.Market.ValueObjects
{
    public class ContactInfo
    {
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }

        public ContactInfo(string phone, string email, string website)
        {
            Phone = phone;
            Email = email;
            Website = website;
        }
    }
}
