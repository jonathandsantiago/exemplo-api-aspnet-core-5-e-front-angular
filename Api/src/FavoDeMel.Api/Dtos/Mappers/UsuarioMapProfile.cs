using AutoMapper;
using FavoDeMel.Domain.Usuarios;

namespace FavoDeMel.Api.Dtos.Mappers
{
    public class UsuarioMapProfile : Profile
    {
        public UsuarioMapProfile()
        {
            CreateMap<Usuario, UsuarioDto>().ReverseMap();
        }
    }
}
