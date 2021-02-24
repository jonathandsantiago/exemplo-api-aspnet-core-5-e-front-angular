using AutoMapper;
using FavoDeMel.Api.Controllers.Common;
using FavoDeMel.Domain.Dtos;
using FavoDeMel.Domain.Models;
using FavoDeMel.Domain.Usuarios;
using FavoDeMel.Framework.Helpers;
using FavoDeMel.IoC.Auth;
using FavoDeMel.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace FavoDeMel.Api.Controllers
{
    public class UsuarioController : ControllerBase<Usuario, int, UsuarioDto, IUsuarioService>
    {
        private readonly SigningConfiguration _signingConfiguration;
        private readonly TokenConfiguration _tokenConfiguration;

        public UsuarioController(IUsuarioService service,
          IHttpContextAccessor httpContextAccessor,
          SigningConfiguration signingConfiguration,
          TokenConfiguration tokenConfiguration)
          : base(service, httpContextAccessor)
        {
            _signingConfiguration = signingConfiguration;
            _tokenConfiguration = tokenConfiguration;
        }       

        /// <summary>
        /// Cadatrar usuario
        /// </summary>
        /// 
        /// <returns>Retorna o id do úsuario cadastrado</returns>
        [HttpPost]
        public async Task<IActionResult> Cadastrar(UsuarioDto dto)
        {
            Func<Task<Usuario>> func = () => _appService.Inserir(Mapper.Map<Usuario>(dto));
            return await ExecutarFuncaoAdminAsync<Usuario, UsuarioDto>(func);
        }

        /// <summary>
        /// Editar usuário
        /// </summary>
        /// 
        /// <returns>Retorna o usuário editado</returns>
        [HttpPut]
        public async Task<IActionResult> Editar(UsuarioDto dto)
        {
            if (dto.Id != UsuarioLogadoId && UsuarioLogadoPerfil != UsuarioPerfil.Administrador)
            {
                return BadRequest("Usúario não possui permissão para executar essa ação.");
            }

            Func<Task<Usuario>> func = () => _appService.Inserir(Mapper.Map<Usuario>(dto));
            return await ExecutarFuncaoAsync<Usuario, UsuarioDto>(func);
        }

        /// <summary>
        /// Alterar Senha usuário
        /// </summary>
        /// 
        /// <returns>Retorna um boleano definindo se deu certo ou não a ação</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> AlterarSenha(int id, string password)
        {

            if (id != UsuarioLogadoId && UsuarioLogadoPerfil != UsuarioPerfil.Administrador)
            {
                return BadRequest("Usúario não possui permissão para executar essa ação.");
            }

            Func<Task<bool>> func = () => _appService.AlterarSenha(id, password);
            return await ExecutarFuncaoAsync(func);
        }

        /// <summary>
        /// Login usuário
        /// </summary>
        /// 
        /// <returns>Retorna as informações do login</returns>
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(loginDto.Login))
                {
                    return BadRequest("Login é obrigatório.");
                }

                if (string.IsNullOrWhiteSpace(loginDto.Password))
                {
                    return BadRequest("Senha é obrigatório.");
                }

                Usuario user = await ObterUsuarioLogin(loginDto.Login, loginDto.Password);

                if (user == null)
                {
                    return BadRequest("Usuário ou Senha inválido.");
                }
                else if (!user.Ativo)
                {
                    return BadRequest("Usuário encontra-se com a situação inativo, por favor entrar em contato com setor Administrativo.");
                }

                JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
                SecurityToken securityToken = GetSecurityToken(user, handler);

                return Ok(new
                {
                    authenticated = true,
                    created = securityToken.ValidFrom.ToString("yyyy-MM-dd HH:mm:ss"),
                    dateExpiration = securityToken.ValidTo.ToString("yyyy-MM-dd HH:mm:ss"),
                    accessToken = handler.WriteToken(securityToken),
                    usuario = Mapper.Map<UsuarioDto>(user)
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
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(UsuarioDto), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> ObterPorId(int id)
        {
            Func<Task<Usuario>> func = () => _appService.ObterPorId(id);
            return await ExecutarFuncaoAsync<Usuario, UsuarioDto>(func);
        }

        /// <summary>
        /// Responsável obter os usuarios paginado
        /// </summary>
        /// 
        /// <returns>Retorna os usuarios paginado</returns>
        [HttpGet]
        [ProducesResponseType(typeof(PaginacaoDto<UsuarioDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> ObterTodosPaginado([FromQuery] FiltroUsuario filtro)
        {
            Func<Task<PaginacaoDto<UsuarioDto>>> func = () => _appService.ObterTodosPaginado(filtro);
            return await ExecutarFuncaoAdminAsync(func);
        }

        private SecurityToken GetSecurityToken(Usuario user, JwtSecurityTokenHandler handler)
        {
            DateTime dateCreation = DateTime.Now;
            DateTime dateExpiration = dateCreation + TimeSpan.FromSeconds(_tokenConfiguration.Seconds);
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
                Issuer = _tokenConfiguration.Issuer,
                Audience = _tokenConfiguration.Audience,
                SigningCredentials = _signingConfiguration.SigningCredentials,
                Subject = identity,
                NotBefore = dateCreation,
                Expires = dateExpiration
            });
        }

        private async Task<Usuario> ObterUsuarioLogin(string login, string password)
        {
            return await _appService.Login(login, StringHelper.CalculateMD5Hash(password));
        }

    }
}
