using FavoDeMel.Domain.Produtos;
using FavoDeMel.Domain.Usuarios;
using FavoDeMel.Framework.Helpers;
using System;
using System.Collections;
using System.Collections.Generic;

namespace FavoDeMel.Domain.Fake
{
    public class EntityFake
    {
        //TODO - Usuar somente em ambiente de desenvolvimento
        public static IEnumerable<Produto> ObterListaDeProdutos()
        {
            IList<Produto> produtos = new List<Produto>();
            produtos.Add(new Produto { Nome = "Coca Cola 2ML", Preco = Convert.ToDecimal(15) });
            produtos.Add(new Produto { Nome = "Guaraná 2ML", Preco = Convert.ToDecimal(10) });
            produtos.Add(new Produto { Nome = "Coca Cola 350ML", Preco = Convert.ToDecimal(5.50) });
            produtos.Add(new Produto { Nome = "Guaraná 350ML", Preco = Convert.ToDecimal(5.50) });
            produtos.Add(new Produto { Nome = "Heineken 250ML", Preco = Convert.ToDecimal(10) });
            produtos.Add(new Produto { Nome = "Pizza G", Preco = Convert.ToDecimal(70) });
            produtos.Add(new Produto { Nome = "Pizza M", Preco = Convert.ToDecimal(55.90) });
            produtos.Add(new Produto { Nome = "Pizza P", Preco = Convert.ToDecimal(35.90) });
            return produtos;
        }

        //TODO - Usuar somente em ambiente de desenvolvimento
        public static IEnumerable<Usuario> ObterListaDeUsuarios()
        {
            IList<Usuario> produtos = new List<Usuario>();
            produtos.Add(GerarUsuario("Admin", UsuarioPerfil.Administrador));
            produtos.Add(GerarUsuario("Garçom", UsuarioPerfil.Garcom));
            produtos.Add(GerarUsuario("Cozinheiro", UsuarioPerfil.Cozinheiro));
            return produtos;
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
