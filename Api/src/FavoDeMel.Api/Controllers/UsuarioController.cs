using FavoDeMel.Api.Controllers.Common;
using FavoDeMel.Api.Dtos;
using FavoDeMel.Domain.Common;
using FavoDeMel.Domain.Usuarios;
using FavoDeMel.IoC.Auth;
using FavoDeMel.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
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

        public UsuarioController(IUsuarioService service, SigningConfiguration signingConfiguration, TokenConfiguration tokenConfiguration)
           : base(service)
        {
            _signingConfiguration = signingConfiguration;
            _tokenConfiguration = tokenConfiguration;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Create(UsuarioDto dto)
        {
            try
            {
                if (!string.IsNullOrEmpty(dto.Password))
                {
                    dto.Password = DomainHelper.CalculateMD5Hash(dto.Password);
                }

                return await ExecuteAction<int>(ActionType.Post, dto);
            }
            catch (Exception ex)
            {
                return Error(ex);
            }
        }

        /// <summary>
        /// Autenticar usuário
        /// </summary>
        /// <returns>IList(Comanda)</returns>
        [AllowAnonymous]
        [HttpPost]
        [ProducesResponseType(typeof(object), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(object), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(object), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(object), (int)HttpStatusCode.RequestTimeout)]
        [ProducesResponseType(typeof(object), (int)HttpStatusCode.GatewayTimeout)]
        public async Task<IActionResult> Authenticate([FromBody]LoginDto loginDto)
        {
            try
            {
                Usuario user = await GetUser(loginDto.Login, loginDto.Password);

                if (user == null)
                {
                    return BadRequest("Usuário inválido.");
                }

                JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
                SecurityToken securityToken = GetSecurityToken(user, handler);

                return Ok(new
                {
                    authenticated = true,
                    created = securityToken.ValidFrom.ToString("yyyy-MM-dd HH:mm:ss"),
                    dateExpiration = securityToken.ValidTo.ToString("yyyy-MM-dd HH:mm:ss"),
                    accessToken = handler.WriteToken(securityToken),
                    UserId = user.Id,
                    UserName = user.Nome,
                    message = "OK"
                });
            }
            catch (Exception ex)
            {
                return Error(ex);
            }
        }

        private SecurityToken GetSecurityToken(Usuario user, JwtSecurityTokenHandler handler)
        {
            DateTime dateCreation = DateTime.Now;
            DateTime dateExpiration = dateCreation + TimeSpan.FromSeconds(_tokenConfiguration.Seconds);
            ClaimsIdentity identity = new ClaimsIdentity(
                     new GenericIdentity(user.Login, "Login"),
                     new[] {
                            new Claim(ClaimName.UserId, user.Id.ToString("N")),
                            new Claim(ClaimName.UserName, user.Nome),
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

        private async Task<Usuario> GetUser(string login, string password)
        {
            if (string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(password))
            {
                return null;
            }

            return await _appService.GetByLoginPassword(login, DomainHelper.CalculateMD5Hash(password));
        }
    }
}
