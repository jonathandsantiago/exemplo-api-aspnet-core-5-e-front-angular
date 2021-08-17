using FavoDeMel.Api.Controllers.Common;
using FavoDeMel.Domain.Dtos;
using FavoDeMel.Domain.Dtos.Filtros;
using FavoDeMel.Service.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using static FavoDeMel.Domain.Models.Routes.Endpoints;

namespace FavoDeMel.Api.Controllers
{
    [Authorize(JwtBearerDefaults.AuthenticationScheme)]
    [Route(Recursos.Produtos)]
    public class ProdutoController : BaseController
    {
        private readonly IProdutoService _service;

        public ProdutoController(IProdutoService service)
        {
            _service = service;
        }

        [HttpPost]
        [Route(Rotas.Cadastrar)]
        [ProducesResponseType(typeof(ProdutoDto), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Cadastrar(ProdutoDto produtoDto)
        {
            return await ExecutarFuncaoAsync(() => _service.CadastrarAsync(produtoDto));
        }

        [HttpPut]
        [Route(Rotas.Editar)]
        [ProducesResponseType(typeof(ProdutoDto), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Editar(ProdutoDto produtoDto)
        {
            return await ExecutarFuncaoAsync(() => _service.EditarAsync(produtoDto));
        }

        [HttpGet]
        [Route(Rotas.ObterPorId)]
        [ProducesResponseType(typeof(ProdutoDto), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> ObterPorId(Guid id)
        {
            return await ExecutarFuncaoAsync(() => _service.ObterPorIdAsync(id));
        }

        [HttpGet]
        [Route(Rotas.ObterTodosPaginado)]
        [ProducesResponseType(typeof(PaginacaoDto<ProdutoDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> ObterTodosPaginado([FromQuery] FiltroProduto filtro)
        {
            return await ExecutarFuncaoAsync(() => _service.ObterTodosPaginadoAsync(filtro));
        }

        [HttpGet]
        [Route(Rotas.ObterTodos)]
        [ProducesResponseType(typeof(IEnumerable<ProdutoDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> ObterTodos()
        {
            return await ExecutarFuncaoAsync(() => _service.ObterTodosAsync());
        }
    }
}