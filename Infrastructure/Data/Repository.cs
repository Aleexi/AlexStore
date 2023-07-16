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

        public async Task<T> GetEntityWithSpecification(Specification<T> specifications)
        {
            var result = await ApplySpecification(specifications).FirstOrDefaultAsync();
            return result;
        }

        public async Task<IReadOnlyList<T>> GetListWithSpecification(Specification<T> specifications)
        {
            var result = await ApplySpecification(specifications).ToListAsync();
            return result;
        }

        public async Task<int> CountAsync(Specification<T> specification)
        {
            return await ApplySpecification(specification).CountAsync();
        }
        
        private IQueryable<T> ApplySpecification(Specification<T> specifications)
        {
            return SpecificationEvaulation<T>.GetQuery(this.context.Set<T>().AsQueryable(), specifications);
        }

        public void Add(T entity)
        {
            this.context.Set<T>().Add(entity);
        }

        public void Update(T entity)
        {
            this.context.Set<T>().Attach(entity);
            this.context.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(T entity)
        {
            this.context.Set<T>().Remove(entity);
        }
    }
}