using FavoDeMel.Domain.Common;
using FavoDeMel.Domain.Extensions;
using FavoDeMel.Domain.Interfaces;
using FavoDeMel.Domain.Entities.Comandas;
using FavoDeMel.Domain.Entities.Produtos;
using FavoDeMel.Domain.Entities.Usuarios;
using FavoDeMel.EF.Repository.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FavoDeMel.Domain.Helpers;

namespace FavoDeMel.Repository
{
    public class ComandaRepository<TDbContext> : RepositoryBase<TDbContext>, IComandaRepository
        where TDbContext : DbContext, IFavoDeMelDbContext
    {
        protected DbSet<Comanda> ComandaCrud => DbContext.Set<Comanda>();
        protected DbSet<ComandaPedido> ComandaPedidoCrud => DbContext.Set<ComandaPedido>();
        protected IQueryable<Comanda> ComandaSelect => DbContext.Set<Comanda>().AsNoTracking();
        protected IQueryable<ComandaPedido> ComandaPedidoSelect => DbContext.Set<ComandaPedido>().AsNoTracking();
        protected IQueryable<Produto> ProdutoSelect => DbContext.Set<Produto>().AsNoTracking();
        protected IQueryable<Usuario> UsuarioSelect => DbContext.Set<Usuario>().AsNoTracking();

        public ComandaRepository(TDbContext dbContext) : base(dbContext)
        { }

        public async Task<Comanda> ObterPorIdAsync(Guid id)
        {
            return await ComandaSelect
                .Include(c => c.Garcom)
                .Include(c => c.Pedidos)
                    .ThenInclude(c => c.Produto)
                .Where(c => c.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Comanda>> ObterTodosPorSituacaoAsync(ComandaSituacao situacao)
        {
            return await ComandaSelect
                .Include(c => c.Garcom)
                .Include(c => c.Pedidos)
                    .ThenInclude(c => c.Produto)
                .Where(c => c.Situacao == situacao)
                .ToListAsync();
        }

        public virtual async Task<PagedList<Comanda>> ObterPaginadoPorSituacaoAsync(ComandaSituacao situacao, DateTime data, int page = 1, int pageSize = 20)
        {
            var pagedList = new PagedList<Comanda>();

            var comandaData = ComandaSelect.Where(x => x.Situacao == situacao && x.DataCadastro.Date == data.Date);
            var comandaPaginada = await comandaData.PageBy(x => x.Codigo, page, pageSize)
                .Include(c => c.Garcom)
                .Include(c => c.Pedidos)
                    .ThenInclude(c => c.Produto)
                .Where(c => c.Situacao == situacao).ToListAsync();

            var total = await comandaData.CountAsync();

            pagedList.Data.AddRange(comandaPaginada);
            pagedList.TotalCount = total;
            pagedList.PageSize = pageSize;

            return pagedList;
        }

        public async Task<Comanda> CadastrarAsync(Comanda comanda)
        {
            comanda.Garcom = comanda.Garcom == null ? null : await UsuarioSelect.Where(c => c.Id == comanda.Garcom.Id).FirstOrDefaultAsync();
            var existe = await ComandaSelect.AnyAsync(c => c.DataCadastro.Date == comanda.DataCadastro.Date);
            comanda.Codigo = !existe ? "0001" : StringHelper.MaxAddPadLeft(ComandaSelect.Where(c => c.DataCadastro.Date == comanda.DataCadastro.Date).Max(c => c.Codigo), 4);

            DbContext.Entry(comanda).State = EntityState.Added;
            Comanda comandaDb = ComandaCrud.Add(comanda).Entity;
            comanda.Id = comandaDb.Id;

            foreach (var comandaPedido in comanda.Pedidos)
            {
                comandaPedido.ComandaId = comanda.Id;
                comandaPedido.Produto = await ProdutoSelect.Where(c => c.Id == comandaPedido.Produto.Id).FirstOrDefaultAsync();
                DbContext.Entry(comandaPedido).State = EntityState.Added;
            }

            return comanda;
        }

        public async Task<Comanda> EditarAsync(Comanda comanda)
        {
            Comanda comandaDb = await ComandaCrud.Where(c => c.Id == comanda.Id).Include(c => c.Pedidos).FirstOrDefaultAsync();

            foreach (var comandaPedido in comanda.Pedidos)
            {
                comandaPedido.ComandaId = comanda.Id;
                comandaPedido.Produto = await ProdutoSelect.Where(c => c.Id == comandaPedido.Produto.Id).FirstOrDefaultAsync();

                if (comandaPedido.Id == Guid.Empty)
                {
                    DbContext.Entry(comandaPedido).State = EntityState.Added;
                }
                else
                {
                    ComandaPedido comandaPedidoDb = await ComandaPedidoCrud.FindAsync(comandaPedido.Id);
                    DbContext.Entry(comandaPedidoDb).CurrentValues.SetValues(comandaPedido);
                }
            }

            foreach (var comandaPedido in comandaDb.Pedidos.Where(c => !comanda.Pedidos.Any(b => b.Id == c.Id)))
            {
                DbContext.Entry(comandaPedido).State = EntityState.Deleted;
            }

            comanda.Garcom = comanda.Garcom != null ? await UsuarioSelect.Where(c => c.Id == comanda.Garcom.Id).FirstOrDefaultAsync() : null;

            DbContext.Entry(comandaDb)
                .CurrentValues
                .SetValues(comanda);

            return comanda;
        }

        public async Task<Comanda> FecharAsync(Guid comandaId)
        {
            Comanda comanda = await ComandaCrud
               .Include(c => c.Garcom)
               .Include(c => c.Pedidos)
                    .ThenInclude(c => c.Produto)
               .Where(c => c.Id == comandaId).FirstOrDefaultAsync();
            comanda.FecharConta();
            DbContext.Entry(comanda)
               .CurrentValues.SetValues(comanda);
            return comanda;
        }

        public async Task<Comanda> ConfirmarAsync(Guid comandaId)
        {
            Comanda comanda = await ComandaCrud
                .Include(c => c.Garcom)
                .Include(c => c.Pedidos)
                    .ThenInclude(c => c.Produto)
                .Where(c => c.Id == comandaId).FirstOrDefaultAsync();
            comanda.Confirmar();
            DbContext.Entry(comanda)
               .CurrentValues.SetValues(comanda);
            return comanda;
        }

        public async Task<PagedList<Comanda>> ObterTodosPaginado(int page = 1, int pageSize = 20)
        {
            var pagedList = new PagedList<Comanda>();

            var itensPaginado = await ComandaSelect.PageBy(x => x.Id, page, pageSize).ToListAsync();
            var total = await ComandaSelect.CountAsync();

            pagedList.Data.AddRange(itensPaginado);
            pagedList.TotalCount = total;
            pagedList.PageSize = pageSize;

            return pagedList;
        }

        public bool Exists(Guid id)
        {
            return ComandaSelect.Any(e => e.Id.Equals(id));
        }
    }
}
