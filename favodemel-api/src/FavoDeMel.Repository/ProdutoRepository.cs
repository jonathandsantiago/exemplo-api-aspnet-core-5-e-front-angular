using FavoDeMel.Domain.Common;
using FavoDeMel.Domain.Extensions;
using FavoDeMel.Domain.Interfaces;
using FavoDeMel.Domain.Entities.Produtos;
using FavoDeMel.EF.Repository.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FavoDeMel.Repository
{
    public class ProdutoRepository<TDbContext> : RepositoryBase<TDbContext>, IProdutoRepository
        where TDbContext : DbContext, IFavoDeMelDbContext
    {
        protected DbSet<Produto> ProdutoCrud => DbContext.Set<Produto>();
        protected IQueryable<Produto> ProdutoSelect => DbContext.Set<Produto>().AsNoTracking();

        public ProdutoRepository(TDbContext dbContext) : base(dbContext)
        { }

        public async Task<bool> NomeJaCadastrado(Guid id, string nome)
        {
            return await ProdutoSelect.AnyAsync(c => c.Id != id && c.Nome == nome);
        }

        public async Task<Produto> EditarAsync(Produto produto)
        {
            ProdutoCrud.Update(produto);
            return await Task.Run(() => produto);
        }

        public async Task<Produto> CadastrarAsync(Produto produto)
        {
            var produtoDb = await ProdutoCrud.AddAsync(produto);
            produto.Id = produtoDb.Entity.Id;
            return produto;
        }

        public async Task<Produto> ObterPorIdAsync(Guid id)
        {
            return await ProdutoSelect.Where(c => c.Id == id).FirstOrDefaultAsync();
        }

        public async Task<PagedList<Produto>> ObterTodosPaginado(int page = 1, int pageSize = 20)
        {
            var pagedList = new PagedList<Produto>();

            var itensPaginado = await ProdutoSelect.PageBy(x => x.Id, page, pageSize).ToListAsync();
            var total = await ProdutoSelect.CountAsync();

            pagedList.Data.AddRange(itensPaginado);
            pagedList.TotalCount = total;
            pagedList.PageSize = pageSize;

            return pagedList;
        }

        public async Task<IEnumerable<Produto>> ObterTodos()
        {
            return await ProdutoSelect.ToListAsync();
        }
    }
}
