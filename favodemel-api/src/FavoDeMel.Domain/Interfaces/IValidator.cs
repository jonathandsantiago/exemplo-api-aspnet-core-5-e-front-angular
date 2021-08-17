using System.Collections.Generic;
using System.Threading.Tasks;

namespace FavoDeMel.Domain.Interfaces
{
    public interface IValidator<TEntity> : IValidator
        where TEntity : class
    {
        /// <summary>
        /// Valida a entidade de forma assíncrona
        /// </summary>
        /// <param name="entidade">Entidade a ser validada</param>
        /// <returns>Retorna o boleano da validação</returns>
        Task<bool> ValidarAsync(TEntity entidade);
    }

    public interface IValidator
    {
        /// <summary>
        /// Mensagens de validação.
        /// </summary>
        /// <returns>Retorna a lista de mensagens da validação</returns>
        IList<string> Mensagens { get; }
        /// <summary>
        /// Retorna se a validação é valida ou não.
        /// </summary>
        /// <returns>Retorna o boleano da validação</returns>
        bool IsValido { get; }
        /// <summary>
        /// Método responsável por adicionar a mensagem de validação.
        /// </summary>
        /// <param name="mensagem">Mensagem</param>
        void AddMensagem(string mensagem);
    }
}