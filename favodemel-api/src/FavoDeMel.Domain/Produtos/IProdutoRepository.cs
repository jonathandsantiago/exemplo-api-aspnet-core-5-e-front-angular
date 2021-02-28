using FavoDeMel.Domain.Interfaces;
using System.Threading.Tasks;

namespace FavoDeMel.Domain.Produtos
{
    public interface IProdutoRepository : IRepositoryBase<int, Produto>
    {
        Task<bool> NomeJaCadastrado(int id, string nome);
    }
}
