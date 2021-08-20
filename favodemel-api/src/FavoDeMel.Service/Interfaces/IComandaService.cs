using FavoDeMel.Domain.Dtos;
using FavoDeMel.Domain.Dtos.Filtros;
using FavoDeMel.Domain.Entities.Comandas;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FavoDeMel.Service.Interfaces
{
    public interface IComandaService : IService
    {
        /// <summary>
        /// Obtem a comanda de forma assíncrona
        /// </summary>
        /// <param name="comandaId">Id da comanda</param>
        /// <returns>Retorna a comanda por id</returns>
        Task<ComandaDto> ObterPorIdAsync(Guid comandaId);
        /// <summary>
        /// Cadastrar a comanda de forma assíncrona
        /// </summary>
        /// <param name="comandaDto">Comanda Dto</param>
        /// <returns>Retorna a comanda cadastrada</returns>
        Task<ComandaDto> CadastrarAsync(ComandaDto comandaDto);
        /// <summary>
        /// Editar a comanda de forma assíncrona
        /// </summary>
        /// <param name="comandaDto"></param>
        /// <returns>Retorna a comanda editada</returns>
        Task<ComandaDto> EditarAsync(ComandaDto comandaDto);
        /// <summary>
        /// Obter todas as comanda pela situação de forma assíncrona
        /// </summary>
        /// <param name="situacao">Situação</param>
        /// <returns>Retorna todas as comandas por situação</returns>
        Task<IEnumerable<ComandaDto>> ObterTodosPorSituacaoAsync(ComandaSituacao situacao);
        /// <summary>
        /// Obter todas as comanda pela situação paginada na data de forma assíncrona
        /// </summary>
        /// <param name="filtroComanda">Filtro comanda</param>
        /// <returns>Retorna todas as comandas por situação</returns>
        Task<PaginacaoDto<ComandaDto>> ObterPaginadoPorSituacaoAsync(FiltroComanda filtroComanda);
        /// <summary>
        /// Confirmar a comanda pelo id de forma assíncrona
        /// </summary>
        /// <param name="comandaId">Id da comanda</param>
        /// <returns>Retorna a comanda confirmada</returns>
        Task<ComandaDto> ConfirmarAsync(Guid comandaId);
        /// <summary>
        /// Fechar a comanda pelo id de forma assíncrona
        /// </summary>
        /// <param name="comandaId">Id da comanda</param>
        /// <returns>Retorna a comanda fechada</returns>
        Task<ComandaDto> Fechar(Guid comandaId);      
    }
}
