using FavoDeMel.Api.Controllers.Common;
using FavoDeMel.Api.Dtos;
using FavoDeMel.Domain.Produtos;
using FavoDeMel.Service.Interfaces;

namespace FavoDeMel.Api.Controllers
{
    public class ProdutoController : ControllerBase<Produto, int, ProdutoDto, IProdutoService>
    {
        public ProdutoController(IProdutoService service) : base(service)
        { }
    }
}
