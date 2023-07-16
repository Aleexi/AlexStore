using System.Linq.Expressions;
using Core.OrderAggregation;

namespace Core.Specifications
{
    public class OrdersWithItemsAndOrderSpecification : Specification<Order>
    {
        public OrdersWithItemsAndOrderSpecification(string userEmail) : base(order => order.UserEmail == userEmail)
        {
            AddInclude(order => order.OrderItems);
            AddInclude(order => order.DelieveryMethod);
            AddOrderByDescending(order => order.OrderDate);
        }

        public OrdersWithItemsAndOrderSpecification(int id, string userEmail) 
            : base(order => order.Id == id && order.UserEmail == userEmail)
        {
            AddInclude(order => order.OrderItems);
            AddInclude(order => order.DelieveryMethod);
        }
    }
}