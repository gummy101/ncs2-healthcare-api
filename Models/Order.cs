namespace HealthcareApi.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string OrderStatus { get; set; }
        public User User { get; set; }
        public Cart Cart { get; set; }
        public ICollection<OrderItem>? Items { get; set; }
        public PaymentInfo PaymentInfo { get; set; }
        public UserAddress BillingAddress { get; set; }
        public UserAddress ShippingAddress { get; set; }
        public Order()
        {
            Items = new List<OrderItem>();
            CreatedDate = DateTime.Now;
            UpdatedDate = DateTime.Now;
        }
    }
}
