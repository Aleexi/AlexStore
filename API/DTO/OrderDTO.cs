using API.DTO;

namespace API.Controllers
{
    public class OrderDTO
    {
        public string BasketId { get; set; }
        public int DelieveryMethodId { get; set; }
        public AddressDTO ShippingAddress { get; set; }
    }
}