using AutoMapper;
using FavoDeMel.Domain.Produtos;

namespace FavoDeMel.Domain.Dtos
{
    public class ProdutoMapProfile : Profile
    {
        public ProdutoMapProfile()
        {
            CreateMap<Produto, ProdutoDto>().ReverseMap();
        }
    }
}
