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
        /// Método responsável obter comanda por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ComandaDto> ObterPorId(Guid id);
        /// <summary>
        /// Método responsável por cadastrar a comanda
        /// </summary>
        /// <param name="comandaDto"></param>
        /// <returns></returns>
        Task<ComandaDto> Inserir(ComandaDto comandaDto);
        /// <summary>
        /// Método responsável por editar a comanda
        /// </summary>
        /// <param name="comandaDto"></param>
        /// <returns></returns>
        Task<ComandaDto> Editar(ComandaDto comandaDto);
        /// <summary>
        /// Método responsável obter as comandas paginado
        /// </summary>
        /// <param name="situacao"></param>
        /// <returns></returns>
        Task<IEnumerable<ComandaDto>> ObterTodosPorSituacao(ComandaSituacao situacao);
        /// <summary>
        /// Método responsável confirmar comanda cozinha
        /// </summary>
        /// <param name="comandaId"></param>
        /// <returns></returns>
        Task<ComandaDto> Confirmar(Guid comandaId);
        /// <summary>
        /// Método responsável fechar comanda
        /// </summary>
        /// <param name="comandaId"></param>
        /// <returns></returns>
        Task<ComandaDto> Fechar(Guid comandaId);      
    }
}
