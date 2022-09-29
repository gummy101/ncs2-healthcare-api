using HealthcareApi.DataTransferObjects;
using HealthcareApi.Models;
namespace HealthcareApi.Interface
{
    public interface ICart
    {
        public CartResponseDTO? GetCartById(int id);
        public Cart? GetCart_ById(int id);
        public CartResponseDTO? GetCartByUserId(string username);
        public Cart? AddCart(Cart entity);
        //public Cart? AddCart(CartAddDTO entity);
        public Cart? AddCart(int userId);
        public Cart? UpdateCart(Cart entity);
        public Cart? DeleteCartById(int id);
        public CartItem AddItemToCart(Cart cart,CartItem entity);
        public CartItem AddItemToCart(AddNewCartItemDTO entity);
        public CartItem RemoveItemFromCart(Cart cart, CartItem entity);
        public CartItem RemoveItemFromCart(int cartid, int cartitemid);
        public Cart RemoveAllItemsFromCart(int cartid);


    }
}
