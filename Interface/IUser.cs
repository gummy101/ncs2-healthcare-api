using HealthcareApi.Models;
using HealthcareApi.DataTransferObjects;

namespace HealthcareApi.Interface
{
    public interface IUser
    {
        public List<User> GetAllUsers();
        public User? GetUserById(int id);
        public User?GetUserByUserName(string userName);
  
        public User? AddUser(User user);
        public User AddUser(AddUserDTO user);
        public User? UpdateUser(User user);
        public User? DeleteUserById(int id);

        public User? AddNewAddress(AddUserAddressDTO addr);

    }
}
