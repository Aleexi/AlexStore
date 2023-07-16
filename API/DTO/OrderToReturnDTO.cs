
using Core.OrderAggregation;

namespace API.DTO
{
    public class OrderToReturnDTO
    {
        public int Id { get; set; }
        public string UserEmail { get; set; }
        public DateTime OrderDate { get; set; }
        public Address ShippingAddress { get; set; }
        public string DelieveryMethod { get; set; } 
        public decimal ShippingPrice { get; set; }
        public IReadOnlyList<OrderItemDTO> OrderItems { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Total { get; set; }
        public string OrderStatus { get; set; }
    }
}