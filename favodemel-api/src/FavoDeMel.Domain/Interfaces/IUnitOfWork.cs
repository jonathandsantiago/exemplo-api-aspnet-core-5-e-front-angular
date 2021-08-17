using System;
using System.Threading.Tasks;

namespace FavoDeMel.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        ///  Inicia uma nova transação de forma assíncrona
        /// </summary>
        Task BeginTransactionAsync();
        /// <summary>
        /// Confirma todas as alterações feitas no banco de dados na transação atual.
        /// </summary>
        Task CommitAsync();
        /// <summary>
        /// Adiciona o validador na transação.
        /// </summary>
        void SetValidator(IValidator validator);
        /// <summary>
        /// Descarta todas as alterações feitas no banco de dados na transação atual de forma assíncrona.
        /// </summary>
        Task RollbackAsync();
        /// <summary>
        /// Salva todas as alterações feitas neste contexto no banco de dados de forma assíncrona.
        /// </summary>
        Task SaveChangesAsync();
    }
}
