using FavoDeMel.Domain.Common;
using FavoDeMel.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FavoDeMel.Domain.Entities.Comandas
{
    public interface IComandaRepository : IRepositoryBase<Guid, Comanda>
    {
        /// <summary>
        /// Obter todas as comanda pela situação de forma assíncrona
        /// </summary>
        /// <param name="situacao">Situação</param>
        /// <returns>Retorna todas as comandas por situação</returns>
        Task<IEnumerable<Comanda>> ObterTodosPorSituacaoAsync(ComandaSituacao situacao);
        /// <summary>
        /// Obter todas as comanda pela situação paginada na data de forma assíncrona
        /// </summary>
        /// <param name="situacao">Situação</param>
        /// <param name="data">Data de cadastro</param>
        /// <param name="page">Pagina atual</param>
        /// <param name="pageSize">Quantidade por pagina</param>
        /// <returns>Retorna todas as comandas por situação</returns>
        Task<PagedList<Comanda>> ObterPaginadoPorSituacaoAsync(ComandaSituacao situacao, DateTime data, int page = 1, int pageSize = 20);
        /// <summary>
        /// Fechar a comanda pelo id de forma assíncrona
        /// </summary>
        /// <param name="comandaId">Id da comanda</param>
        /// <returns>Retorna a comanda fechada</returns>
        Task<Comanda> FecharAsync(Guid comandaId);
        /// <summary>
        /// Confirmar a comanda pelo id de forma assíncrona
        /// </summary>
        /// <param name="comandaId">Id da comanda</param>
        /// <returns>Retorna a comanda confirmada</returns>
        Task<Comanda> ConfirmarAsync(Guid comandaId);
        /// <summary>
        /// Validar se existe a comanda pelo id no sistema
        /// </summary>
        /// <returns>Retorna a validação se existe a comanda no sistem</returns>
        bool Exists(Guid comandaId);
    }
}
