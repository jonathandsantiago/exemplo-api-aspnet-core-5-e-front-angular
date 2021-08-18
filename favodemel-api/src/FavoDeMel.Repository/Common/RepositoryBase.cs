using FavoDeMel.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FavoDeMel.EF.Repository.Common
{
    public abstract class RepositoryBase<TDbContext>
          where TDbContext : DbContext, IFavoDeMelDbContext
    {
        protected virtual TDbContext DbContext { get; private set; }
        protected virtual IUnitOfWork UnitOfWork { get; set; }

        protected RepositoryBase(TDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public virtual IUnitOfWork BeginTransaction(IValidator _validator = null)
        {
            UnitOfWork ??= new UnitOfWork(DbContext, _validator);

            UnitOfWork.BeginTransactionAsync().Wait();
            return UnitOfWork;
        }
    }
}
