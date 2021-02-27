using AutoMapper;
using FavoDeMel.Api.Controllers.Common;
using FavoDeMel.Domain.Comandas;
using FavoDeMel.Domain.Dtos;
using FavoDeMel.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace FavoDeMel.Api.Controllers
{
    [Authorize("Bearer")]
    public class ComandaController : ControllerBase<Comanda, int, ComandaDto, IComandaService>
    {
        public ComandaController(IComandaService service,
            IHttpContextAccessor httpContextAccessor)
            : base(service, httpContextAccessor)
        { }

        /// <summary>
        /// Inserir comanda
        /// </summary>
        /// 
        /// <returns>Retorna a comanda</returns>
        [HttpPost]
        public async Task<IActionResult> Cadastrar(ComandaDto dto)
        {
            Func<Task<Comanda>> func = () => _appService.Inserir(Mapper.Map<Comanda>(dto));
            return await ExecutarFuncaoAsync<Comanda, ComandaDto>(func);
        }

        /// <summary>
        /// Editar comanda
        /// </summary>
        /// 
        /// <returns>Retorna a comanda</returns>
        [HttpPut]
        public async Task<IActionResult> Editar(ComandaDto dto)
        {
            Func<Task<Comanda>> func = () => _appService.Editar(Mapper.Map<Comanda>(dto));
            return await ExecutarFuncaoAsync<Comanda, ComandaDto>(func);
        }

        /// <summary>
        /// Responsável obter comanda por id
        /// </summary>
        /// 
        /// <returns>Retorna os comanda pro id</returns>
        [HttpGet]
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
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ComandaDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> ObterTodosPorSituao(ComandaSituacao situacao)
        {
            Func<Task<IEnumerable<Comanda>>> func = () => _appService.ObterTodosPorSituacao(situacao);
            return await ExecutarFuncaoAsync<IEnumerable<Comanda>, IEnumerable<ComandaDto>>(func);
        }

        /// <summary>
        /// Responsável confirmar comanda cozinha
        /// </summary>
        /// 
        /// <returns>Retorna comanda atualizada</returns>
        [HttpPost]
        [ProducesResponseType(typeof(Comanda), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Confirmar([FromBody] int comandaId)
        {
            Func<Task<Comanda>> func = () => _appService.Confirmar(comandaId);
            return await ExecutarFuncaoAsync<Comanda, ComandaDto>(func);
        }

        /// <summary>
        /// Responsável fechar comanda
        /// </summary>
        /// 
        /// <returns>Retorna a comanda atualizada</returns>
        [HttpPost]
        [ProducesResponseType(typeof(Comanda), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Fechar([FromBody] int comandaId)
        {
            Func<Task<Comanda>> func = () => _appService.Fechar(comandaId);
            return await ExecutarFuncaoAsync<Comanda, ComandaDto>(func);
        }
    }
}