using Core.Entities;

namespace Core.OrderAggregation
{
    public class OrderItem : SuperEntity
    {
        public OrderItem()
        {
        }

        public OrderItem(ItemOrdered itemOrdered, decimal price, int quantity)
        {
            ItemOrdered = itemOrdered;
            Price = price;
            Quantity = quantity;
        }

        public ItemOrdered ItemOrdered { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}