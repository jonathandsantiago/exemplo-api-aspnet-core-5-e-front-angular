using System.Collections.Generic;

namespace FavoDeMel.Service.Interfaces
{
    public interface IServiceBase
    {
        /// <summary>
        /// Responsável por obter as mensagens de validação
        /// </summary>
        /// <returns></returns>
        IList<string> MensagensValidacao { get; }
    }
}