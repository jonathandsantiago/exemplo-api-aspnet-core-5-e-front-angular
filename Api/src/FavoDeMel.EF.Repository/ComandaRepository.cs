using FavoDeMel.Domain.Comandas;
using FavoDeMel.Domain.Produtos;
using FavoDeMel.Domain.Usuarios;
using FavoDeMel.EF.Repository.Common;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FavoDeMel.EF.Repository
{
    public class ComandaRepository : RepositoryBase<int, Comanda>, IComandaRepository
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly IUsuarioRepository _usuarioRepository;
        protected DbSet<ComandaPedido> _comandaPedidoSet { get { return DbContext.Set<ComandaPedido>(); } }

        public ComandaRepository(BaseDbContext dbContext,
            IProdutoRepository produtoRepository,
            IUsuarioRepository usuarioRepository) : base(dbContext)
        {
            _produtoRepository = produtoRepository;
            _usuarioRepository = usuarioRepository;
        }

        public async Task<IEnumerable<Comanda>> ObterTodosPorSituacao(ComandaSituacao situacao)
        {
            return await _dbSet
                .Include(c => c.Garcom)
                .Include(c => c.Pedidos)
                    .ThenInclude(c => c.Produto)
                .Where(c => c.Situacao == situacao)
                .ToListAsync();
        }

        public override async Task Inserir(Comanda comanda)
        {
            comanda.Garcom = comanda.Garcom == null ? null : await _usuarioRepository.ObterPorId(comanda.Garcom.Id);

            foreach (var comandaPedido in comanda.Pedidos)
            {
                comandaPedido.Produto = await _produtoRepository.ObterPorId(comandaPedido.Produto.Id);
                DbContext.Entry(comandaPedido).State = EntityState.Added;
            }

            DbContext.Entry(comanda).State = EntityState.Added;
            Comanda comandaDb = _dbSet.Add(comanda).Entity;
            await DbContext.SaveChangesAsync();
            comanda.Id = comandaDb.Id;
        }

        public override async Task Editar(Comanda comanda)
        {
            Comanda comandaDb = await _dbSet.Where(c => c.Id == comanda.Id).Include(c => c.Pedidos).FirstOrDefaultAsync();

            foreach (var comandaPedido in comanda.Pedidos)
            {
                comandaPedido.Produto = await _produtoRepository.ObterPorId(comandaPedido.Produto.Id);

                if (comandaPedido.Id == 0)
                {
                    DbContext.Entry(comandaPedido).State = EntityState.Added;
                }
                else
                {
                    ComandaPedido comandaPedidoDb = await _comandaPedidoSet.FindAsync(comandaPedido.Id);
                    DbContext.Entry(comandaPedidoDb).CurrentValues.SetValues(comandaPedido);
                }
            }

            foreach (var comandaPedido in comandaDb.Pedidos.Where(c => !comanda.Pedidos.Any(b => b.Id == c.Id)))
            {
                DbContext.Entry(comandaPedido).State = EntityState.Deleted;
            }

            comanda.Garcom = comanda.Garcom != null ? await _usuarioRepository.ObterPorId(comanda.Garcom.Id) : null;

            DbContext.Entry(comandaDb)
                .CurrentValues
                .SetValues(comanda);
            await DbContext.SaveChangesAsync();
        }

        public async Task<Comanda> Fechar(int comandaId)
        {
            Comanda comanda = await _dbSet
               .Include(c => c.Garcom)
               .Include(c => c.Pedidos)
                    .ThenInclude(c => c.Produto)
               .Where(c => c.Id == comandaId).FirstOrDefaultAsync();
            comanda.FecharConta();
            DbContext.Entry(comanda)
               .CurrentValues.SetValues(comanda);
            await DbContext.SaveChangesAsync();
            return comanda;
        }

        public async Task<Comanda> Confirmar(int comandaId)
        {
            Comanda comanda = await _dbSet
                .Include(c => c.Garcom)
                .Include(c => c.Pedidos)
                    .ThenInclude(c => c.Produto)
                .Where(c => c.Id == comandaId).FirstOrDefaultAsync();
            comanda.Confirmar();
            DbContext.Entry(comanda)
               .CurrentValues.SetValues(comanda);
            await DbContext.SaveChangesAsync();
            return comanda;
        }
    }
}
