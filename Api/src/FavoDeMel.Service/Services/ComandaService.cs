using FavoDeMel.Domain.Comandas;
using FavoDeMel.Domain.Dtos;
using FavoDeMel.Domain.Models;
using FavoDeMel.Service.Common;
using FavoDeMel.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FavoDeMel.Service.Services
{
    public class ComandaService : ServiceBase<int, Comanda, IComandaRepository>, IComandaService
    {
        public ComandaService(IComandaRepository repository) : base(repository)
        { }

        public async Task<Comanda> InserirOuEditar(Comanda comanda)
        {
            using (var dbTransaction = _repository.BeginTransaction(_validador))
            {
                if (_validador != null && !await ObterValidador<ComandaValidator>().Validar(comanda))
                {
                    return null;
                }

                comanda.Prepare();

                if (comanda.Id > 0)
                {
                    await _repository.Editar(comanda);
                }
                else
                {
                    await _repository.Inserir(comanda);
                }

                return await _repository.ObterPorId(comanda.Id);
            }
        }

        public async Task<IEnumerable<Comanda>> GetComandasAbertas()
        {
            return await _repository.ObterComandasAbertas();
        }

        public async Task<PaginacaoDto<ComandaDto>> ObterTodosPaginado(FiltroComanda filtro)
        {
            int pagina = filtro.Pagina;
            var comandas = ObterAsQueryable()
               .Where(c => !filtro.ComandasIds.Any() || filtro.ComandasIds.Contains(c.Id));
            int total = await comandas.CountAsync();

            if (total == 0)
            {
                return new PaginacaoDto<ComandaDto>();
            }

            var items = filtro.Limite > 0 && total >= filtro.Limite ? await comandas.Skip(pagina * filtro.Limite).Take(filtro.Limite).ToListAsync() :
                await comandas.ToListAsync();

            return new PaginacaoDto<ComandaDto>(total, filtro.Limite, pagina)
                .SetItens(items.OrderByDescending(c => c.Id).ToList());
        }
    }
}
