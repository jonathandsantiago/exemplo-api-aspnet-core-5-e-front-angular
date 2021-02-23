using AutoMapper;
using FavoDeMel.Domain.Produtos;

namespace FavoDeMel.Api.Dtos.Mappers
{
    public class ProdutoMapProfile : Profile
    {
        public ProdutoMapProfile()
        {
            CreateMap<Produto, ProdutoDto>().ReverseMap();
        }
    }
}
