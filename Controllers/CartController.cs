using HealthcareApi.Data;
using HealthcareApi.DataTransferObjects;
using HealthcareApi.Interface;
using HealthcareApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HealthcareApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class CartController : ControllerBase
    {
        private readonly EHealthDbContext _context;
        private readonly ICart _cartRepo;

        public CartController(ICart cartRepo)
        {
            _cartRepo = cartRepo;          
        }

        [HttpGet("CartByCartId/{cartid}", Name = "GetCartByCartId")]       
        [ProducesResponseType(typeof(Cart), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Cart), StatusCodes.Status400BadRequest)]
        public  async Task <IActionResult> GetCartByCartId(int cartid)
        {
            var cart = await Task.FromResult(_cartRepo.GetCartById(cartid));
            if (cart == null)
            {
                return NotFound();
            }
            else
            {

                return Ok(cart);
            }
        }

        [HttpGet("CartByUserId/{userid}", Name = "GetCartByUserId")]
        
        [ProducesResponseType(typeof(Cart), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Cart), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetCartByUserId(string userid)
        {

            var cart = await Task.FromResult(_cartRepo.GetCartByUserId(userid));
            if (cart == null)
            {
                return NotFound();
            }
            else
            {

                return Ok(cart);
            }
        }

        [HttpPost("Add",Name = "CreateCart")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task <IActionResult> CreateCart(int userId)
        {
            //check if cart exists for user
            try
            {
                var cartmodel = await Task.FromResult(_cartRepo.AddCart(userId));
                //map dto to model
                return Ok(cartmodel);
            }
            catch(Exception ex)
            {
                return Problem(detail: ex.Message + " " + ex.InnerException,statusCode:500, title: "Cart_Add Error");
            } 
            //return Created(uri, cartmodel);
        }

        [HttpPost("AddItemtoCart", Name = "AddItemToCart")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> AddItemtoCart(AddNewCartItemDTO cidto)
        {
            try
            {
                var cartitem = await Task.FromResult(_cartRepo.AddItemToCart(cidto));
                return Ok(cartitem);
            }
            catch(Exception ex)
            {
                return Problem(detail: ex.Message + " " + ex.InnerException, statusCode: 500, title: "Cart_Ad_Items_Error");
            }
        }

        [HttpPost("RemoveItemFromCart", Name = "RemoveItemFromCart")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> RemoveItemFromCart(int cartid, int cartitemid)
        {
            try
            {
                var cartitem = await Task.FromResult(_cartRepo.RemoveItemFromCart(cartid, cartitemid));
                return Ok(cartitem);
            }
            catch (Exception ex)
            {
                return Problem(detail: ex.Message + " " + ex.InnerException, statusCode: 500, title: "Cart_Remove_Items_Error");
            }
        }
        
        [HttpDelete("{cartid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteCart(int cartid)
        {
            try
            {
                Cart cart = _cartRepo.DeleteCartById(cartid);
                return Ok("Delete_Success");
            }
            catch(Exception ex)
            {
                return Problem(detail: ex.Message, statusCode: 500, title: "Cart_Delete Error");
            }
            
        }

        [HttpPost("RemoveAllItemsFromCart", Name = "RemoveAllItemsFromCart")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> RemoveAllItemsFromCart(int cartid)
        {
            try
            {
                var cart = await Task.FromResult(_cartRepo.RemoveAllItemsFromCart(cartid));
                return Ok(cart);
            }
            catch (Exception ex)
            {
                return Problem(detail: ex.Message + " " + ex.InnerException, statusCode: 500, title: "Cart_Clear_Items_Error");
            }
        }
    }
}
