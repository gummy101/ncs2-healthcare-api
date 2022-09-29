using HealthcareApi.DataTransferObjects;
using HealthcareApi.Models;

namespace HealthcareApi.Interface
{
    public interface IOrder
    {
        public List<Order> GetAllOrders();
        public List<Order> GetOrdersByUserName(string username);
        public Order? GetOrderById(int id);
        public Order? AddOrder(Order entity);
        public Order? AddOrder(CreateOrderDTO orderDTO);
        public Order? UpdateOrder(Order entity);
        public Order? DeleteOrderById(int id);
    }
}
