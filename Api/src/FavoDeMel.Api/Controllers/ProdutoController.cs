using AutoMapper;
using FavoDeMel.Api.Controllers.Common;
using FavoDeMel.Domain.Dtos;
using FavoDeMel.Domain.Models;
using FavoDeMel.Domain.Produtos;
using FavoDeMel.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;

namespace FavoDeMel.Api.Controllers
{
    public class ProdutoController : ControllerBase<Produto, int, ProdutoDto, IProdutoService>
    {
        public ProdutoController(IProdutoService service,
          IHttpContextAccessor httpContextAccessor)
          : base(service, httpContextAccessor)
        { }

        /// <summary>
        /// Cadatrar produto
        /// </summary>
        /// 
        /// <returns>Retorna o id do produto cadastrado</returns>
        [HttpPost]
        public async Task<IActionResult> Cadastrar(ProdutoDto dto)
        {
            Func<Task<Produto>> func = () => _appService.InserirOuEditar(Mapper.Map<Produto>(dto));
            return await ExecutarFuncaoAsync<Produto, ProdutoDto>(func);
        }

        /// <summary>
        /// Editar produto
        /// </summary>
        /// 
        /// <returns>Retorna o produto editada</returns>
        [HttpPut]
        public async Task<IActionResult> Editar(ProdutoDto dto)
        {
            Func<Task<Produto>> func = () => _appService.InserirOuEditar(Mapper.Map<Produto>(dto));
            return await ExecutarFuncaoAsync<Produto, ProdutoDto>(func);
        }

        /// <summary>
        /// Responsável obter produto por id
        /// </summary>
        /// 
        /// <returns>Retorna os produto pro id</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ProdutoDto), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> ObterPorId(int id)
        {
            Func<Task<Produto>> func = () => _appService.ObterPorId(id);
            return await ExecutarFuncaoAsync<Produto, ProdutoDto>(func);
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