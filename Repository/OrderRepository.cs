using HealthcareApi.Data;
using HealthcareApi.DataTransferObjects;
using HealthcareApi.Interface;
using HealthcareApi.Models;
using Microsoft.EntityFrameworkCore;

namespace HealthcareApi.Repository
{
    public class OrderRepository :IOrder
    {
        private readonly EHealthDbContext _dbContext;
        private readonly IUser _userRepository;
        private readonly IPayment _paymentRepository;
        private readonly ICart _cartRepo;



        public OrderRepository(EHealthDbContext context, IUser userRepository, IPayment paymentRepository, ICart  cartRepo)
        {
            _dbContext = context;
            _userRepository = userRepository;
            _paymentRepository = paymentRepository;
            _cartRepo = cartRepo;
        }

        public List<Order> GetAllOrders()
        {
            try
            {
                try
                {
                    return _dbContext.Orders.ToList();
                }
                catch
                {
                    return new List<Order>();
                }
            }
            catch
            {
                return null;
            }
        }
        public Order? GetOrderById(int id)
        {
            try
            {
                return _dbContext
                    .Orders
                    .AsNoTracking()
                    .Include(oi=>oi.Items)
                    .ThenInclude(oi=>oi.Medicine)
                    .Include(p=>p.PaymentInfo)
                    .Include(a=>a.BillingAddress)
                    .Include(s=>s.ShippingAddress)
                    .Where(o => o.Id == id)
                    .FirstOrDefault();
                    
            }
            catch
            {
                return null;
            }
        }
        public List<Order>? GetOrdersByUserName(string username)
        {
            try
            {
                var orders = _dbContext
                        .Orders
                        .AsNoTracking()
                        .Include(o=>o.User)
                        .Where(o=>o.User.UserId == username)
                        .ToList();
                return orders;
            }
            catch
            {
                return null;
            }
        }
        public Order AddOrder(Order order)
        {
            _dbContext.Orders.Add(order);
            _dbContext.SaveChanges();
            return order;
        }
        public Order? AddOrder(CreateOrderDTO orderdto)
        {
            try
            {
                //create order from the CreateOrderDTO
                var order = new Order();
                order.OrderStatus = orderdto.OrderStatus;
                order.CreatedDate = DateTime.Now;
                order.UpdatedDate = DateTime.Now;
                order.User = _userRepository.GetUserById(orderdto.UserId);
                //order.Cart = _cartRepo.GetCart_ById(orderdto.CartId);
                order.Cart = _dbContext.Carts.Include(c=>c.CartItems).ThenInclude(ci=>ci.Medicine).Where(c=>c.Id == orderdto.CartId).FirstOrDefault();
                order.PaymentInfo = _paymentRepository.GetPaymentById(orderdto.PaymentInfoId);
                order.ShippingAddress = order.User.Address.Where((addr) => (addr.Id == orderdto.ShipAddrId)).FirstOrDefault();
                order.BillingAddress = order.User.Address.Where((addr) => (addr.Id == orderdto.BillAddrId)).FirstOrDefault();

                //add order items by iterating thru cart items
                foreach (var ci in order.Cart.CartItems)
                {
                    var oi = new OrderItem();
                    oi.Order = order;
                    oi.Medicine = ci.Medicine;
                    oi.Quantity = ci.Quantity;
                    order.Items.Add(oi);
                }
                _dbContext.Orders.Add(order);
                _dbContext.SaveChanges();
                //AddOrder(order);
                return order;
            }
            catch
            {
                return null;
            }
        }
        public Order? UpdateOrder(Order entity)
        {
            try
            {
                _dbContext.Orders.Update(entity);
                _dbContext.SaveChanges();
                return entity;
            }
            catch
            {
                return null;
            }
        }
        public Order? DeleteOrderById(int id)
        {
            try
            {
                Order entity = _dbContext.Orders.Find(id);
                if (entity == null)
                    throw new ArgumentNullException("id param is invalid.Unable to find order");
                else
                {
                    _dbContext.Orders.Remove(entity);
                    _dbContext.SaveChanges();
                    return entity;
                }
            }
            catch
            {
                return null;
            }
        }
    }
}
