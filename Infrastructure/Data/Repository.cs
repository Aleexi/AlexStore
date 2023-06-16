using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class Repository<T> : InterfaceRepository<T> where T : SuperEntity
    {
        private readonly StoreContext context;

        public Repository(StoreContext context)
        { this.context = context; }

        public async Task<T> GetByIdAsync(int id)
        {
            return await this.context.Set<T>().FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IReadOnlyList<T>> GetListByGeneric()
        {
            return await this.context.Set<T>().ToListAsync();
        }

        public async Task<T> GetEntityWithSpecification(InterfaceSpecification<T> specifications)
        {
            var result = await ApplySpecification(specifications).FirstOrDefaultAsync();
            return result;
        }
        public async Task<IReadOnlyList<T>> GetListWithSpecification(InterfaceSpecification<T> specifications)
        {
            var result = await ApplySpecification(specifications).ToListAsync();
            return result;
        }
        private IQueryable<T> ApplySpecification(InterfaceSpecification<T> specifications)
        {
            return SpecificationEvaulation<T>.GetQuery(this.context.Set<T>().AsQueryable(), specifications);
        }
    }
}