using FavoDeMel.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FavoDeMel.Domain.Entities.Comandas
{
    public interface IComandaRepository : IRepositoryBase<Guid, Comanda>
    {
        /// <summary>
        /// Método responsável por obter todos pela situação
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Comanda>> ObterTodosPorSituacao(ComandaSituacao situacao);
        /// <summary>
        /// Método responsável por fechar a comanda
        /// </summary>
        /// <returns></returns>
        Task<Comanda> Fechar(Guid comandaId);
        /// <summary>
        /// Método responsável por confirmar a comanda
        /// </summary>
        /// <returns></returns>
        Task<Comanda> Confirmar(Guid comandaId);
        /// <summary>
        /// Método responsável por validar se existe a comanda
        /// </summary>
        /// <returns></returns>
        bool Exists(Guid comandaId);
    }
}
