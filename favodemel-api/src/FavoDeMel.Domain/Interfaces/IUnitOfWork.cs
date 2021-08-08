using System;
using System.Threading.Tasks;

namespace FavoDeMel.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        Task BeginTransaction();
        Task CommitAsync();
        void SetValidator(IValidator validator);
        Task RollbackAsync();
        Task SaveChangesAsync();
    }
}
