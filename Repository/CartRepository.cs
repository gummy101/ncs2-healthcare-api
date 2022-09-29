using HealthcareApi.Data;
using HealthcareApi.DataTransferObjects;
using HealthcareApi.Interface;
using HealthcareApi.Models;
using Microsoft.EntityFrameworkCore;

namespace HealthcareApi.Repository
{
    public class CartRepository : ICart
    {
        private readonly EHealthDbContext _dbContext;
        private IMedicine _medRepo;
        

        public CartRepository(EHealthDbContext context , IMedicine medRepository)
        {
            _dbContext = context;
            _medRepo = medRepository;            
        }

       
        private CartResponseDTO projectCarttoCartDTO(Cart cart)
        {
            var cart_id = cart.Id;
            var created = cart.Created;
            var updated = cart.Updated;
            var cartitems = cart.CartItems;
            var response = new CartResponseDTO(cart_id, created, updated);
            var user = new UserResponseDTO(cart.User.FirstName, cart.User.LastName, cart.User.Email);
            //ads user
            response.User = user;

            //add cartitems.
            foreach (var item in cart.CartItems)
            {
                var cartitemresponse = new CartItemDTO(item.Id, item.Medicine.Name, item.Created, item.Updated, item.Quantity, item.Medicine.Price);
                response.CartItems.Add(cartitemresponse);

            }
            return response;
        }
        public CartResponseDTO? GetCartById(int id)
        {
          try
            {
                var cart = _dbContext.Carts
                            .AsNoTracking()
                            .Include((u) => u.User)
                            .Include((ci) => ci.CartItems)
                            .ThenInclude(m => m.Medicine)
                            .Where((c) => c.Id == id)
                            .FirstOrDefault();
                var response = projectCarttoCartDTO(cart);
                return response;
            }
            catch
            {
                return null;
            }
        }

        public Cart? GetCart_ById(int id)
        {
            return _dbContext.Carts
                .AsNoTracking().Include((u) => u.User)
                .Include((ci) => ci.CartItems)
                .ThenInclude((m) => m.Medicine)
                .Where((c) => c.Id == id)
                .FirstOrDefault();
        }
        public CartResponseDTO GetCartByUserId(string username)
        {
            try
            {
                var cart = _dbContext.Carts
                            .AsNoTracking()
                            .Include((u) => u.User)
                            .Include((ci) => ci.CartItems)
                            .ThenInclude(m => m.Medicine)
                            .Where((c) => c.User.UserId == username)
                            .FirstOrDefault();
                var response = projectCarttoCartDTO(cart);
                return response;                
            }
            catch
            {
                return null;
            }
        }
        public Cart? AddCart(Cart entity)
        {
            try
            {
                _dbContext.Carts.Add(entity);
                _dbContext.SaveChanges();
                return entity;
            }
            catch
            {
                return null;
            }
        }

        public Cart? AddCart(int userId)
        {
            var cart = new Cart();
            cart.UserId = userId;
            cart.Created = DateTime.Now;
            cart.Updated = DateTime.Now;
            cart = AddCart(cart);
            return cart;

        }
        public Cart? UpdateCart(Cart entity)
        {
            _dbContext.Carts.Update(entity);
            _dbContext.SaveChanges();
            return entity;
        }
        public Cart? DeleteCartById(int id)
        {
            try
            {
                Cart entity = GetCart_ById(id);
                if (entity == null)
                    throw new ArgumentNullException("id param is invalid");
                else
                {
                    _dbContext.Carts.Remove(entity);
                    _dbContext.SaveChanges();
                    return entity;
                }
            }
            catch
            {
                return null;
            }
        }
        public CartItem AddItemToCart(Cart cart, CartItem entity)
        {
            try
            {
                cart.CartItems.Add(entity);
                UpdateCart(cart);
                //_dbContext.Entry(cart).State = EntityState.Modified;                ;
                //_dbContext.SaveChanges();
                return entity;
            }
            catch
            {
                return null;
            }
            
        }
        public CartItem AddItemToCart(AddNewCartItemDTO entity)
        {
            try
            {
                var cart = GetCart_ById(entity.CartId);
                var newcartitem = new CartItem();
                newcartitem.Medicine = _medRepo.GetMedicineById(entity.MedicineId);
                newcartitem.Quantity = 1;
                AddItemToCart(cart,newcartitem);
                return newcartitem;
            }
            catch
            {
                return null;
            }
        }
        public CartItem RemoveItemFromCart(Cart cart, CartItem entity)
        {
            try
            {
                cart.CartItems.Remove(entity);
                UpdateCart(cart);
                return entity;
            }
            catch
            {
                return null;
            }
            
        }
        public CartItem RemoveItemFromCart(int cartid, int cartitemid)
        {
            try
            {
                var cart = _dbContext.Carts.Include(c=>c.CartItems).Where((c) => c.Id == cartid).FirstOrDefault();
                var cartitem = cart.CartItems.Where((i) => i.Id == cartitemid).FirstOrDefault();
                if (cartitem != null && cart!= null)
                {
                    RemoveItemFromCart(cart,cartitem);
                }
                return cartitem;

            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public Cart RemoveAllItemsFromCart(int cartid)
        {
            try
            {
                var cart = _dbContext.Carts.Include(c => c.CartItems).Where((c) => c.Id == cartid).FirstOrDefault();
                cart.CartItems.Clear();
                UpdateCart(cart);
                return cart;
            }
            catch
            {
                return null;
            }
        }
    }
}
