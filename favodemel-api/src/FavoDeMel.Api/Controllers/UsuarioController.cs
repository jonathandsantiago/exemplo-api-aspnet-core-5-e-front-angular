using FavoDeMel.Api.Controllers.Common;
using FavoDeMel.Domain.Dtos;
using FavoDeMel.Domain.Dtos.Filtros;
using FavoDeMel.Domain.Entities.Usuarios;
using FavoDeMel.Domain.Helpers;
using FavoDeMel.Domain.Models.Auths;
using FavoDeMel.Domain.Models.Settings;
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
        [ProducesResponseType(typeof(UsuarioDto), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Cadastrar(UsuarioDto dto)
        {
            return await ExecutarFuncaoAsync(() => _service.CadastrarAsync(dto));
        }

        [HttpPut]
        [Route(Rotas.Editar)]
        [ProducesResponseType(typeof(UsuarioDto), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Editar(UsuarioDto dto)
        {
            return await ExecutarFuncaoAsync(() => _service.EditarAsync(dto));
        }

        [HttpPut]
        [Route(UsuarioApi.AlterarSenha)]
        [ProducesResponseType(typeof(UsuarioDto), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> AlterarSenha(Guid id, string password)
        {
            return await ExecutarFuncaoAsync(() => _service.AlterarSenhaAsync(id, password));
        }

        [AllowAnonymous]
        [HttpPost]
        [Route(UsuarioApi.Login)]
        [ProducesResponseType(typeof(object), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            try
            {
                UsuarioDto user = await _service.LoginAsync(loginDto);

                if (_service.MensagensValidacao.Any()) return BadRequest(StringHelper.JoinHtmlMensagem(_service.MensagensValidacao));

                JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
                SecurityToken securityToken = ObterSecurityToken(user, handler);

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

        [HttpGet]
        [Route(Rotas.ObterPorId)]
        [ProducesResponseType(typeof(UsuarioDto), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> ObterPorId(Guid id)
        {
            return await ExecutarFuncaoAsync(() => _service.ObterPorIdAsync(id));
        }

        [HttpGet]
        [Route(Rotas.ObterTodosPaginado)]
        [ProducesResponseType(typeof(PaginacaoDto<UsuarioDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> ObterTodosPaginado([FromQuery] FiltroUsuario filtro)
        {
            return await ExecutarFuncaoAsync(() => _service.ObterTodosPaginadoAsync(filtro));
        }

        [HttpGet]
        [Route(UsuarioApi.ObterTodosPorPerfil)]
        [ProducesResponseType(typeof(IEnumerable<UsuarioDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> ObterTodosPorPerfil(UsuarioPerfil perfil)
        {
            return await ExecutarFuncaoAsync(() => _service.ObterTodosPorPerfilAsync(perfil));
        }

        private SecurityToken ObterSecurityToken(UsuarioDto usuario, JwtSecurityTokenHandler handler)
        {
            DateTime dateCreation = DateTime.Now;
            DateTime dateExpiration = dateCreation + TimeSpan.FromSeconds(_authSettings.Seconds);
            ClaimsIdentity identity = new ClaimsIdentity(
                     new GenericIdentity(usuario.Login, "Login"),
                     new[] {
                            new Claim(ClaimName.UserId, Convert.ToString(usuario.Id)),
                            new Claim(ClaimName.UserName, usuario.Nome),
                            new Claim(ClaimName.UserPerfil, Convert.ToString((int)usuario.Perfil)),
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