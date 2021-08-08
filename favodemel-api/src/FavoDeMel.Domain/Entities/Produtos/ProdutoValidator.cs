using FavoDeMel.Domain.Common;
using FavoDeMel.Domain.Dtos;
using System;
using System.Threading.Tasks;

namespace FavoDeMel.Domain.Entities.Produtos
{
    public class ProdutoValidator : ValidatorBase<ProdutoDto>
    {
        private readonly IProdutoRepository _repository;

        public ProdutoValidator(IProdutoRepository repository)
        {
            _repository = repository;
        }

        public override async Task<bool> Validar(ProdutoDto produto)
        {
            if (string.IsNullOrEmpty(produto.Nome))
            {
                AddMensagem(ProdutoMessage.NomeObrigatorio);
            }
            else if (await _repository.NomeJaCadastrado(produto.Id ?? Guid.Empty, produto.Nome))
            {
                AddMensagem(ProdutoMessage.NomeJaCadastrado);
            }

            if (produto.Preco <= 0)
            {
                AddMensagem(ProdutoMessage.PrecoObrigatorio);
            }

            return await base.Validar(produto);
        }
    }
}
