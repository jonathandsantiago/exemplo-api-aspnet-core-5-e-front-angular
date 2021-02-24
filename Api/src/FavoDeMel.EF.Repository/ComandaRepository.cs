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

        public ComandaRepository(BaseDbContext dbContext,
            IProdutoRepository produtoRepository,
            IUsuarioRepository usuarioRepository) : base(dbContext)
        {
            _produtoRepository = produtoRepository;
            _usuarioRepository = usuarioRepository;
        }

        public async Task<IEnumerable<Comanda>> ObterComandasAbertas()
        {
            return await _dbSet
                .Include(c => c.Garcom)
                .Include(c => c.Pedidos)
                    .ThenInclude(c => c.Produto)
                .Where(c => c.Situacao == ComandaSituacao.Aberta)
                .ToListAsync();
        }

        public async Task<Comanda> FecharConta(int comandaId)
        {
            Comanda comanda = _dbSet.Find(comandaId);

            comanda.FecharConta();
            DbContext.Entry(comanda)
               .CurrentValues.SetValues(comanda);
            await DbContext.SaveChangesAsync();
            return comanda;
        }

        public override async Task Editar(Comanda comanda)
        {
            Comanda comandaAtual = await ObterPorId(comanda.Id);

            foreach (var comandaPedido in comanda.Pedidos)
            {
                DbContext.Entry(comandaPedido).State = EntityState.Detached;
                comandaPedido.Produto = await _produtoRepository.ObterPorId(comandaPedido.Produto.Id);

                if (!comandaAtual.Pedidos.Any(c => c.Id == comandaPedido.Id))
                {
                    DbContext.Entry(comandaPedido).State = EntityState.Added;
                }
            }

            foreach (var comandaPedido in comandaAtual.Pedidos.Where(c => !comanda.Pedidos.Any(b => b.Id == c.Id)))
            {
                DbContext.Entry(comandaPedido).State = EntityState.Deleted;
            }

            comanda.Garcom = comanda.Garcom != null ? await _usuarioRepository.ObterPorId(comanda.Garcom.Id) : null;

            DbContext.Entry(comandaAtual)
                .CurrentValues
                .SetValues(comanda);
            await DbContext.SaveChangesAsync();
        }
    }
}
