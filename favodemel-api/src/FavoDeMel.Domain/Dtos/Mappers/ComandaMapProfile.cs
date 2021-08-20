using AutoMapper;
using FavoDeMel.Domain.Common;
using FavoDeMel.Domain.Entities.Comandas;

namespace FavoDeMel.Domain.Dtos
{
    public class ComandaMapProfile : Profile
    {
        public ComandaMapProfile()
        {
            CreateMap<Comanda, ComandaDto>(MemberList.Destination).ReverseMap();
            CreateMap<ComandaPedido, ComandaPedidoDto>(MemberList.Destination)
                .ForMember(c => c.Total, c => c.Ignore())
                .ReverseMap();
            CreateMap<PagedList<Comanda>, PaginacaoDto<ComandaDto>>(MemberList.Destination)
             .ForMember(c => c.Itens, opt => opt.MapFrom(x => x.Data))
             .ForMember(c => c.Total, opt => opt.MapFrom(x => x.TotalCount))
             .ForMember(c => c.Limite, opt => opt.MapFrom(x => x.PageSize));
        }
    }
}
