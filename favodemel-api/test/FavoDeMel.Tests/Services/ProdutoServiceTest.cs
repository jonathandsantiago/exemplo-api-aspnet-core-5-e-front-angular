using FavoDeMel.Domain.Dtos;
using FavoDeMel.Domain.Helpers;
using FavoDeMel.Domain.Entities.Produtos;
using FavoDeMel.Service.Interfaces;
using FavoDeMel.Tests.Mocks;
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
                Produto = MockHelper.Obter<Produto>(),
                Produtos = MockHelper.ObterTodos<Produto>(),
                NomeJaCadastrado = false,
            };
            _serviceProvider = Startup.GetServiceProvider(new ServiceParameter(parameter));
        }

        [Test]
        public async Task DeveCadastarProdutoValido()
        {
            IProdutoService produtoService = _serviceProvider.GetRequiredService<IProdutoService>();
            var usuario = await produtoService.CadastrarAsync(new ProdutoDto
            {
                Nome = "Teste",
                Preco = Convert.ToDecimal(10.5)
            });
            usuario.Should().NotBeNull(StringHelper.JoinHtmlMensagem(produtoService.MensagensValidacao));
        }

        [Test]
        public async Task NaoDeveCadastarCasoNomeNulo()
        {
            IProdutoService produtoService = _serviceProvider.GetRequiredService<IProdutoService>();
            var usuario = await produtoService.CadastrarAsync(new ProdutoDto
            {
                Nome = null,
                Preco = Convert.ToDecimal(10.5)
            });
            usuario.Should().BeNull();
            produtoService.MensagensValidacao.Should().NotBeNull(StringHelper.JoinHtmlMensagem(produtoService.MensagensValidacao));
            produtoService.MensagensValidacao.Any(c => c == ProdutoMessage.NomeObrigatorio).Should().BeTrue();
        }

        [Test]
        public async Task NaoDeveCadastarCasoPrecoInvalido()
        {
            IProdutoService produtoService = _serviceProvider.GetRequiredService<IProdutoService>();
            var usuario = await produtoService.CadastrarAsync(new ProdutoDto
            {
                Nome = "Coca Cola",
                Preco = 0
            });
            usuario.Should().BeNull();
            produtoService.MensagensValidacao.Should().NotBeNull(StringHelper.JoinHtmlMensagem(produtoService.MensagensValidacao));
            produtoService.MensagensValidacao.Any(c => c == ProdutoMessage.PrecoObrigatorio).Should().BeTrue();
        }

        [Test]
        public async Task DeveEditarProdutoValido()
        {
            IProdutoService produtoService = _serviceProvider.GetRequiredService<IProdutoService>();
            var usuario = await produtoService.EditarAsync(new ProdutoDto
            {
                Id = Guid.NewGuid(),
                Nome = "Teste",
                Preco = Convert.ToDecimal(10.5)
            });
            usuario.Should().NotBeNull(StringHelper.JoinHtmlMensagem(produtoService.MensagensValidacao));
        }

        [Test]
        public async Task NaoDeveEditarCasoNomeNulo()
        {
            IProdutoService produtoService = _serviceProvider.GetRequiredService<IProdutoService>();
            var usuario = await produtoService.EditarAsync(new ProdutoDto
            {
                Id = Guid.NewGuid(),
                Nome = null,
                Preco = Convert.ToDecimal(10.5)
            });
            usuario.Should().BeNull();
            produtoService.MensagensValidacao.Should().NotBeNull(StringHelper.JoinHtmlMensagem(produtoService.MensagensValidacao));
            produtoService.MensagensValidacao.Any(c => c == ProdutoMessage.NomeObrigatorio).Should().BeTrue();
        }

        [Test]
        public async Task NaoDeveEditarCasoPrecoInvalido()
        {
            IProdutoService produtoService = _serviceProvider.GetRequiredService<IProdutoService>();
            var usuario = await produtoService.EditarAsync(new ProdutoDto
            {
                Id = Guid.NewGuid(),
                Nome = "Coca Cola",
                Preco = 0
            });
            usuario.Should().BeNull();
            produtoService.MensagensValidacao.Should().NotBeNull(StringHelper.JoinHtmlMensagem(produtoService.MensagensValidacao));
            produtoService.MensagensValidacao.Any(c => c == ProdutoMessage.PrecoObrigatorio).Should().BeTrue();
        }
    }
}