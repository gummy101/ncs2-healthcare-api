using System.ComponentModel.DataAnnotations;

namespace HealthcareApi.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public DateTime BirthDate { get; set; }
        public string UserId { get; set; }
        public string password { get; set; }
        public string Role { get; set; }
        public Cart? Cart { get; set; }
        public ICollection<UserAddress>? Address { get; set; }
        public ICollection<PaymentInfo>? PaymentInfo { get; set; }
        public User()
        {
            Address = new List<UserAddress>();
            PaymentInfo = new List<PaymentInfo>();
            CreatedDate = DateTime.Now;
            UpdatedDate = DateTime.Now;
        }
    }
}
