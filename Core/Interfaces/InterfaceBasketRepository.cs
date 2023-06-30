using Core.Entities;

namespace Core.Interfaces
{
    public interface InterfaceBasketRepository
    {
        Task<CustomerBasket> GetBasketAsync(string basketId);

        Task<CustomerBasket> UpdateOrCreateBasketAsync(CustomerBasket basket);

        Task<Boolean> DeleteBasketAsync(string basketId);
    }
}