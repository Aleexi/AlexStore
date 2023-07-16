using Core.Entities;

namespace Core.Interfaces
{
    public interface InterfaceUnitOfWork : IDisposable
    {
        InterfaceRepository<TEntity> Repository<TEntity>() where TEntity : SuperEntity;
        Task<int> Complete();
        
    }
}