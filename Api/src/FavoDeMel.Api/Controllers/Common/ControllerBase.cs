using AutoMapper;
using FavoDeMel.Domain.Common;
using FavoDeMel.Domain.Dtos;
using FavoDeMel.Domain.Interfaces;
using FavoDeMel.Domain.Usuarios;
using FavoDeMel.IoC.Auth;
using FavoDeMel.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FavoDeMel.Api.Controllers.Common
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public abstract class ControllerBase<TEntity, TId, TDto, TAppService> : ControllerBase<TEntity, TDto, TAppService>
       where TEntity : Entity<TId>
       where TDto : DtoBase<TId>
       where TAppService : IServiceBase<TId, TEntity>
    {
        public ControllerBase(TAppService appService, IHttpContextAccessor httpContextAccessor)
            : base(appService, httpContextAccessor)
        { }

        /// <summary>
        /// Responsavel por enviar as mensagens de validação que produz um Microsoft.AspNetCore.Http.StatusCodes.Status 400
        /// </summary>
        /// 
        /// <returns>Retorna as mensagens de validação serializada</returns>
        protected virtual IActionResult BadRequest(string message = null)
        {
            if (!string.IsNullOrEmpty(message))
            {
                if (_appService.MensagensValidacao.Any())
                {
                    return StatusCode(400, $"{message}{"<br>"}{string.Join("<br>", _appService.MensagensValidacao)}");
                }

                return StatusCode(400, message);
            }

            return StatusCode(400, string.Join("<br>", _appService.MensagensValidacao));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        protected new IActionResult Response(object result = null)
        {
            if (_appService.MensagensValidacao.Any())
            {
                return BadRequest();
            }

            if (result is string)
            {
                return Ok(new { result });
            }

            return Ok(result);
        }

        /// <summary>
        /// Executa uma ação, e retorna um IActionResult
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="funcao"></param>
        /// <returns></returns>
        protected async override Task<IActionResult> ExecutarFuncaoAsync<T>(Func<Task<T>> funcao)
        {
            try
            {
                var retorno = await funcao();
                return Response(retorno);
            }
            catch (Exception ex)
            {
                return Error(ex);
            }
        }

        /// <summary>
        /// Executa uma ação, e retorna um IActionResult com automapper
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="funcao"></param>
        /// <returns></returns>
        protected async override Task<IActionResult> ExecutarFuncaoAsync<T, TResult>(Func<Task<T>> funcao)
        {
            try
            {
                var retorno = await funcao();
                return Response(Mapper.Map<TResult>(retorno));
            }
            catch (Exception ex)
            {
                return Error(ex);
            }
        }

        /// <summary>
        /// Executa uma ação somente para usuário administrador, e retorna um IActionResult
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="funcao"></param>
        /// <returns></returns>
        protected async Task<IActionResult> ExecutarFuncaoAdminAsync<T>(Func<Task<T>> funcao)
        {
            if (UsuarioLogadoPerfil != UsuarioPerfil.Administrador)
            {
                return BadRequest("Usúario não possui permissão para executar essa ação.");
            }

            return await ExecutarFuncaoAsync(funcao);
        }

        /// <summary>
        /// Executa uma ação somente para usuário administrador, e retorna um IActionResult
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="funcao"></param>
        /// <returns></returns>
        protected async Task<IActionResult> ExecutarFuncaoAdminAsync<T, TResult>(Func<Task<T>> funcao)
        {
            if (UsuarioLogadoPerfil != UsuarioPerfil.Administrador)
            {
                return BadRequest("Usúario não possui permissão para executar essa ação.");
            }

            return await ExecutarFuncaoAsync<T, TResult>(funcao);
        }

        /// <summary>
        /// Executa uma ação e retorna um FileResult
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="funcao"></param>
        /// <returns></returns>
        protected async Task<IActionResult> ExecutarFuncaoExportacaoZipAsync(Func<Task<byte[]>> funcao, string nomeArquivo)
        {
            try
            {
                var resultado = await funcao();
                return File(resultado.ToArray(), "application/octet-stream", nomeArquivo);
            }
            catch (Exception ex)
            {
                return Error(ex);
            }
        }

        /// <summary>
        /// Executa uma ação e retorna um FileResult
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="funcao"></param>
        /// <returns></returns>
        protected async Task<IActionResult> ExecutarFuncaoExportacaoExcelAsync(Func<Task<byte[]>> funcao, string nomeArquivo)
        {
            try
            {
                var resultado = await funcao();
                return File(resultado.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", nomeArquivo);
            }
            catch (Exception ex)
            {
                return Error(ex);
            }
        }
    }

    [Route("api/[controller]/[action]")]
    [ApiController]
    public abstract class ControllerBase<TEntity, TDto, TAppService> : ControllerBase
      where TEntity : Entity
      where TDto : IDtoBase
      where TAppService : IServiceBase<TEntity>
    {
        protected readonly TAppService _appService;
        protected HttpContext _httpContext;

        /// <summary>
        /// Responsavel por obter o id do usuario logado
        /// </summary>
        /// 
        /// <returns>Id do usuário logado</returns>
        protected virtual int UsuarioLogadoId
        {
            get
            {
                Claim claim = GetClaim(ClaimName.UserId);
                return claim == null ? 0 : Convert.ToInt32(claim.Value);
            }
        }

        /// <summary>
        /// Responsavel por obter o nome do usuario logado
        /// </summary>
        /// 
        /// <returns>Nome do usuário logado</returns>
        protected virtual string UsuarioLogadoNome
        {
            get
            {
                return GetClaim(ClaimName.UserName)?.Value;
            }
        }

        /// <summary>
        /// Responsavel por obter o tipo de cadastro do usuario logado
        /// </summary>
        /// 
        /// <returns>Nome do usuário logado</returns>
        protected virtual UsuarioPerfil UsuarioLogadoPerfil
        {
            get
            {
                Claim claim = GetClaim(ClaimName.UserPerfil);
                return claim == null ? UsuarioPerfil.Garcon : (UsuarioPerfil)Convert.ToInt32(claim.Value);
            }
        }

        public ControllerBase(TAppService appService,
            IHttpContextAccessor httpContextAccessor)
        {
            _appService = appService;
            _httpContext = httpContextAccessor.HttpContext;
        }

        /// <summary>
        /// Responsavel por enviar o erro causado no servidor que produz um Microsoft.AspNetCore.Http.StatusCodes.Status 500
        /// </summary>
        /// 
        /// <returns>Retorna a exceção causada no servidor</returns>
        protected virtual IActionResult Error(Exception ex)
        {
            return StatusCode(500, ex.Message);
        }

        /// <summary>
        /// Responsavel por converter o User.Identity ClaimsIdentity
        /// </summary>
        /// 
        /// <returns>Retorna o primeiro Claim do usuario logado pelo tipo</returns>
        protected virtual Claim GetClaim(string claim)
        {
            if (User == null)
            {
                return null;
            }

            ClaimsIdentity identity = (ClaimsIdentity)_httpContext.User.Identity;
            return identity?.Claims.FirstOrDefault(c => c.Type == claim);
        }

        /// <summary>
        /// Responsavel por converter o User.Identity ClaimsIdentity
        /// </summary>
        /// 
        /// <returns>Retorna todos os Claims do usuario logado</returns>
        protected virtual ClaimsIdentity GetClaimsIdentity()
        {
            return (ClaimsIdentity)User.Identity;
        }

        /// <summary>
        /// Executa uma ação, e retorna um IActionResult
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="funcao"></param>
        /// <returns></returns>
        protected async virtual Task<IActionResult> ExecutarFuncaoAsync<T>(Func<Task<T>> funcao)
        {
            try
            {
                var retorno = await funcao();
                return Ok(retorno);
            }
            catch (Exception ex)
            {
                return Error(ex);
            }
        }

        /// <summary>
        /// Executa uma ação, e retorna um IActionResult com automapper
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="funcao"></param>
        /// <returns></returns>
        protected async virtual Task<IActionResult> ExecutarFuncaoAsync<T, TResult>(Func<Task<T>> funcao)
        {
            try
            {
                var retorno = await funcao();
                return Ok(Mapper.Map<TResult>(retorno));
            }
            catch (Exception ex)
            {
                return Error(ex);
            }
        }
    }
}