using FavoDeMel.Domain.Produtos;
using FavoDeMel.Service.Common;
using FavoDeMel.Service.Interfaces;

namespace FavoDeMel.Service.Services
{
    public class ProdutoService : ServiceBase<int, Produto, IProdutoRepository>, IProdutoService
    {
        public ProdutoService(IProdutoRepository repository) : base(repository)
        { }
    }
}
