using FavoDeMel.Domain.Fake;
using FavoDeMel.Domain.Produtos;
using FavoDeMel.Service.Interfaces;
using FavoDeMel.Tests.Mocks.Parameters;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace FavoDeMel.Tests.Services
{
    public class ProdutoServiceTest
    {
        private ServiceProvider _serviceProvider;

        [SetUp]
        public void Setup()
        {
            MockProdutoParameter parameter = new MockProdutoParameter()
            {
                Exists = false,
                Produto = EntityFake.Obter<Produto>(),
                Produtos = EntityFake.ObterTodos<Produto>(),
                NomeJaCadastrado = false,
            };
            _serviceProvider = Startup.GetServiceProvider(new ServiceParameter(parameter));
        }

        [Test]
        public async Task DeveCadastarProdutoValido()
        {
            using (IProdutoService produtoService = _serviceProvider.GetRequiredService<IProdutoService>())
            {
                var usuario = await produtoService.Inserir(new Produto
                {
                    Nome = "Teste",
                    Preco = Convert.ToDecimal(10.5)
                });
                usuario.Should().NotBeNull();
            }
        }

        [Test]
        public async Task NaoDeveCadastarCasoNomeNulo()
        {
            using (IProdutoService produtoService = _serviceProvider.GetRequiredService<IProdutoService>())
            {
                var usuario = await produtoService.Inserir(new Produto
                {
                    Nome = null,
                    Preco = Convert.ToDecimal(10.5)
                });
                usuario.Should().BeNull();
                produtoService.MensagensValidacao.Should().NotBeNull();
                produtoService.MensagensValidacao.Any(c => c == ProdutoMessage.NomeObrigatorio).Should().BeTrue();
            }
        }

        [Test]
        public async Task NaoDeveCadastarCasoPrecoInvalido()
        {
            using (IProdutoService produtoService = _serviceProvider.GetRequiredService<IProdutoService>())
            {
                var usuario = await produtoService.Inserir(new Produto
                {
                    Nome = "Coca Cola",
                    Preco = 0
                });
                usuario.Should().BeNull();
                produtoService.MensagensValidacao.Should().NotBeNull();
                produtoService.MensagensValidacao.Any(c => c == ProdutoMessage.PrecoObrigatorio).Should().BeTrue();
            }
        }

        [Test]
        public async Task DeveEditarProdutoValido()
        {
            using (IProdutoService produtoService = _serviceProvider.GetRequiredService<IProdutoService>())
            {
                var usuario = await produtoService.Editar(new Produto
                {
                    Id = 1,
                    Nome = "Teste",
                    Preco = Convert.ToDecimal(10.5)
                });
                usuario.Should().NotBeNull();
            }
        }

        [Test]
        public async Task NaoDeveEditarCasoNomeNulo()
        {
            using (IProdutoService produtoService = _serviceProvider.GetRequiredService<IProdutoService>())
            {
                var usuario = await produtoService.Editar(new Produto
                {
                    Id = 1,
                    Nome = null,
                    Preco = Convert.ToDecimal(10.5)
                });
                usuario.Should().BeNull();
                produtoService.MensagensValidacao.Should().NotBeNull();
                produtoService.MensagensValidacao.Any(c => c == ProdutoMessage.NomeObrigatorio).Should().BeTrue();
            }
        }

        [Test]
        public async Task NaoDeveEditarCasoPrecoInvalido()
        {
            using (IProdutoService produtoService = _serviceProvider.GetRequiredService<IProdutoService>())
            {
                var usuario = await produtoService.Editar(new Produto
                {
                    Id = 1,
                    Nome = "Coca Cola",
                    Preco = 0
                });
                usuario.Should().BeNull();
                produtoService.MensagensValidacao.Should().NotBeNull();
                produtoService.MensagensValidacao.Any(c => c == ProdutoMessage.PrecoObrigatorio).Should().BeTrue();
            }
        }
    }
}