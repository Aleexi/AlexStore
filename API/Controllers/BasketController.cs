using API.DTO;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class BasketController : SuperController
    {
        private readonly InterfaceBasketRepository basketRepository;
        private readonly IMapper mapper;
        public BasketController(InterfaceBasketRepository basketRepository, IMapper mapper)
        {
            this.mapper = mapper;
            this.basketRepository = basketRepository;
        }

        [HttpGet]
        public async Task<ActionResult<CustomerBasket>> GetBasketById([FromQuery] string basketId) {
            
            var basket = await this.basketRepository.GetBasketAsync(basketId);

            if (basket != null) {
                return Ok(basket);
            }
            return new CustomerBasket(basketId);
        }

        [HttpPost]
        public async Task<ActionResult<CustomerBasket>> UpdateBasket(CustomerBasketDTO basket) {
        
            var UpdateBasket = await this.basketRepository.UpdateOrCreateBasketAsync(
                this.mapper.Map<CustomerBasketDTO, CustomerBasket>(basket));

            return Ok(UpdateBasket);
        }
        
        [HttpDelete]
        public async Task DeleteBasket([FromQuery] string basketId) {
            await this.basketRepository.DeleteBasketAsync(basketId);
        }
    }
}