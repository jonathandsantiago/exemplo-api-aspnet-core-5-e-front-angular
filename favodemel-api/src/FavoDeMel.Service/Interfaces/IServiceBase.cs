using System.Collections.Generic;

namespace FavoDeMel.Service.Interfaces
{
    public interface IServiceBase
    {
        /// <summary>
        /// Obter as mensagens de validação
        /// </summary>
        /// <returns>Retornas as mensagens de validação</returns>
        IList<string> MensagensValidacao { get; }
    }
}