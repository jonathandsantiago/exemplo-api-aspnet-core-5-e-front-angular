using FavoDeMel.Api.Controllers.Common;
using FavoDeMel.Domain.Dtos;
using FavoDeMel.Domain.Entities.Comandas;
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
    [Route(Recursos.Comandas)]
    public class ComandaController : BaseController
    {
        private readonly IComandaService _service;

        public ComandaController(IComandaService service)
        {
            _service = service;
        }
        
        [HttpPost]
        [Route(Rotas.Cadastrar)]
        public async Task<IActionResult> Cadastrar(ComandaDto dto)
        {
            return await ExecutarFuncaoAsync(() => _service.Inserir(dto));
        }
       
        [HttpPut]
        [Route(Rotas.Editar)]
        public async Task<IActionResult> Editar(ComandaDto dto)
        {
            return await ExecutarFuncaoAsync(() => _service.Editar(dto));
        }
     
        [HttpGet]
        [Route(Rotas.ObterPorId)]
        [ProducesResponseType(typeof(ComandaDto), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> ObterPorId(Guid id)
        {
            return await ExecutarFuncaoAsync(() => _service.ObterPorId(id));
        }

        [HttpGet]
        [Route(ComandaApi.ObterTodosPorSituacao)]
        [ProducesResponseType(typeof(IEnumerable<ComandaDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> ObterTodosPorSituao(ComandaSituacao situacao)
        {
            return await ExecutarFuncaoAsync(() => _service.ObterTodosPorSituacao(situacao));
        }
      
        [HttpPost]
        [Route(ComandaApi.Confirmar)]
        [ProducesResponseType(typeof(ComandaDto), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Confirmar([FromBody] DtoBase<Guid> dto)
        {
            return await ExecutarFuncaoAsync(() => _service.Confirmar(dto.Id));
        }

        [HttpPost]
        [Route(ComandaApi.Fechar)]
        [ProducesResponseType(typeof(ComandaDto), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Fechar([FromBody] DtoBase<Guid> dto)
        {
            return await ExecutarFuncaoAsync(() => _service.Fechar(dto.Id));
        }
    }
}