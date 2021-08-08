using AutoMapper;
using FavoDeMel.Domain.Common;
using FavoDeMel.Domain.Entities.Usuarios;

namespace FavoDeMel.Domain.Dtos
{
    public class UsuarioMapProfile : Profile
    {
        public UsuarioMapProfile()
        {
            CreateMap<Usuario, UsuarioDto>(MemberList.Destination).ReverseMap();
            CreateMap<PagedList<Usuario>, PaginacaoDto<UsuarioDto>>(MemberList.Destination)
             .ForMember(c => c.Itens, opt => opt.MapFrom(x => x.Data));
        }
    }
}
