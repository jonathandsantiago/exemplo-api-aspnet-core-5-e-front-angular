using AutoMapper;
using FavoDeMel.Api.Controllers.Common;
using FavoDeMel.Domain.Dtos;
using FavoDeMel.Domain.Models;
using FavoDeMel.Domain.Produtos;
using FavoDeMel.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace FavoDeMel.Api.Controllers
{
    [Authorize("Bearer")]
    public class ProdutoController : ControllerBase<Produto, int, ProdutoDto, IProdutoService>
    {
        public ProdutoController(IProdutoService service,
          IHttpContextAccessor httpContextAccessor)
          : base(service, httpContextAccessor)
        { }

        /// <summary>
        /// Inserir produto
        /// </summary>
        /// 
        /// <returns>Retorna a produto</returns>
        [HttpPost]
        public async Task<IActionResult> Cadastrar(ProdutoDto dto)
        {
            Func<Task<Produto>> func = () => _appService.Inserir(Mapper.Map<Produto>(dto));
            return await ExecutarFuncaoAsync<Produto, ProdutoDto>(func);
        }

        /// <summary>
        /// Editar produto
        /// </summary>
        /// 
        /// <returns>Retorna a produto</returns>
        [HttpPut]
        public async Task<IActionResult> Editar(ProdutoDto dto)
        {
            Func<Task<Produto>> func = () => _appService.Editar(Mapper.Map<Produto>(dto));
            return await ExecutarFuncaoAsync<Produto, ProdutoDto>(func);
        }

        /// <summary>
        /// Responsável obter produto por id
        /// </summary>
        /// 
        /// <returns>Retorna os produto pro id</returns>
        [HttpGet]
        [ProducesResponseType(typeof(ProdutoDto), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> ObterPorId(int id)
        {
            Func<Task<Produto>> func = () => _appService.ObterPorId(id);
            return await ExecutarFuncaoAsync<Produto, ProdutoDto>(func);
        }

        /// <summary>
        /// Responsável obter os produtos
        /// </summary>
        /// 
        /// <returns>Retorna os produtos</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ProdutoDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> ObterTodos()
        {
            Func<Task<IEnumerable<Produto>>> func = () => _appService.ObterTodos();
            return await ExecutarFuncaoAsync<IEnumerable<Produto>, IEnumerable<ProdutoDto>>(func);
        }

        /// <summary>
        /// Responsável obter os produtos paginado
        /// </summary>
        /// 
        /// <returns>Retorna os produtos paginado</returns>
        [HttpGet]
        [ProducesResponseType(typeof(PaginacaoDto<ProdutoDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> ObterTodosPaginado([FromQuery] FiltroProduto filtro)
        {
            Func<Task<PaginacaoDto<ProdutoDto>>> func = () => _appService.ObterTodosPaginado(filtro);
            return await ExecutarFuncaoAsync(func);
        }
    }
}