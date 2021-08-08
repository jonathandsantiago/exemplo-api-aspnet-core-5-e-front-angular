using AutoMapper;
using FavoDeMel.Domain.Common;
using FavoDeMel.Domain.Entities.Produtos;

namespace FavoDeMel.Domain.Dtos
{
    public class ProdutoMapProfile : Profile
    {
        public ProdutoMapProfile()
        {
            CreateMap<Produto, ProdutoDto>(MemberList.Destination).ReverseMap();
            CreateMap<PagedList<Produto>, PaginacaoDto<ProdutoDto>>(MemberList.Destination)
             .ForMember(c => c.Itens, opt => opt.MapFrom(x => x.Data));
        }
    }
}
