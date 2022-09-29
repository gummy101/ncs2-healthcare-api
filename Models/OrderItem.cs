namespace HealthcareApi.Models
{
    public class OrderItem
    {
        public int Id { get; set; }
        public Order Order { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public Medicine Medicine { get; set; }  

        public int Quantity { get; set; }

        public OrderItem()
        {
            Created = DateTime.Now;
            Updated = DateTime.Now;
        }
        
    }
}
