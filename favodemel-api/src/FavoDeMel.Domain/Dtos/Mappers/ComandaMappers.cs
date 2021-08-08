using AutoMapper;
using FavoDeMel.Domain.Common;
using FavoDeMel.Domain.Entities.Comandas;
using System.Collections.Generic;

namespace FavoDeMel.Domain.Dtos.Mappers
{
    public static class ComandaMappers
    {
        static ComandaMappers()
        {
            Mapper = new MapperConfiguration(cfg => cfg.AddProfile<ComandaMapProfile>()).CreateMapper();
        }

        internal static IMapper Mapper { get; }

        public static ComandaDto ToDto(this Comanda resource)
        {
            return resource == null ? null : Mapper.Map<ComandaDto>(resource);
        }

        public static Comanda ToEntity(this ComandaDto resource)
        {
            return resource == null ? null : Mapper.Map<Comanda>(resource);
        }

        public static IEnumerable<ComandaDto> ToListDto(this IEnumerable<Comanda> resource)
        {
            return Mapper.Map<IEnumerable<ComandaDto>>(resource);
        }

        public static PaginacaoDto<ComandaDto> ToPaginacaoDto<T>(this PagedList<Comanda> source)
        {
            return Mapper.Map<PaginacaoDto<ComandaDto>>(source);
        }
    }
}
