using FavoDeMel.Domain.Comandas;
using FavoDeMel.Domain.Fake;
using FavoDeMel.Domain.Produtos;
using FavoDeMel.Service.Interfaces;
using FavoDeMel.Tests.Mocks.Parameters;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
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
                Comanda = EntityFake.Obter<Comanda>(),
                Comandas = EntityFake.ObterTodos<Comanda>()
            };
            _serviceProvider = Startup.GetServiceProvider(new ServiceParameter(parameter));
        }

        [Test]
        public async Task DeveCadastarComandaValido()
        {
            using (IComandaService comandaService = _serviceProvider.GetRequiredService<IComandaService>())
            {
                Comanda comanda = new Comanda
                {
                    Situacao = ComandaSituacao.Aberta
                };

                foreach (var produto in EntityFake.ObterListaDeProdutos())
                {
                    comanda.Pedidos.Add(new ComandaPedido() { Produto = produto, Quantidade = 1, Situacao = ComandaPedidoSituacao.Pedido });
                }

                var result = await comandaService.Inserir(comanda);
                result.Should().NotBeNull();
            }
        }

        [Test]
        public async Task NaoDeveCadastarCasoSituacaoInValida()
        {
            using (IComandaService comandaService = _serviceProvider.GetRequiredService<IComandaService>())
            {
                Comanda comanda = new Comanda();

                foreach (var produto in EntityFake.ObterListaDeProdutos())
                {
                    comanda.Pedidos.Add(new ComandaPedido() { Produto = produto, Quantidade = 1, Situacao = ComandaPedidoSituacao.Pedido });
                }

                var result = await comandaService.Inserir(comanda);
                result.Should().BeNull();
                comandaService.MensagensValidacao.Should().NotBeNull();
                comandaService.MensagensValidacao.Any(c => c == ComandaMessage.SituacaoInvalida).Should().BeTrue();
            }
        }

        [Test]
        public async Task NaoDeveCadastarCasoNaoContenhaPedido()
        {
            using (IComandaService comandaService = _serviceProvider.GetRequiredService<IComandaService>())
            {
                Comanda comanda = new Comanda
                {
                    Situacao = ComandaSituacao.Aberta
                };

                var result = await comandaService.Inserir(comanda);
                result.Should().BeNull();
                comandaService.MensagensValidacao.Should().NotBeNull();
                comandaService.MensagensValidacao.Any(c => c == ComandaMessage.PedidoObrigatorio).Should().BeTrue();
            }
        }

        [Test]
        public async Task NaoDeveCadastarCasoPedidoNaoContenhaProduto()
        {
            using (IComandaService comandaService = _serviceProvider.GetRequiredService<IComandaService>())
            {
                Comanda comanda = new Comanda
                {
                    Situacao = ComandaSituacao.Aberta
                };

                comanda.Pedidos.Add(new ComandaPedido() { Quantidade = 1, Situacao = ComandaPedidoSituacao.Pedido });

                var result = await comandaService.Inserir(comanda);
                result.Should().BeNull();
                comandaService.MensagensValidacao.Should().NotBeNull();
                comandaService.MensagensValidacao.Any(c => c == ComandaMessage.ProdutoObrigatorio).Should().BeTrue();
            }
        }

        [Test]
        public async Task NaoDeveCadastarCasoPedidoContenhaSituacaoInvalida()
        {
            using (IComandaService comandaService = _serviceProvider.GetRequiredService<IComandaService>())
            {
                Comanda comanda = new Comanda
                {
                    Situacao = ComandaSituacao.Aberta
                };

                foreach (var produto in EntityFake.ObterListaDeProdutos())
                {
                    comanda.Pedidos.Add(new ComandaPedido() { Produto = produto, Quantidade = 1, Situacao = default(ComandaPedidoSituacao) });
                }

                var result = await comandaService.Inserir(comanda);
                result.Should().BeNull();
                comandaService.MensagensValidacao.Should().NotBeNull();
                comandaService.MensagensValidacao.Any(c => c == ComandaMessage.SituacaoInvalida).Should().BeTrue();
            }
        }

        [Test]
        public async Task DeveEditarComandaValido()
        {
            using (IComandaService comandaService = _serviceProvider.GetRequiredService<IComandaService>())
            {
                Comanda comanda = new Comanda
                {
                    Id = 1,
                    Situacao = ComandaSituacao.Aberta
                };

                foreach (var produto in EntityFake.ObterListaDeProdutos())
                {
                    comanda.Pedidos.Add(new ComandaPedido() { Produto = produto, Quantidade = 1, Situacao = ComandaPedidoSituacao.Pedido });
                }

                var result = await comandaService.Editar(comanda);
                result.Should().NotBeNull();
            }
        }

        [Test]
        public async Task NaoDeveEditarCasoSituacaoInValida()
        {
            using (IComandaService comandaService = _serviceProvider.GetRequiredService<IComandaService>())
            {
                Comanda comanda = new Comanda();

                foreach (var produto in EntityFake.ObterListaDeProdutos())
                {
                    comanda.Pedidos.Add(new ComandaPedido() { Produto = produto, Quantidade = 1, Situacao = ComandaPedidoSituacao.Pedido });
                }

                var result = await comandaService.Editar(comanda);
                result.Should().BeNull();
                comandaService.MensagensValidacao.Should().NotBeNull();
                comandaService.MensagensValidacao.Any(c => c == ComandaMessage.SituacaoInvalida).Should().BeTrue();
            }
        }

        [Test]
        public async Task NaoDeveEditarCasoNaoContenhaPedido()
        {
            using (IComandaService comandaService = _serviceProvider.GetRequiredService<IComandaService>())
            {
                Comanda comanda = new Comanda
                {
                    Id = 1,
                    Situacao = ComandaSituacao.Aberta
                };

                var result = await comandaService.Editar(comanda);
                result.Should().BeNull();
                comandaService.MensagensValidacao.Should().NotBeNull();
                comandaService.MensagensValidacao.Any(c => c == ComandaMessage.PedidoObrigatorio).Should().BeTrue();
            }
        }

        [Test]
        public async Task NaoDeveEditarCasoPedidoNaoContenhaProduto()
        {
            using (IComandaService comandaService = _serviceProvider.GetRequiredService<IComandaService>())
            {
                Comanda comanda = new Comanda
                {
                    Id = 1,
                    Situacao = ComandaSituacao.Aberta
                };

                comanda.Pedidos.Add(new ComandaPedido() { Quantidade = 1, Situacao = ComandaPedidoSituacao.Pedido });

                var result = await comandaService.Editar(comanda);
                result.Should().BeNull();
                comandaService.MensagensValidacao.Should().NotBeNull();
                comandaService.MensagensValidacao.Any(c => c == ComandaMessage.ProdutoObrigatorio).Should().BeTrue();
            }
        }

        [Test]
        public async Task NaoDeveEditarCasoPedidoContenhaSituacaoInvalida()
        {
            using (IComandaService comandaService = _serviceProvider.GetRequiredService<IComandaService>())
            {
                Comanda comanda = new Comanda
                {
                    Id = 1,
                    Situacao = ComandaSituacao.Aberta
                };

                foreach (var produto in EntityFake.ObterListaDeProdutos())
                {
                    comanda.Pedidos.Add(new ComandaPedido() { Produto = produto, Quantidade = 1, Situacao = default(ComandaPedidoSituacao) });
                }

                var result = await comandaService.Inserir(comanda);
                result.Should().BeNull();
                comandaService.MensagensValidacao.Should().NotBeNull();
                comandaService.MensagensValidacao.Any(c => c == ComandaMessage.SituacaoInvalida).Should().BeTrue();
            }
        }

        [Test]
        public async Task DeveFecharCasoComandaValido()
        {
            MockComandaParameter parameter = new MockComandaParameter()
            {
                Exists = true,
                Comanda = EntityFake.Obter<Comanda>(),
                Comandas = EntityFake.ObterTodos<Comanda>()
            };

            using (IComandaService comandaService = Startup.GetServiceProvider(new ServiceParameter(parameter)).GetRequiredService<IComandaService>())
            {
                var result = await comandaService.Fechar(1);
                result.Should().NotBeNull();
            }
        }

        [Test]
        public async Task DeveConfirmarCasoComandaValido()
        {
            MockComandaParameter parameter = new MockComandaParameter()
            {
                Exists = true,
                Comanda = EntityFake.Obter<Comanda>(),
                Comandas = EntityFake.ObterTodos<Comanda>()
            };

            using (IComandaService comandaService = Startup.GetServiceProvider(new ServiceParameter(parameter)).GetRequiredService<IComandaService>())
            {
                var result = await comandaService.Confirmar(1);
                result.Should().NotBeNull();
            }
        }

        [Test]
        public async Task NaoDeveConfirmarCasoComandaInValido()
        {
            using (IComandaService comandaService = _serviceProvider.GetRequiredService<IComandaService>())
            {
                var result = await comandaService.Confirmar(0);
                result.Should().BeNull();
                comandaService.MensagensValidacao.Should().NotBeNull();
                comandaService.MensagensValidacao.Any(c => c == ComandaMessage.ComandaInvalida).Should().BeTrue();
            }
        }

        [Test]
        public async Task NaoDeveFecharCasoComandaInValido()
        {
            using (IComandaService comandaService = _serviceProvider.GetRequiredService<IComandaService>())
            {
                var result = await comandaService.Confirmar(0);
                result.Should().BeNull();
                comandaService.MensagensValidacao.Should().NotBeNull();
                comandaService.MensagensValidacao.Any(c => c == ComandaMessage.ComandaInvalida).Should().BeTrue();
            }
        }
    }
}