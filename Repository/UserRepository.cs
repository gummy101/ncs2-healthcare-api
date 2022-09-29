using HealthcareApi.Data;
using HealthcareApi.Interface;
using HealthcareApi.Models;
using Microsoft.EntityFrameworkCore;
using HealthcareApi.DataTransferObjects;
namespace HealthcareApi.Repository
{
    public class UserRepository : IUser
    {
        private readonly EHealthDbContext _dbContext;

        public UserRepository(EHealthDbContext context)
        {
            _dbContext = context;            
        }
        public List<User> GetAllUsers()
        {
            try
            {
                return _dbContext.Users.ToList();
            }
            catch
            {
                return null;
            }
        }
        public User? GetUserById(int id)
        {
            try
            {

                var entity = _dbContext.Users
                    .Include(u => u.Address)
                    .Include(p => p.PaymentInfo)
                    .Include(c => c.Cart)
                    .Where(u => u.Id == id)
                    .FirstOrDefault();
                return entity;
            }
            catch
            {
                return null;
            }
        }

        public User? GetUserByUserName(string username)
        {
            try
            {
                var entity = _dbContext.Users
                    .Include(u => u.Address)
                    .Include(p => p.PaymentInfo)
                    .Include(c => c.Cart)
                    .Where(u => u.UserId == username)
                    .FirstOrDefault();
                return entity;
            }
            catch
            {
                return null;
            }
        }
        public User? AddUser(User entity)
        {
            try
            {
                _dbContext.Users.Add(entity);
                _dbContext.SaveChanges();
                return entity;
            }
            catch
            {
                return null;
            }
        }

        public User? AddUser(AddUserDTO entity)
        {
            try
            {
                var User = new User();
                User.FirstName = entity.FirstName;
                User.LastName = entity.LastName;
                User.Email = entity.Email;
                User.BirthDate = entity.BirthDate;
                User.UserId = entity.UserId;
                User.password = entity.password;
                User.Role = entity.Role;
                foreach (var addr in entity.Address)
                {
                    var newaddr = new UserAddress();
                    newaddr.Address = addr.Address;
                    newaddr.City = addr.City;
                    newaddr.State = addr.State;
                    newaddr.PostalCode = addr.PostalCode;
                    newaddr.Country = addr.Country;
                    newaddr.AddressType = addr.AddressType;
                    User.Address.Add(newaddr);
                }
                return AddUser(User);
            }
            catch
            {
                return null;
            }
        }
        public User? UpdateUser(User entity)
        {
            try
            {
                _dbContext.Entry(entity).State = EntityState.Modified;
                _dbContext.SaveChanges();
                return entity;
            }
            catch
            {
                return null;
            }
        }
        public User? DeleteUserById(int id)
        {
            try
            {
                var entity = _dbContext.Users.Find(id); 
                if(entity == null)
                    throw new ArgumentNullException("id param is invalid. Unable to find User");
                else
                {
                    _dbContext.Users.Remove(entity);
                    _dbContext.SaveChanges();
                    return entity;
                }
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        public User AddNewAddress(AddUserAddressDTO addrdto)
        {
            try
            {
                var user = GetUserById(addrdto.UserId);
                var useraddress = new UserAddress();
                useraddress.Address = addrdto.Address;
                useraddress.City = addrdto.City;
                useraddress.PostalCode = addrdto.PostalCode;
                useraddress.Country = addrdto.Country;
                useraddress.State = addrdto.State;
                useraddress.AddressType = addrdto.AddressType;
                user.Address.Add(useraddress);
                UpdateUser(user);
                return user;
                
            }
            catch
            {
                return null;
            }
        }
    }
}
