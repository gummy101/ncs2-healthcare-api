using HealthcareApi.Models;

namespace HealthcareApi.DataTransferObjects
{
    public class UserResponseDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }  
        public int cartid { get; set;}

        public int Id { get; set; } 

        public string Role { get; set; }

        public string Token { get; set; }
        public UserResponseDTO(string fname, string lname, string email)
        {
            FirstName = fname;
            LastName = lname;
            Email = email;
        }
    }
}
