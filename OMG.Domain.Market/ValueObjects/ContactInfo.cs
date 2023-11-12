namespace OMG.Domain.Market.ValueObjects
{
    public class ContactInfo
    {
        public string Phone { get; private set; }
        public string Email { get; private set; }
        public string Website { get; private set; }

        public ContactInfo(string phone, string email, string website)
        {
            Phone = phone;
            Email = email;
            Website = website;
        }
    }
}
