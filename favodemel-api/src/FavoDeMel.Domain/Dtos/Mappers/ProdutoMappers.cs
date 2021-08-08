using AutoMapper;
using FavoDeMel.Domain.Common;
using FavoDeMel.Domain.Entities.Produtos;
using System.Collections.Generic;

namespace FavoDeMel.Domain.Dtos.Mappers
{
    public static class ProdutoMappers
    {
        static ProdutoMappers()
        {
            Mapper = new MapperConfiguration(cfg => cfg.AddProfile<ProdutoMapProfile>()).CreateMapper();
        }

        internal static IMapper Mapper { get; }

        public static ProdutoDto ToDto(this Produto resource)
        {
            return resource == null ? null : Mapper.Map<ProdutoDto>(resource);
        }

        public static Produto ToEntity(this ProdutoDto resource)
        {
            return resource == null ? null : Mapper.Map<Produto>(resource);
        }

        public static IEnumerable<ProdutoDto> ToListDto(this IEnumerable<Produto> resource)
        {
            return Mapper.Map<IEnumerable<ProdutoDto>>(resource);
        }

        public static PaginacaoDto<ProdutoDto> ToPaginacaoDto<T>(this PagedList<Produto> source)
        {
            return Mapper.Map<PaginacaoDto<ProdutoDto>>(source);
        }
    }
}
