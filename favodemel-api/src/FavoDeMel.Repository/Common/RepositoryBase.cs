using FavoDeMel.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace FavoDeMel.EF.Repository.Common
{
    public abstract class RepositoryBase<TDbContext>
          where TDbContext : DbContext, IFavoDeMelDbContext
    {
        protected virtual TDbContext DbContext { get; private set; }
        public virtual bool AutoSaveChanges { get; set; } = true;
        protected virtual IUnitOfWork UnitOfWork { get; set; }

        protected RepositoryBase(TDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public virtual async Task<int> SaveAllChangesAsync()
        {
            return await DbContext.SaveChangesAsync();
        }

        public virtual IUnitOfWork BeginTransaction(IValidator _validator = null)
        {
            UnitOfWork ??= new UnitOfWork(DbContext, _validator);

            UnitOfWork.BeginTransaction().Wait();
            return UnitOfWork;
        }

        public IUnitOfWork BeginTransaction(IValidator _validator = null, IUnitOfWork unitOfWork = null)
        {
            UnitOfWork = unitOfWork;
            return BeginTransaction(_validator);
        }
    }
}
