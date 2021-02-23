using AutoMapper;
using FavoDeMel.Domain.Comandas;

namespace FavoDeMel.Api.Dtos.Mappers
{
    public class ComandaMapProfile : Profile
    {
        public ComandaMapProfile()
        {
            CreateMap<Comanda, ComandaDto>().ReverseMap();
            CreateMap<ComandaPedido, ComandaPedidoDto>().ReverseMap();
        }
    }
}
