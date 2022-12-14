namespace HealthcareApi.Models
{
    public class UserAddress
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }

        public string AddressType { get; set; }
        public User User { get; set; }
    }
}
