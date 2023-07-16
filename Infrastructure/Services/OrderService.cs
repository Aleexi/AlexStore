using Core.Entities;
using Core.Interfaces;
using Core.OrderAggregation;
using Core.Specifications;

namespace Infrastructure.Services
{
    public class OrderService : InterfaceOrderService
    {
        private readonly InterfaceBasketRepository basketRepository;
        private readonly InterfaceUnitOfWork unitOfWork;

        public OrderService(InterfaceBasketRepository basketRepository, InterfaceUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            
            this.basketRepository = basketRepository;

        }

        public async Task<Order> CreateOrder(string userEmail, int delieveryMethodId, string basketId, Address shippingAddress)
        {
            var basket = await this.basketRepository.GetBasketAsync(basketId);

            // get items from product and pokemon repositories
            var items = new List<OrderItem>();

            foreach (var item in basket.Items)
            {
                if (item.Abilitie != null) {
                    var pokemonItem = await this.unitOfWork.Repository<Pokemon>().GetByIdAsync(item.Id);
                    var itemOrdered = new ItemOrdered(pokemonItem.Id, pokemonItem.Name, pokemonItem.PictureURL);
                    var orderItem = new OrderItem(itemOrdered, pokemonItem.Strength, item.Quantity);
                    items.Add(orderItem);
                }

                else if (item.Abilitie == null) {
                    var productItem = await this.unitOfWork.Repository<Product>().GetByIdAsync(item.Id);
                    var itemOrdered = new ItemOrdered(productItem.Id, productItem.Name, productItem.PictureURL);
                    var orderItem = new OrderItem(itemOrdered, productItem.Price, item.Quantity);
                    items.Add(orderItem);
                }
            }

            var delieveryMethod = await this.unitOfWork.Repository<DelieveryMethod>().GetByIdAsync(delieveryMethodId);

            var subTotal = items.Sum(item => item.Price * item.Quantity);

            var order = new Order(items, userEmail, shippingAddress, delieveryMethod, subTotal);

            this.unitOfWork.Repository<Order>().Add(order);

            var saveResult = await this.unitOfWork.Complete();

            if (saveResult <= 0)
            {
                return null;
            }
            await this.basketRepository.DeleteBasketAsync(basketId);

            return order;
        }

        public async Task<IReadOnlyList<DelieveryMethod>> GetDelieveryMethods()
        {
            return await this.unitOfWork.Repository<DelieveryMethod>().GetListByGeneric();
        }

        public async Task<Order> GetOrderById(int id, string userEmail)
        {
            var spec = new OrdersWithItemsAndOrderSpecification(id, userEmail);

            return await this.unitOfWork.Repository<Order>().GetEntityWithSpecification(spec);
        }

        public async Task<IReadOnlyList<Order>> GetOrdersByUser(string userEmail)
        {
            var spec = new OrdersWithItemsAndOrderSpecification(userEmail);

            return await this.unitOfWork.Repository<Order>().GetListWithSpecification(spec);
            
        }
    }
}