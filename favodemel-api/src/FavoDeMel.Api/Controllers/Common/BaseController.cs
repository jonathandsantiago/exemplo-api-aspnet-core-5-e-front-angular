using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace FavoDeMel.Api.Controllers.Common
{
    [ApiController]
    public class BaseController : ControllerBase
    {
        /// <summary>
        /// 
        /// </summary>
        public BaseController()
        { }

        /// <summary>
        /// Retorna o object resultado no response com status code 200
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        protected new IActionResult Response(object result = null)
        {
            return Ok(result);
        }

        /// <summary>
        /// Executa uma ação ao serviço
        /// </summary>
        /// <typeparam name="T">Tipo do retorna da ação</typeparam>
        /// <param name="funcao">Função a ser executada</param>
        /// <returns></returns>
        protected IActionResult ExecutarFuncao<T>(Func<T> funcao)
        {
            var retorno = funcao();
            return Response(retorno);
        }

        /// <summary>
        /// Executa ação ao serviço de forma assíncrona
        /// </summary>
        /// <typeparam name="T">Tipo do retorna da ação</typeparam>
        /// <param name="funcao">Função a ser executada</param>
        /// <returns></returns>
        protected async Task<IActionResult> ExecutarFuncaoAsync<T>(Func<Task<T>> funcao)
        {
            var retorno = await funcao();
            return Response(retorno);
        }

        /// <summary>
        /// Retorna a exceção no response com status code 500
        /// </summary>
        /// <param name="ex">Exceção</param>
        /// <returns></returns>
        protected virtual IActionResult Error(Exception ex)
        {

            return StatusCode(ex.HResult, ex.Message);
        }

        /// <summary>
        /// Executa ação ao serviço de forma assíncrona
        /// </summary>
        /// <param name="acao">Ação a ser executada</param>
        /// <returns></returns>
        protected IActionResult ExecutarAcao(Action acao)
        {
            acao();
            return Response();
        }

        /// <summary>
        /// Retorna o ip remoto da requisição
        /// </summary>
        /// <returns>Ip remoto da requisição</returns>
        protected string IpAddress()
        {
            if (Request.Headers.ContainsKey("X-Forwarded-For"))
                return Request.Headers["X-Forwarded-For"];
            else
                return HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
        }
    }
}