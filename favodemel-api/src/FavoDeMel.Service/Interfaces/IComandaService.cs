using FavoDeMel.Domain.Dtos;
using FavoDeMel.Domain.Entities.Comandas;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FavoDeMel.Service.Interfaces
{
    public interface IComandaService : IServiceBase
    {
        /// <summary>
        /// Obtem a comanda de forma assíncrona
        /// </summary>
        /// <param name="idComanda">Id da comanda</param>
        /// <returns>Retorna a comanda por id</returns>
        Task<ComandaDto> ObterPorIdAsync(Guid idComanda);
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
        /// Confirmar a comanda pelo id de forma assíncrona
        /// </summary>
        /// <param name="idComanda">Id da comanda</param>
        /// <returns>Retorna a comanda confirmada</returns>
        Task<ComandaDto> ConfirmarAsync(Guid idComanda);
        /// <summary>
        /// Fechar a comanda pelo id de forma assíncrona
        /// </summary>
        /// <param name="idComanda">Id da comanda</param>
        /// <returns>Retorna a comanda fechada</returns>
        Task<ComandaDto> Fechar(Guid idComanda);      
    }
}
