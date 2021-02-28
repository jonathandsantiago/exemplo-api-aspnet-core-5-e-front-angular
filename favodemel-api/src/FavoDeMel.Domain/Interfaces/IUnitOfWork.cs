using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Threading.Tasks;

namespace FavoDeMel.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IDbContextTransaction Transaction { get; }
        Task BeginTransaction();
        Task CommitAsync();
        Task RollbackAsync();
        Task SaveChangesAsync();
        void SetValidator(IValidator validator);
    }
}
