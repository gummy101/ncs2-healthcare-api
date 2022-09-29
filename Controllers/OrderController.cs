using HealthcareApi.Data;
using HealthcareApi.DataTransferObjects;
using HealthcareApi.Interface;
using HealthcareApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace HealthcareApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class OrderController : ControllerBase
    {
        private readonly IOrder _orderRepo;
        private readonly EHealthDbContext _context;
        public OrderController(IOrder orderRepo)
        {
            _orderRepo = orderRepo;
        }
        [HttpGet("{orderid}")]
        [ProducesResponseType(typeof(Order), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Order), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetOrderById(int orderid)
        {
            try
            {
                var order =  await Task.FromResult(_orderRepo.GetOrderById(orderid));
                if (order == null)
                    return NotFound();
                return Ok(order);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }            

        }
        [HttpGet("OrdersByUserId/{userId}")]
        [ProducesResponseType(typeof(Order), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Order), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetOrdersByUser(string userId)
        {
            try
            {
                var orders = await Task.FromResult(_orderRepo.GetOrdersByUserName(userId));
                if (orders == null)
                    return NotFound();
                return Ok(orders);

            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
        [HttpPost("PlaceOrder")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public IActionResult CreateOrder(CreateOrderDTO orderdto)
        {
            try
            {
                var order = _orderRepo.AddOrder(orderdto);
                var o = new {orderid = order.Id,status= "success"};
                return Ok(o);
            }
            catch(Exception ex)
            {
                return Problem(detail: ex.Message + " " + ex.InnerException,statusCode: 500, title: "Order_Add Error");
            }
        }

    }
}
