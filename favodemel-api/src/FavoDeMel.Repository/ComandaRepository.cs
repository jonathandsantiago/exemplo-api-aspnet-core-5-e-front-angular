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

        public async Task<Comanda> ObterPorId(Guid id)
        {
            return await ComandaSelect
                .Include(c => c.Garcom)
                .Include(c => c.Pedidos)
                    .ThenInclude(c => c.Produto)
                .Where(c => c.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Comanda>> ObterTodosPorSituacao(ComandaSituacao situacao)
        {
            return await ComandaSelect
                .Include(c => c.Garcom)
                .Include(c => c.Pedidos)
                    .ThenInclude(c => c.Produto)
                .Where(c => c.Situacao == situacao)
                .ToListAsync();
        }

        public async Task<Comanda> Inserir(Comanda comanda)
        {
            comanda.Garcom = comanda.Garcom == null ? null : await UsuarioSelect.Where(c => c.Id == comanda.Garcom.Id).FirstOrDefaultAsync();
            var existe = await ComandaSelect.AnyAsync(c => c.DataCadastro.Date == comanda.DataCadastro.Date);
            comanda.Codigo = !existe ? "0001" : StringHelper.MaxAddPadLeft(ComandaSelect.Where(c => c.DataCadastro.Date == comanda.DataCadastro.Date).Max(c => c.Codigo), 4);

            foreach (var comandaPedido in comanda.Pedidos)
            {
                comandaPedido.Comanda = comanda;
                comandaPedido.Produto = await ProdutoSelect.Where(c => c.Id == comandaPedido.Produto.Id).FirstOrDefaultAsync();
                DbContext.Entry(comandaPedido).State = EntityState.Added;
            }

            DbContext.Entry(comanda).State = EntityState.Added;
            Comanda comandaDb = ComandaCrud.Add(comanda).Entity;
            comanda.Id = comandaDb.Id;

            return comanda;
        }

        public async Task<Comanda> Editar(Comanda comanda)
        {
            Comanda comandaDb = await ComandaCrud.Where(c => c.Id == comanda.Id).Include(c => c.Pedidos).FirstOrDefaultAsync();

            foreach (var comandaPedido in comanda.Pedidos)
            {
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

        public async Task<Comanda> Fechar(Guid comandaId)
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

        public async Task<Comanda> Confirmar(Guid comandaId)
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
