using FavoDeMel.Api.Controllers.Common;
using FavoDeMel.Api.Dtos;
using FavoDeMel.Domain.Comandas;
using FavoDeMel.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace FavoDeMel.Api.Controllers
{
    public class ComandaController : ControllerBase<Comanda, int, ComandaDto, IComandaService>
    {
        public ComandaController(IComandaService service)
            : base(service)
        { }

        /// <summary>
        /// Responsável por obter todos as comandas em aberta
        /// </summary>
        /// <returns>Lista das comandas em amberto</returns>
        [HttpGet("{pageNumber}/{pageSize}")]
        public virtual async Task<IActionResult> ComandasAbertas(int pageNumber, int pageSize)
        {
            try
            {
                var source = _appService.GetComandasAbertas();
                int count = await source.CountAsync();
                int TotalCount = count;
                var items = await source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

                return Ok(new
                {
                    Items = items,
                    Total = TotalCount,
                    PageSize = pageSize,
                    currentPage = pageNumber,
                });
            }
            catch (Exception ex)
            {
                return Error(ex);
            }
        }
    }
}
