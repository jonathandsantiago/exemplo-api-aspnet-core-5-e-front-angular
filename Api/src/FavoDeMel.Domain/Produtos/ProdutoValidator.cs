using FavoDeMel.Domain.Common;

namespace FavoDeMel.Domain.Produtos
{
    public class ProdutoValidator : ValidatorBase<int, Produto, IProdutoRepository>
    {
        public ProdutoValidator(IProdutoRepository repository) :
            base(repository)
        { }
    }
}
