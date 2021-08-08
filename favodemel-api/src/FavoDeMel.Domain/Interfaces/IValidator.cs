using System.Collections.Generic;
using System.Threading.Tasks;

namespace FavoDeMel.Domain.Interfaces
{
    public interface IValidator<TEntity> : IValidator
        where TEntity : class
    {
        /// <summary>
        /// Método responsável por validar a entidade.
        /// </summary>
        /// <param name="entity">Entidade</param>
        /// <returns></returns>
        Task<bool> Validar(TEntity entity);
    }

    public interface IValidator
    {
        IList<string> Mensagens { get; }
        bool IsValido { get; }
        /// <summary>
        /// Método responsável por adicionar as mensagens de validação.
        /// </summary>
        /// <param name="mensagem">Mensagem</param>
        /// <returns></returns>
        void AddMensagem(string mensagem);
    }
}