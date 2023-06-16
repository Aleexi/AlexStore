using Core.Entities;
using Core.Specifications;

namespace Core.Interfaces
{
    public interface InterfaceRepository<T> where T : SuperEntity
    {
        // Products and Pokemons by id
        Task<T> GetByIdAsync(int id);

        Task<IReadOnlyList<T>> GetListByGeneric();

        Task<T> GetEntityWithSpecification(InterfaceSpecification<T> specifications);
        
        Task<IReadOnlyList<T>> GetListWithSpecification(InterfaceSpecification<T> specifications);
    }
}