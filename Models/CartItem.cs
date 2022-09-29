namespace HealthcareApi.Models
{
    public class CartItem
    {
        public int Id { get; set; }
        
        public Medicine Medicine { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }

        public int Quantity { get; set; }
        public Cart Cart { get; set; }

        public CartItem()
        {
            Created = DateTime.Now;
            Updated = DateTime.Now;
        }
    }
}
