using FavoDeMel.Domain.Entities.Usuarios;
using System.Collections.Generic;
using System.Linq;

namespace FavoDeMel.Domain.Dtos.Filtros
{
    public class FiltroUsuario : Filtro
    {
        public string Usuarios { get; set; }
        public UsuarioPerfil? Perfil { get; set; }

        public IList<int> UsuariosIds
        {
            get
            {
                if (Usuarios == null)
                {
                    return new List<int>();
                }

                return Usuarios.Split(',').Select(c => int.Parse(c)).ToList();
            }
        }
    }
}