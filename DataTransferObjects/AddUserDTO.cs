using HealthcareApi.Models;

namespace HealthcareApi.DataTransferObjects
{
    public class AddUserDTO
    {
        public string FirstName { get; set; }
        
        public string LastName { get; set; }
        
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
        public string UserId { get; set; }
        public string password { get; set; }

        public string Role { get; set; }

        public ICollection<AddUserAddressDTO>? Address { get; set; }

        public AddUserDTO()
        {
            FirstName = string.Empty;
            LastName = string.Empty;
            Email = string.Empty;
            BirthDate = DateTime.MinValue;
            Address = new List<AddUserAddressDTO>();
        }
    }
}
