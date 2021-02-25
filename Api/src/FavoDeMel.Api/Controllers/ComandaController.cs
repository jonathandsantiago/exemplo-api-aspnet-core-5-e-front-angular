using AutoMapper;
using FavoDeMel.Api.Controllers.Common;
using FavoDeMel.Domain.Comandas;
using FavoDeMel.Domain.Dtos;
using FavoDeMel.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace FavoDeMel.Api.Controllers
{
    public class ComandaController : ControllerBase<Comanda, int, ComandaDto, IComandaService>
    {
        public ComandaController(IComandaService service,
            IHttpContextAccessor httpContextAccessor)
            : base(service, httpContextAccessor)
        { }

        /// <summary>
        /// Cadatrar comanda
        /// </summary>
        /// 
        /// <returns>Retorna o id do comanda cadastrado</returns>
        [HttpPost]
        public async Task<IActionResult> Cadastrar(ComandaDto dto)
        {
            Func<Task<Comanda>> func = () => _appService.InserirOuEditar(Mapper.Map<Comanda>(dto));
            return await ExecutarFuncaoAsync<Comanda, ComandaDto>(func);
        }

        /// <summary>
        /// Editar comanda
        /// </summary>
        /// 
        /// <returns>Retorna o comanda editada</returns>
        [HttpPut]
        public async Task<IActionResult> Editar(ComandaDto dto)
        {
            Func<Task<Comanda>> func = () => _appService.InserirOuEditar(Mapper.Map<Comanda>(dto));
            return await ExecutarFuncaoAsync<Comanda, ComandaDto>(func);
        }

        /// <summary>
        /// Responsável obter comanda por id
        /// </summary>
        /// 
        /// <returns>Retorna os comanda pro id</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ComandaDto), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> ObterPorId(int id)
        {
            Func<Task<Comanda>> func = () => _appService.ObterPorId(id);
            return await ExecutarFuncaoAsync<Comanda, ComandaDto>(func);
        }

        /// <summary>
        /// Responsável obter os comandas paginado
        /// </summary>
        /// 
        /// <returns>Retorna os comandas paginado</returns>
        [HttpGet("{situacao}")]
        [ProducesResponseType(typeof(IList<ComandaDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> ObterTodosPorSituao(ComandaSituacao situacao)
        {
            Func<Task<IList<Comanda>>> func = () => _appService.ObterTodosPorSituacao(situacao);
            return await ExecutarFuncaoAsync(func);
        }
    }
}