using FavoDeMel.Domain.Comandas;
using FavoDeMel.Domain.Interfaces;
using FavoDeMel.Domain.Produtos;
using FavoDeMel.Domain.Usuarios;
using FavoDeMel.Tests.Mocks.Parameters;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System.Threading.Tasks;

namespace FavoDeMel.Tests.Mocks
{
    public static class RepositoryMock
    {
        public static IServiceCollection AddRepositoryMock(this IServiceCollection services, ServiceParameter serviceParameter)
        {
            services.AddScoped<IUsuarioRepository>(provider => ObterUsuarioRepositoryMock(serviceParameter.UsuarioParameter));
            services.AddScoped<IProdutoRepository>(provider => ObterProdutoRepositoryMock(serviceParameter.ProdutoParameter));
            services.AddScoped<IComandaRepository>(provider => ObterComandaRepositoryMock(serviceParameter.ComandaParameter));

            return services;
        }

        public static IUsuarioRepository ObterUsuarioRepositoryMock(MockUsuarioParameter parameter)
        {
            var mock = new Mock<IUsuarioRepository>();
            mock.Setup(c => c.Login(It.IsAny<string>(), It.IsAny<string>())).Returns(Task.FromResult(parameter.Usuario));
            mock.Setup(c => c.ExistsLogin(It.IsAny<string>())).Returns(Task.FromResult(parameter.ExistsLogin));
            mock.Setup(c => c.ObterTodosPorPerfil(It.IsAny<UsuarioPerfil>())).Returns(Task.FromResult(parameter.Usuarios));

            mock.Setup(c => c.Editar(It.IsAny<Usuario>()));
            mock.Setup(c => c.Inserir(It.IsAny<Usuario>()));
            mock.Setup(c => c.Excluir(It.IsAny<int>()));
            mock.Setup(c => c.Exists(It.IsAny<int>())).Returns(parameter.Exists);
            mock.Setup(c => c.ObterPorId(It.IsAny<int>())).Returns(Task.FromResult(parameter.Usuario));

            mock.Setup(c => c.InstanceUnitOfWork(It.IsAny<IValidator>()));
            mock.Setup(c => c.SetUnitOfWork(It.IsAny<IUnitOfWork>()));
            mock.Setup(c => c.BeginTransaction(It.IsAny<IValidator>()));
            mock.Setup(c => c.ObterTodos()).Returns(Task.FromResult(parameter.Usuarios));
            mock.Setup(c => c.ObterAsQueryable());
            return mock.Object;
        }

        public static IProdutoRepository ObterProdutoRepositoryMock(MockProdutoParameter parameter)
        {
            var mock = new Mock<IProdutoRepository>();
            mock.Setup(c => c.NomeJaCadastrado(It.IsAny<int>(), It.IsAny<string>())).Returns(Task.FromResult(parameter.NomeJaCadastrado));

            mock.Setup(c => c.Editar(It.IsAny<Produto>()));
            mock.Setup(c => c.Inserir(It.IsAny<Produto>()));
            mock.Setup(c => c.Excluir(It.IsAny<int>()));
            mock.Setup(c => c.Exists(It.IsAny<int>())).Returns(parameter.Exists);
            mock.Setup(c => c.ObterPorId(It.IsAny<int>())).Returns(Task.FromResult(parameter.Produto));

            mock.Setup(c => c.InstanceUnitOfWork(It.IsAny<IValidator>()));
            mock.Setup(c => c.SetUnitOfWork(It.IsAny<IUnitOfWork>()));
            mock.Setup(c => c.BeginTransaction(It.IsAny<IValidator>()));
            mock.Setup(c => c.ObterTodos()).Returns(Task.FromResult(parameter.Produtos));
            mock.Setup(c => c.ObterAsQueryable());
            return mock.Object;
        }

        public static IComandaRepository ObterComandaRepositoryMock(MockComandaParameter parameter)
        {
            var mock = new Mock<IComandaRepository>();
            mock.Setup(c => c.ObterTodosPorSituacao(It.IsAny<ComandaSituacao>())).Returns(Task.FromResult(parameter.Comandas));
            mock.Setup(c => c.Fechar(It.IsAny<int>())).Returns(Task.FromResult(parameter.Comanda));
            mock.Setup(c => c.Confirmar(It.IsAny<int>())).Returns(Task.FromResult(parameter.Comanda));

            mock.Setup(c => c.Editar(It.IsAny<Comanda>()));
            mock.Setup(c => c.Inserir(It.IsAny<Comanda>()));
            mock.Setup(c => c.Excluir(It.IsAny<int>()));
            mock.Setup(c => c.Exists(It.IsAny<int>())).Returns(parameter.Exists);
            mock.Setup(c => c.ObterPorId(It.IsAny<int>())).Returns(Task.FromResult(parameter.Comanda));

            mock.Setup(c => c.InstanceUnitOfWork(It.IsAny<IValidator>()));
            mock.Setup(c => c.SetUnitOfWork(It.IsAny<IUnitOfWork>()));
            mock.Setup(c => c.BeginTransaction(It.IsAny<IValidator>()));
            mock.Setup(c => c.ObterTodos()).Returns(Task.FromResult(parameter.Comandas));
            mock.Setup(c => c.ObterAsQueryable());
            return mock.Object;
        }
    }
}