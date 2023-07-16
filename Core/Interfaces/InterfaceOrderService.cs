using System.Collections.Generic;
using Core.OrderAggregation;

namespace Core.Interfaces
{
    public interface InterfaceOrderService
    {
        Task<Order> CreateOrder(string userEmail, int delieveryMethod, string basketId, Address shippingAddress);
        Task<IReadOnlyList<Order>> GetOrdersByUser(string userEmail);
        Task<Order> GetOrderById(int id, string userEmail);
        Task<IReadOnlyList<DelieveryMethod>> GetDelieveryMethods();
    }
}