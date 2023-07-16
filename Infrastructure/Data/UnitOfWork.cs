using System.Collections;
using Core.Entities;
using Core.Interfaces;

namespace Infrastructure.Data
{
    public class UnitOfWork : InterfaceUnitOfWork
    {
        private readonly StoreContext context;
        private Hashtable repositories;
        public UnitOfWork(StoreContext context)
        {
            this.context = context;
        }

        public async Task<int> Complete()
        {
            return await this.context.SaveChangesAsync();
        }

        public void Dispose()
        {
            this.context.Dispose();
        }

        public InterfaceRepository<TEntity> Repository<TEntity>() where TEntity : SuperEntity
        {
            if (this.repositories == null) 
            {
                this.repositories = new Hashtable();
            }

            var type = typeof(TEntity).Name;

            if (!this.repositories.ContainsKey(type))
            {
                var repoType = typeof(Repository<>);
                var repoInstance = Activator.CreateInstance(repoType.MakeGenericType(typeof(TEntity)), 
                this.context);

                this.repositories.Add(type, repoInstance);
            }
            return (InterfaceRepository<TEntity>) this.repositories[type];
        }
    }
}