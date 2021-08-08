using AutoMapper;
using FavoDeMel.Domain.Common;
using FavoDeMel.Domain.Entities.Usuarios;
using System.Collections.Generic;

namespace FavoDeMel.Domain.Dtos.Mappers
{
    public static class UsuarioMappers
    {
        static UsuarioMappers()
        {
            Mapper = new MapperConfiguration(cfg => cfg.AddProfile<UsuarioMapProfile>()).CreateMapper();
        }

        internal static IMapper Mapper { get; }

        public static UsuarioDto ToDto(this Usuario resource)
        {
            return resource == null ? null : Mapper.Map<UsuarioDto>(resource);
        }

        public static Usuario ToEntity(this UsuarioDto resource)
        {
            return resource == null ? null : Mapper.Map<Usuario>(resource);
        }

        public static IEnumerable<UsuarioDto> ToListDto(this IEnumerable<Usuario> resource)
        {
            return Mapper.Map<IEnumerable<UsuarioDto>>(resource);
        }

        public static PaginacaoDto<UsuarioDto> ToPaginacaoDto<T>(this PagedList<Usuario> source)
        {
            return Mapper.Map<PaginacaoDto<UsuarioDto>>(source);
        }
    }
}
