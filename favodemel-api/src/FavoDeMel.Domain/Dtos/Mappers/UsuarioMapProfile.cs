using AutoMapper;
using FavoDeMel.Domain.Usuarios;

namespace FavoDeMel.Domain.Dtos
{
    public class UsuarioMapProfile : Profile
    {
        public UsuarioMapProfile()
        {
            CreateMap<Usuario, UsuarioDto>().ReverseMap();
        }
    }
}
