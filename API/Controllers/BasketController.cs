using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class BasketController : SuperController
    {
        private readonly InterfaceBasketRepository basketRepository;
        public BasketController(InterfaceBasketRepository basketRepository)
        {
            this.basketRepository = basketRepository;
        }

        [HttpGet("{basketId}")]
        public async Task<ActionResult<CustomerBasket>> GetBasketById(string basketId) {

            var basket = await this.basketRepository.GetBasketAsync(basketId);

            if (basket != null) {
                return Ok(basket);
            }
            return new CustomerBasket(basketId);
        }
        [HttpPost]
        public async Task<ActionResult<CustomerBasket>> UpdateBasket(CustomerBasket basket) {
            var UpdateBasket = await this.basketRepository.UpdateOrCreateBasketAsync(basket);

            return Ok(UpdateBasket);
        }
        [HttpDelete("{basketId}")]
        public async Task DeleteBasket(string basketId) {
            await this.basketRepository.DeleteBasketAsync(basketId);
        }
    }
}