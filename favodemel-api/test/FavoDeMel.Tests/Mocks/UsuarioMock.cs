using FavoDeMel.Domain.Helpers;
using FavoDeMel.Domain.Entities.Usuarios;
using System.Collections.Generic;
using System.Linq;

namespace FavoDeMel.Tests.Mocks
{
    public class UsuarioMock
    {
        public static IEnumerable<Usuario> ObterListaDeUsuarios()
        {
            IList<Usuario> produtos = new List<Usuario>();
            produtos.Add(GerarUsuario("Admin", UsuarioPerfil.Administrador));
            produtos.Add(GerarUsuario("Garçom", UsuarioPerfil.Garcom));
            produtos.Add(GerarUsuario("Cozinheiro", UsuarioPerfil.Cozinheiro));
            return produtos;
        }

        public static Usuario ObterUsuarioAdmin()
        {
            return ObterListaDeUsuarios().Where(c => c.Perfil == UsuarioPerfil.Administrador).FirstOrDefault();
        }

        private static Usuario GerarUsuario(string nome, UsuarioPerfil perfil)
        {
            return new Usuario
            {
                Nome = nome,
                Login = nome,
                Password = StringHelper.CalculateMD5Hash(nome),
                Perfil = perfil,
                Ativo = true
            };
        }
    }
}
