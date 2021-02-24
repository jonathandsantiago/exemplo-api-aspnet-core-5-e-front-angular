using FavoDeMel.Domain.Common;
using System.Threading.Tasks;

namespace FavoDeMel.Domain.Produtos
{
    public class ProdutoValidator : ValidatorBase<int, Produto, IProdutoRepository>
    {
        public ProdutoValidator(IProdutoRepository repository) :
            base(repository)
        { }

        public override async Task<bool> Validar(Produto produto)
        {
            if (string.IsNullOrEmpty(produto.Nome))
            {
                AddMensagem("Nome é obrigatório.");
            }
            else if (await _repository.NomeJaCadastrado(produto.Id, produto.Nome))
            {
                AddMensagem("Nome já cadastrado.");
            }

            if (produto.Preco <= 0)
            {
                AddMensagem("Preço é obrigatório.");
            }

            return await base.Validar(produto);
        }
    }
}
