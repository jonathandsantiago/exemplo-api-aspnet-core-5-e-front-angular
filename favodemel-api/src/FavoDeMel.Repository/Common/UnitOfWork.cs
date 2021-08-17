using FavoDeMel.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace FavoDeMel.EF.Repository.Common
{
    public class UnitOfWork : IUnitOfWork
    {
        public IDbContextTransaction Transaction { get; private set; }
        private DbContext _dbContext;
        private IValidator _validator;
        int contador;

        public UnitOfWork(DbContext dbContext, IValidator validator)
        {
            _dbContext = dbContext;
            _validator = validator;
        }

        public virtual async Task BeginTransactionAsync()
        {
            if (Transaction == null)
            {
                Transaction = await _dbContext.Database.BeginTransactionAsync();
            }

            contador += 1;
        }

        public async Task CommitAsync()
        {
            if (contador > 1)
            {
                contador -= 1;
                return;
            }

            try
            {
                await SaveChangesAsync();
                Transaction.Commit();
            }
            finally
            {
                Transaction.Dispose();
            }
        }

        public virtual void SetValidator(IValidator validator)
        {
            if (_validator == null)
            {
                _validator = validator;
            }
        }

        public virtual async Task RollbackAsync()
        {
            await Transaction.RollbackAsync();
            Transaction.Dispose();
        }

        public virtual async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            if (!IsInException() && (_validator == null || _validator.IsValido))
            {
                CommitAsync().Wait();
            }
            else
            {
                RollbackAsync().Wait();
            }
        }

        private bool IsInException()
        {
            return Marshal.GetExceptionPointers() != IntPtr.Zero;
        }
    }
}
