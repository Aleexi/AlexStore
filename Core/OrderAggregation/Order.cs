using Core.Entities;

namespace Core.OrderAggregation
{
    public class Order : SuperEntity
    {
        public Order()
        {
        }

        public Order(IReadOnlyList<OrderItem> orderItems, string userEmail, Address shippingAddress, DelieveryMethod delieveryMethod, decimal subTotal)
        {
            UserEmail = userEmail;
            ShippingAddress = shippingAddress;
            DelieveryMethod = delieveryMethod;
            OrderItems = orderItems;
            SubTotal = subTotal;
        }

        public string UserEmail { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;
        public Address ShippingAddress { get; set; }
        public DelieveryMethod DelieveryMethod { get; set; } 
        public IReadOnlyList<OrderItem> OrderItems { get; set; }
        public decimal SubTotal { get; set; }
        public OrderStatus OrderStatus { get; set; } = OrderStatus.Pending;
        public string PaymentIntentId { get; set; }

        public decimal GetTotal() {
            return SubTotal + DelieveryMethod.PriceOfDelievery;
        }
    }
}