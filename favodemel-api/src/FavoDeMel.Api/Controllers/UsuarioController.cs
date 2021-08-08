using FavoDeMel.Api.Controllers.Common;
using FavoDeMel.Domain.Dtos;
using FavoDeMel.Domain.Dtos.Filtros;
using FavoDeMel.Domain.Helpers;
using FavoDeMel.Domain.Models.Auths;
using FavoDeMel.Domain.Models.Settings;
using FavoDeMel.Domain.Entities.Usuarios;
using FavoDeMel.Service.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using static FavoDeMel.Domain.Models.Routes.Endpoints;

namespace FavoDeMel.Api.Controllers
{
    [Authorize(JwtBearerDefaults.AuthenticationScheme)]
    [Route(Recursos.Usuarios)]
    public class UsuarioController : BaseController
    {
        private readonly SigningConfiguration _signingConfiguration;
        private readonly AuthSettings _authSettings;
        private readonly IUsuarioService _service;

        public UsuarioController(IUsuarioService service,
              SigningConfiguration signingConfiguration,
              AuthSettings authSettings)
        {
            _signingConfiguration = signingConfiguration;
            _authSettings = authSettings;
            _service = service;
        }

        [HttpPost]
        [Route(Rotas.Cadastrar)]
        public async Task<IActionResult> Cadastrar(UsuarioDto dto)
        {
            return await ExecutarFuncaoAsync(() => _service.Inserir(dto));
        }

        [HttpPut]
        [Route(Rotas.Editar)]
        public async Task<IActionResult> Editar(UsuarioDto dto)
        {
            return await ExecutarFuncaoAsync(() => _service.Editar(dto));
        }

        [HttpPut]
        [Route(UsuarioApi.AlterarSenha)]
        public async Task<IActionResult> AlterarSenha(Guid id, string password)
        {
            return await ExecutarFuncaoAsync(() => _service.AlterarSenha(id, password));
        }

        [AllowAnonymous]
        [HttpPost]
        [Route(UsuarioApi.Login)]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            try
            {
                UsuarioDto user = await _service.Login(loginDto);

                if (_service.MensagensValidacao.Any()) return BadRequest(StringHelper.JoinHtmlMensagem(_service.MensagensValidacao));

                JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
                SecurityToken securityToken = GetSecurityToken(user, handler);

                return Ok(new
                {
                    authenticated = true,
                    created = securityToken.ValidFrom.ToString("yyyy-MM-dd HH:mm:ss"),
                    dateExpiration = securityToken.ValidTo.ToString("yyyy-MM-dd HH:mm:ss"),
                    accessToken = handler.WriteToken(securityToken),
                    usuario = new UsuarioDto
                    {
                        Id = user.Id,
                        Nome = user.Nome,
                        Perfil = user.Perfil
                    }
                });
            }
            catch (Exception ex)
            {
                return Error(ex);
            }
        }

        /// <summary>
        /// Responsável obter usuario por id
        /// </summary>
        /// 
        /// <returns>Retorna os usuario pro id</returns>
        [HttpGet]
        [Route(Rotas.ObterPorId)]
        [ProducesResponseType(typeof(UsuarioDto), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> ObterPorId(Guid id)
        {
            return await ExecutarFuncaoAsync(() => _service.ObterPorId(id));
        }

        /// <summary>
        /// Responsável obter os usuarios paginado
        /// </summary>
        /// 
        /// <returns>Retorna os usuarios paginado</returns>
        [HttpGet]
        [Route(Rotas.ObterTodosPaginado)]
        [ProducesResponseType(typeof(PaginacaoDto<UsuarioDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> ObterTodosPaginado([FromQuery] FiltroUsuario filtro)
        {
            return await ExecutarFuncaoAsync(() => _service.ObterTodosPaginado(filtro));
        }

        /// <summary>
        /// Responsável obter os usuarios por perfil
        /// </summary>
        /// 
        /// <returns>Retorna os usuarios por perfil</returns>
        [HttpGet]
        [Route(UsuarioApi.ObterTodosPorPerfil)]
        [ProducesResponseType(typeof(IEnumerable<UsuarioDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> ObterTodosPorPerfil(UsuarioPerfil perfil)
        {
            return await ExecutarFuncaoAsync(() => _service.ObterTodosPorPerfil(perfil));
        }

        private SecurityToken GetSecurityToken(UsuarioDto user, JwtSecurityTokenHandler handler)
        {
            DateTime dateCreation = DateTime.Now;
            DateTime dateExpiration = dateCreation + TimeSpan.FromSeconds(_authSettings.Seconds);
            ClaimsIdentity identity = new ClaimsIdentity(
                     new GenericIdentity(user.Login, "Login"),
                     new[] {
                            new Claim(ClaimName.UserId, Convert.ToString(user.Id)),
                            new Claim(ClaimName.UserName, user.Nome),
                            new Claim(ClaimName.UserPerfil, Convert.ToString((int)user.Perfil)),
                     }
                 );

            return handler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _authSettings.Issuer,
                Audience = _authSettings.Audience,
                SigningCredentials = _signingConfiguration.SigningCredentials,
                Subject = identity,
                NotBefore = dateCreation,
                Expires = dateExpiration
            });
        }
    }
}