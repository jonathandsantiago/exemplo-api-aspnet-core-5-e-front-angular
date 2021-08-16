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
        /// 
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        protected new IActionResult Response(object result = null)
        {
            return Ok(result);
        }

        /// <summary>
        /// Executa uma ação, e retorna um IActionResult
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="funcao"></param>
        /// <returns></returns>
        protected IActionResult ExecutarFuncao<T>(Func<T> funcao)
        {
            var retorno = funcao();
            return Response(retorno);
        }

        /// <summary>
        /// Executa uma ação, e retorna um IActionResult
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="funcao"></param>
        /// <returns></returns>
        protected async Task<IActionResult> ExecutarFuncaoAsync<T>(Func<Task<T>> funcao)
        {
            var retorno = await funcao();
            return Response(retorno);
        }

        protected virtual IActionResult Error(Exception ex)
        {

            return StatusCode(ex.HResult, ex.Message);
        }

        /// <summary>
        /// Executa uma ação, e retorna um HttpResponseMessage
        /// </summary>
        /// <param name="acao"></param>
        /// <returns></returns>
        protected IActionResult ExecutarAcao(Action acao)
        {
            acao();
            return Response();
        }
    }
}