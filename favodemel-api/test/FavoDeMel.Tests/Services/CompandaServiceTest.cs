using FavoDeMel.Domain.Dtos;
using FavoDeMel.Domain.Helpers;
using FavoDeMel.Domain.Entities.Comandas;
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
    public class CompandaServiceTest
    {
        private ServiceProvider _serviceProvider;

        [SetUp]
        public void Setup()
        {
            MockComandaParameter parameter = new MockComandaParameter()
            {
                Exists = false,
                Comanda = MockHelper.Obter<Comanda>(),
                Comandas = MockHelper.ObterTodos<Comanda>()
            };
            _serviceProvider = Startup.GetServiceProvider(new ServiceParameter(parameter));
        }

        [Test]
        public async Task DeveCadastarComandaValido()
        {
            IComandaService comandaService = _serviceProvider.GetRequiredService<IComandaService>();
            ComandaDto comanda = new ComandaDto
            {
                Situacao = ComandaSituacao.Aberta
            };

            foreach (var produto in ProdutoMock.ObterListaDeProdutos())
            {
                comanda.Pedidos.Add(new ComandaPedidoDto()
                {
                    ProdutoId = Guid.NewGuid(),
                    ProdutoNome = produto.Nome,
                    ProdutoPreco = produto.Preco,
                    Quantidade = 1,
                    Situacao = ComandaPedidoSituacao.Pedido
                });
            }

            var result = await comandaService.CadastrarAsync(comanda);
            result.Should().NotBeNull(StringHelper.JoinHtmlMensagem(comandaService.MensagensValidacao));
        }

        [Test]
        public async Task NaoDeveCadastarCasoSituacaoInValida()
        {
            IComandaService comandaService = _serviceProvider.GetRequiredService<IComandaService>();
            ComandaDto comanda = new ComandaDto();

            foreach (var produto in ProdutoMock.ObterListaDeProdutos())
            {
                comanda.Pedidos.Add(new ComandaPedidoDto()
                {
                    ProdutoId = Guid.NewGuid(),
                    ProdutoNome = produto.Nome,
                    ProdutoPreco = produto.Preco,
                    Quantidade = 1,
                    Situacao = ComandaPedidoSituacao.Pedido
                });
            }

            var result = await comandaService.CadastrarAsync(comanda);
            result.Should().BeNull();
            comandaService.MensagensValidacao.Should().NotBeNull(StringHelper.JoinHtmlMensagem(comandaService.MensagensValidacao));
            comandaService.MensagensValidacao.Any(c => c == ComandaMessage.SituacaoInvalida).Should().BeTrue();
        }

        [Test]
        public async Task NaoDeveCadastarCasoNaoContenhaPedido()
        {
            IComandaService comandaService = _serviceProvider.GetRequiredService<IComandaService>();
            ComandaDto comanda = new ComandaDto
            {
                Situacao = ComandaSituacao.Aberta
            };

            var result = await comandaService.CadastrarAsync(comanda);
            result.Should().BeNull();
            comandaService.MensagensValidacao.Should().NotBeNull(StringHelper.JoinHtmlMensagem(comandaService.MensagensValidacao));
            comandaService.MensagensValidacao.Any(c => c == ComandaMessage.PedidoObrigatorio).Should().BeTrue();
        }

        [Test]
        public async Task NaoDeveCadastarCasoPedidoNaoContenhaProduto()
        {
            IComandaService comandaService = _serviceProvider.GetRequiredService<IComandaService>();
            ComandaDto comanda = new ComandaDto
            {
                Situacao = ComandaSituacao.Aberta
            };

            comanda.Pedidos.Add(new ComandaPedidoDto() { Quantidade = 1, Situacao = ComandaPedidoSituacao.Pedido });

            var result = await comandaService.CadastrarAsync(comanda);
            result.Should().BeNull();
            comandaService.MensagensValidacao.Should().NotBeNull(StringHelper.JoinHtmlMensagem(comandaService.MensagensValidacao));
            comandaService.MensagensValidacao.Any(c => c == ComandaMessage.ProdutoObrigatorio).Should().BeTrue();
        }

        [Test]
        public async Task NaoDeveCadastarCasoPedidoContenhaSituacaoInvalida()
        {
            IComandaService comandaService = _serviceProvider.GetRequiredService<IComandaService>();
            ComandaDto comanda = new ComandaDto
            {
                Situacao = ComandaSituacao.Aberta
            };

            foreach (var produto in ProdutoMock.ObterListaDeProdutos())
            {
                comanda.Pedidos.Add(new ComandaPedidoDto()
                {
                    ProdutoId = produto.Id,
                    ProdutoNome = produto.Nome,
                    ProdutoPreco = produto.Preco,
                    Quantidade = 1,
                    Situacao = default(ComandaPedidoSituacao)
                });
            }

            var result = await comandaService.CadastrarAsync(comanda);
            result.Should().BeNull();
            comandaService.MensagensValidacao.Should().NotBeNull(StringHelper.JoinHtmlMensagem(comandaService.MensagensValidacao));
            comandaService.MensagensValidacao.Any(c => c == ComandaMessage.SituacaoInvalida).Should().BeTrue();
        }

        [Test]
        public async Task DeveEditarComandaValido()
        {
            IComandaService comandaService = _serviceProvider.GetRequiredService<IComandaService>();
            ComandaDto comanda = new ComandaDto
            {
                Id = Guid.NewGuid(),
                Situacao = ComandaSituacao.Aberta
            };

            foreach (var produto in ProdutoMock.ObterListaDeProdutos())
            {
                comanda.Pedidos.Add(new ComandaPedidoDto()
                {
                    ProdutoId = Guid.NewGuid(),
                    ProdutoNome = produto.Nome,
                    ProdutoPreco = produto.Preco,
                    Quantidade = 1,
                    Situacao = ComandaPedidoSituacao.Pedido
                });
            }

            var result = await comandaService.EditarAsync(comanda);
            result.Should().NotBeNull(StringHelper.JoinHtmlMensagem(comandaService.MensagensValidacao));
        }

        [Test]
        public async Task NaoDeveEditarCasoSituacaoInValida()
        {

            IComandaService comandaService = _serviceProvider.GetRequiredService<IComandaService>();
            ComandaDto comanda = new ComandaDto();

            foreach (var produto in ProdutoMock.ObterListaDeProdutos())
            {
                comanda.Pedidos.Add(new ComandaPedidoDto()
                {
                    ProdutoId = Guid.NewGuid(),
                    ProdutoNome = produto.Nome,
                    ProdutoPreco = produto.Preco,
                    Quantidade = 1,
                    Situacao = ComandaPedidoSituacao.Pedido
                });
            }

            var result = await comandaService.EditarAsync(comanda);
            result.Should().BeNull();
            comandaService.MensagensValidacao.Should().NotBeNull(StringHelper.JoinHtmlMensagem(comandaService.MensagensValidacao));
            comandaService.MensagensValidacao.Any(c => c == ComandaMessage.SituacaoInvalida).Should().BeTrue();
        }

        [Test]
        public async Task NaoDeveEditarCasoNaoContenhaPedido()
        {
            IComandaService comandaService = _serviceProvider.GetRequiredService<IComandaService>();
            ComandaDto comanda = new ComandaDto
            {
                Id = Guid.NewGuid(),
                Situacao = ComandaSituacao.Aberta
            };

            var result = await comandaService.EditarAsync(comanda);
            result.Should().BeNull();
            comandaService.MensagensValidacao.Should().NotBeNull(StringHelper.JoinHtmlMensagem(comandaService.MensagensValidacao));
            comandaService.MensagensValidacao.Any(c => c == ComandaMessage.PedidoObrigatorio).Should().BeTrue();
        }

        [Test]
        public async Task NaoDeveEditarCasoPedidoNaoContenhaProduto()
        {
            IComandaService comandaService = _serviceProvider.GetRequiredService<IComandaService>();
            ComandaDto comanda = new ComandaDto
            {
                Id = Guid.NewGuid(),
                Situacao = ComandaSituacao.Aberta
            };

            comanda.Pedidos.Add(new ComandaPedidoDto() { Quantidade = 1, Situacao = ComandaPedidoSituacao.Pedido });

            var result = await comandaService.EditarAsync(comanda);
            result.Should().BeNull();
            comandaService.MensagensValidacao.Should().NotBeNull(StringHelper.JoinHtmlMensagem(comandaService.MensagensValidacao));
            comandaService.MensagensValidacao.Any(c => c == ComandaMessage.ProdutoObrigatorio).Should().BeTrue();
        }

        [Test]
        public async Task NaoDeveEditarCasoPedidoContenhaSituacaoInvalida()
        {
            IComandaService comandaService = _serviceProvider.GetRequiredService<IComandaService>();
            ComandaDto comanda = new ComandaDto
            {
                Id = Guid.NewGuid(),
                Situacao = ComandaSituacao.Aberta
            };

            foreach (var produto in ProdutoMock.ObterListaDeProdutos())
            {
                comanda.Pedidos.Add(new ComandaPedidoDto()
                {
                    ProdutoId = produto.Id,
                    ProdutoNome = produto.Nome,
                    ProdutoPreco = produto.Preco,
                    Quantidade = 1,
                    Situacao = default(ComandaPedidoSituacao)
                });
            }

            var result = await comandaService.CadastrarAsync(comanda);
            result.Should().BeNull();
            comandaService.MensagensValidacao.Should().NotBeNull(StringHelper.JoinHtmlMensagem(comandaService.MensagensValidacao));
            comandaService.MensagensValidacao.Any(c => c == ComandaMessage.SituacaoInvalida).Should().BeTrue();
        }

        [Test]
        public async Task DeveFecharCasoComandaValido()
        {
            MockComandaParameter parameter = new MockComandaParameter()
            {
                Exists = true,
                Comanda = MockHelper.Obter<Comanda>(),
                Comandas = MockHelper.ObterTodos<Comanda>()
            };

            IComandaService comandaService = Startup.GetServiceProvider(new ServiceParameter(parameter)).GetRequiredService<IComandaService>();
            var result = await comandaService.Fechar(Guid.NewGuid());
            result.Should().NotBeNull(StringHelper.JoinHtmlMensagem(comandaService.MensagensValidacao));
        }

        [Test]
        public async Task DeveConfirmarCasoComandaValido()
        {
            MockComandaParameter parameter = new MockComandaParameter()
            {
                Exists = true,
                Comanda = MockHelper.Obter<Comanda>(),
                Comandas = MockHelper.ObterTodos<Comanda>()
            };

            IComandaService comandaService = Startup.GetServiceProvider(new ServiceParameter(parameter)).GetRequiredService<IComandaService>();
            var result = await comandaService.ConfirmarAsync(Guid.NewGuid());
            result.Should().NotBeNull(StringHelper.JoinHtmlMensagem(comandaService.MensagensValidacao));
        }

        [Test]
        public async Task NaoDeveConfirmarCasoComandaInValido()
        {
            IComandaService comandaService = _serviceProvider.GetRequiredService<IComandaService>();
            var result = await comandaService.ConfirmarAsync(Guid.Empty);
            result.Should().BeNull();
            comandaService.MensagensValidacao.Should().NotBeNull(StringHelper.JoinHtmlMensagem(comandaService.MensagensValidacao));
            comandaService.MensagensValidacao.Any(c => c == ComandaMessage.ComandaInvalida).Should().BeTrue();
        }

        [Test]
        public async Task NaoDeveFecharCasoComandaInValido()
        {
            IComandaService comandaService = _serviceProvider.GetRequiredService<IComandaService>();
            var result = await comandaService.ConfirmarAsync(Guid.Empty);
            result.Should().BeNull();
            comandaService.MensagensValidacao.Should().NotBeNull(StringHelper.JoinHtmlMensagem(comandaService.MensagensValidacao));
            comandaService.MensagensValidacao.Any(c => c == ComandaMessage.ComandaInvalida).Should().BeTrue();
        }
    }
}