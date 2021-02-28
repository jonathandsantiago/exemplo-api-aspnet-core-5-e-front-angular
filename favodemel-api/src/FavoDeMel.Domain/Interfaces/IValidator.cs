using FavoDeMel.Domain.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FavoDeMel.Domain.Interfaces
{
    public interface IValidator<TId, TEntity> : IValidator
        where TEntity : Entity<TId>
    {
        Task<bool> Validar(TEntity entity);
        Task<bool> PermiteExcluir(TId id);
    }

    public interface IValidator
    {
        IList<string> Mensagens { get; }
        bool IsValido { get; }
        void AddMensagem(string mensagem);
    }
}