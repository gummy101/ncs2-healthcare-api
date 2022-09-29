using System.ComponentModel.DataAnnotations;

namespace HealthcareApi.Models
{
    public class Cart
    {       
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public ICollection<CartItem>? CartItems { get; set; }

        public Cart()
        {
            CartItems = new List<CartItem>();
            Created = DateTime.Now;
            Updated = DateTime.Now;
        }
    }
}
