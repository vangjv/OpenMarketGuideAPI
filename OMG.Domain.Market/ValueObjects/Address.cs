namespace OMG.Domain.Market.ValueObjects
{
    public class Address
    {
        public string Street { get; }
        public string Street2 { get; }
        public string City { get; }
        public string State { get; }
        public string ZipCode { get; }

        public Address(string street, string city, string state, string zipCode)
        {
            if (string.IsNullOrWhiteSpace(street)) throw new ArgumentException("Street cannot be empty", nameof(street));
            if (string.IsNullOrWhiteSpace(city)) throw new ArgumentException("City cannot be empty", nameof(city));
            if (string.IsNullOrWhiteSpace(state)) throw new ArgumentException("State cannot be empty", nameof(state));
            if (string.IsNullOrWhiteSpace(zipCode)) throw new ArgumentException("Zip code cannot be empty", nameof(zipCode));

            Street = street;
            City = city;
            State = ValidateState(state);
            ZipCode = ValidateZipCode(zipCode);
        }

        private string ValidateState(string state)
        {
            // Implement state validation logic, potentially against a list of known U.S. states.
            return state;
        }

        private string ValidateZipCode(string zipCode)
        {
            // Implement ZIP code validation logic, ensure it's in a valid format
            if (!System.Text.RegularExpressions.Regex.IsMatch(zipCode, @"^\d{5}(?:[-\s]\d{4})?$"))
            {
                throw new ArgumentException("Invalid ZIP code format", nameof(zipCode));
            }
            return zipCode;
        }

        // Override equality members if needed
        public override bool Equals(object obj)
        {
            return obj is Address address &&
                   Street == address.Street &&
                   Street2 == address.Street2 &&
                   City == address.City &&
                   State == address.State &&
                   ZipCode == address.ZipCode;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Street, City, State, ZipCode);
        }

        public override string ToString()
        {
            return $"{Street}, {City}, {State} {ZipCode}";
        }

        // Additional methods such as for formatting or comparison might be added here
    }

}
