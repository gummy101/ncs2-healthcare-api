using HealthcareApi.Models;

namespace HealthcareApi.DataTransferObjects
{
    public class CartResponseDTO
    {
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        //public int UserId { get; set; }
        public UserResponseDTO? User { get; set; }
        public ICollection<CartItemDTO>? CartItems { get; set; }
        public CartResponseDTO(int id, DateTime created,DateTime updated)
        {
            Id = id;
            Created = created;
            Updated = updated; 
            CartItems = new List<CartItemDTO>();
        }
    }
}
