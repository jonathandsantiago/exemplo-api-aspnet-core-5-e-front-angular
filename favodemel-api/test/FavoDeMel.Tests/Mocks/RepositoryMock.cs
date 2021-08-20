using FavoDeMel.Domain.Interfaces;
using FavoDeMel.Domain.Entities.Comandas;
using FavoDeMel.Domain.Entities.Produtos;
using FavoDeMel.Domain.Entities.Usuarios;
using FavoDeMel.Tests.Mocks.Parameters;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Threading.Tasks;

namespace FavoDeMel.Tests.Mocks
{
    public static class RepositoryMock
    {
        public static IServiceCollection AddRepositoryMock(this IServiceCollection services, ServiceParameter serviceParameter)
        {
            services.AddScoped(provider => ObterUsuarioRepositoryMock(serviceParameter.UsuarioParameter));
            services.AddScoped(provider => ObterProdutoRepositoryMock(serviceParameter.ProdutoParameter));
            services.AddScoped(provider => ObterComandaRepositoryMock(serviceParameter.ComandaParameter));

            return services;
        }

        public static IUsuarioRepository ObterUsuarioRepositoryMock(MockUsuarioParameter parameter)
        {
            var mock = new Mock<IUsuarioRepository>();
            mock.Setup(c => c.LoginAsync(It.IsAny<string>(), It.IsAny<string>())).Returns(Task.FromResult(parameter.Usuario));
            mock.Setup(c => c.ExistsLoginAsync(It.IsAny<string>())).Returns(Task.FromResult(parameter.ExistsLogin));
            mock.Setup(c => c.ObterTodosPorPerfilAsync(It.IsAny<UsuarioPerfil>())).Returns(Task.FromResult(parameter.Usuarios));

            mock.Setup(c => c.EditarAsync(It.IsAny<Usuario>())).Returns(Task.FromResult(parameter.Usuario));
            mock.Setup(c => c.CadastrarAsync(It.IsAny<Usuario>())).Returns(Task.FromResult(parameter.Usuario));
            mock.Setup(c => c.ObterPorIdAsync(It.IsAny<Guid>())).Returns(Task.FromResult(parameter.Usuario));

            mock.Setup(c => c.BeginTransaction(It.IsAny<IValidator>()));
            mock.Setup(c => c.ObterTodosPaginadoAsync(It.IsAny<int>(), It.IsAny<int>())).Returns(Task.FromResult(parameter.UsuarioPaginado));
            return mock.Object;
        }

        public static IProdutoRepository ObterProdutoRepositoryMock(MockProdutoParameter parameter)
        {
            var mock = new Mock<IProdutoRepository>();
            mock.Setup(c => c.NomeJaCadastradoAsync(It.IsAny<Guid>(), It.IsAny<string>())).Returns(Task.FromResult(parameter.NomeJaCadastrado));

            mock.Setup(c => c.EditarAsync(It.IsAny<Produto>())).Returns(Task.FromResult(parameter.Produto));
            mock.Setup(c => c.CadastrarAsync(It.IsAny<Produto>())).Returns(Task.FromResult(parameter.Produto));
            mock.Setup(c => c.ObterPorIdAsync(It.IsAny<Guid>())).Returns(Task.FromResult(parameter.Produto));

            mock.Setup(c => c.BeginTransaction(It.IsAny<IValidator>()));
            mock.Setup(c => c.ObterTodosPaginadoAsync(It.IsAny<int>(), It.IsAny<int>())).Returns(Task.FromResult(parameter.ProdutoPaginado));
            return mock.Object;
        }

        public static IComandaRepository ObterComandaRepositoryMock(MockComandaParameter parameter)
        {
            var mock = new Mock<IComandaRepository>();
            mock.Setup(c => c.ObterTodosPorSituacaoAsync(It.IsAny<ComandaSituacao>())).Returns(Task.FromResult(parameter.Comandas));
            mock.Setup(c => c.FecharAsync(It.IsAny<Guid>())).Returns(Task.FromResult(parameter.Comanda));
            mock.Setup(c => c.ConfirmarAsync(It.IsAny<Guid>())).Returns(Task.FromResult(parameter.Comanda));

            mock.Setup(c => c.EditarAsync(It.IsAny<Comanda>())).Returns(Task.FromResult(parameter.Comanda));
            mock.Setup(c => c.CadastrarAsync(It.IsAny<Comanda>())).Returns(Task.FromResult(parameter.Comanda));
            mock.Setup(c => c.Exists(It.IsAny<Guid>())).Returns(parameter.Exists);
            mock.Setup(c => c.ObterPorIdAsync(It.IsAny<Guid>())).Returns(Task.FromResult(parameter.Comanda));

            mock.Setup(c => c.BeginTransaction(It.IsAny<IValidator>()));
            return mock.Object;
        }
    }
}