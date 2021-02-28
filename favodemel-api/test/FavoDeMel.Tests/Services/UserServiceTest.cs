using FavoDeMel.Domain.Fake;
using FavoDeMel.Domain.Usuarios;
using FavoDeMel.Framework.Helpers;
using FavoDeMel.Service.Interfaces;
using FavoDeMel.Tests.Mocks.Parameters;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System.Linq;
using System.Threading.Tasks;

namespace FavoDeMel.Tests.Services
{
    public class UserServiceTest
    {
        private ServiceProvider _serviceProvider;

        [SetUp]
        public void Setup()
        {
            MockUsuarioParameter parameter = new MockUsuarioParameter()
            {
                Exists = false,
                Usuario = EntityFake.ObterUsuarioAdmin(),
                Usuarios = EntityFake.ObterTodos<Usuario>(),
                ExistsLogin = false,
            };
            _serviceProvider = Startup.GetServiceProvider(new ServiceParameter(parameter));
        }

        [Test]
        public async Task DeveCadastarUsuarioValido()
        {
            using (IUsuarioService usuarioService = _serviceProvider.GetRequiredService<IUsuarioService>())
            {
                var usuario = await usuarioService.Inserir(new Usuario
                {
                    Nome = "Teste",
                    Login = "Teste",
                    Password = StringHelper.CalculateMD5Hash("Teste"),
                    Perfil = UsuarioPerfil.Garcom,
                    Comissao = 10,
                    Ativo = true
                });
                usuario.Should().NotBeNull();
            }
        }

        [Test]
        public async Task NaoDeveCadastarCasoUsuarioNulo()
        {
            using (IUsuarioService usuarioService = _serviceProvider.GetRequiredService<IUsuarioService>())
            {
                var usuario = await usuarioService.Inserir(null);
                usuario.Should().BeNull();
                usuarioService.MensagensValidacao.Should().NotBeNull();
                usuarioService.MensagensValidacao.Any(c => c == UsuarioMessage.UsuarioNaoPodeSerNulo).Should().BeTrue();
            }
        }

        [Test]
        public async Task NaoDeveCadastarCasoLoginJaCadastrado()
        {
            MockUsuarioParameter parameter = new MockUsuarioParameter()
            {
                Exists = false,
                Usuario = EntityFake.ObterUsuarioAdmin(),
                Usuarios = EntityFake.ObterTodos<Usuario>(),
                ExistsLogin = true,
            };

            using (IUsuarioService usuarioService = Startup.GetServiceProvider(new ServiceParameter(parameter)).GetRequiredService<IUsuarioService>())
            {
                var usuario = await usuarioService.Inserir(new Usuario
                {
                    Nome = "Teste",
                    Login = "Teste",
                    Password = StringHelper.CalculateMD5Hash("Teste"),
                    Perfil = UsuarioPerfil.Garcom,
                    Comissao = 10,
                    Ativo = true
                });
                usuario.Should().BeNull();
                usuarioService.MensagensValidacao.Should().NotBeNull();
                usuarioService.MensagensValidacao.Any(c => c == UsuarioMessage.LoginJaCadastrado).Should().BeTrue();
            }
        }

        [Test]
        public async Task NaoDeveCadastarCasoUsuarioInvalido()
        {
            using (IUsuarioService usuarioService = _serviceProvider.GetRequiredService<IUsuarioService>())
            {
                var usuario = await usuarioService.Inserir(new Usuario());
                usuario.Should().BeNull();
                usuarioService.MensagensValidacao.Should().NotBeNull();
            }
        }

        [Test]
        public async Task NaoDeveCadastarCasoNomeNulo()
        {
            using (IUsuarioService usuarioService = _serviceProvider.GetRequiredService<IUsuarioService>())
            {
                var usuario = await usuarioService.Inserir(new Usuario
                {
                    Nome = null,
                    Login = "Teste",
                    Password = StringHelper.CalculateMD5Hash("Teste1"),
                    Perfil = UsuarioPerfil.Garcom,
                    Comissao = 10,
                    Ativo = true
                });
                usuario.Should().BeNull();
                usuarioService.MensagensValidacao.Should().NotBeNull();
                usuarioService.MensagensValidacao.Any(c => c == UsuarioMessage.NomeObrigatorio).Should().BeTrue();
            }
        }

        [Test]
        public async Task NaoDeveCadastarCasoSenhaLoginNuLo()
        {
            using (IUsuarioService usuarioService = _serviceProvider.GetRequiredService<IUsuarioService>())
            {
                var usuario = await usuarioService.Inserir(new Usuario
                {
                    Nome = "Teste",
                    Login = null,
                    Password = null,
                    Perfil = UsuarioPerfil.Garcom,
                    Comissao = 10,
                    Ativo = true
                });
                usuario.Should().BeNull();
                usuarioService.MensagensValidacao.Should().NotBeNull();
                usuarioService.MensagensValidacao.Any(c => c == UsuarioMessage.SenhaObrigatoria).Should().BeTrue(); ;
                usuarioService.MensagensValidacao.Any(c => c == UsuarioMessage.LoginObrigatorio).Should().BeTrue(); ;
            }
        }

        [Test]
        public async Task NaoDeveCadastarCasoSenhaNaoCumpraRequisitoMinimo()
        {
            using (IUsuarioService usuarioService = _serviceProvider.GetRequiredService<IUsuarioService>())
            {
                var usuario = await usuarioService.Inserir(new Usuario
                {
                    Nome = "Teste",
                    Login = "Teste",
                    Password = "123",
                    Perfil = UsuarioPerfil.Garcom,
                    Comissao = 10,
                    Ativo = true
                });
                usuario.Should().BeNull();
                usuarioService.MensagensValidacao.Should().NotBeNull();
                usuarioService.MensagensValidacao.Any(c => c == UsuarioMessage.SenhaDeConterMinioCaracters(6)).Should().BeTrue(); ;
            }
        }

        [Test]
        public async Task NaoDeveCadastarCasoSenhaContenhaEspacoEmBranco()
        {
            using (IUsuarioService usuarioService = _serviceProvider.GetRequiredService<IUsuarioService>())
            {
                var usuario = await usuarioService.Inserir(new Usuario
                {
                    Nome = "Teste",
                    Login = "Teste",
                    Password = "TESTE 123",
                    Perfil = UsuarioPerfil.Garcom,
                    Comissao = 10,
                    Ativo = true
                });
                usuario.Should().BeNull();
                usuarioService.MensagensValidacao.Should().NotBeNull();
                usuarioService.MensagensValidacao.Any(c => c == UsuarioMessage.SenhaNaoPodeConterEspacoEmBranco).Should().BeTrue(); ;
            }
        }

        [Test]
        public async Task NaoDeveCadastarPerfilInvalido()
        {
            using (IUsuarioService usuarioService = _serviceProvider.GetRequiredService<IUsuarioService>())
            {
                var usuario = await usuarioService.Inserir(new Usuario
                {
                    Nome = "Teste",
                    Login = "Teste",
                    Password = "TESTE 123",
                    Perfil = default(UsuarioPerfil),
                    Comissao = 10,
                    Ativo = true
                });
                usuario.Should().BeNull();
                usuarioService.MensagensValidacao.Should().NotBeNull();
                usuarioService.MensagensValidacao.Any(c => c == UsuarioMessage.PerfilInvalido).Should().BeTrue();
            }
        }

        [Test]
        public async Task NaoDevePermitirEditarSenhaCasoNovaSenhaNula()
        {
            using (IUsuarioService usuarioService = _serviceProvider.GetRequiredService<IUsuarioService>())
            {
                var usuario = await usuarioService.AlterarSenha(1, null);
                usuario.Should().BeFalse();
                usuarioService.MensagensValidacao.Should().NotBeNull();
                usuarioService.MensagensValidacao.Any(c => c == UsuarioMessage.SenhaObrigatoria).Should().BeTrue();
            }
        }

        [Test]
        public async Task NaoDevePermitirEditarSenhaCasoNovaSejaIgualAnterior()
        {
            using (IUsuarioService usuarioService = _serviceProvider.GetRequiredService<IUsuarioService>())
            {
                var usuario = await usuarioService.AlterarSenha(1, StringHelper.CalculateMD5Hash("Admin"));
                usuario.Should().BeFalse();
                usuarioService.MensagensValidacao.Should().NotBeNull();
                usuarioService.MensagensValidacao.Any(c => c == UsuarioMessage.NovaSenhaNaoPodeSerIgualAtual).Should().BeTrue();
            }
        }

        [Test]
        public async Task NaoDevePermitirEditarSenhaCasoNaoCumpraRequisitoMinimo()
        {
            using (IUsuarioService usuarioService = _serviceProvider.GetRequiredService<IUsuarioService>())
            {
                var usuario = await usuarioService.AlterarSenha(1, "123");
                usuario.Should().BeFalse();
                usuarioService.MensagensValidacao.Should().NotBeNull();
                usuarioService.MensagensValidacao.Any(c => c == UsuarioMessage.SenhaDeConterMinioCaracters(6)).Should().BeTrue();
            }
        }

        [Test]
        public async Task NaoDevePermitirEditarSenhaCasoContenhaEspacoEmBranco()
        {
            using (IUsuarioService usuarioService = _serviceProvider.GetRequiredService<IUsuarioService>())
            {
                var usuario = await usuarioService.AlterarSenha(1, "teste 123");
                usuario.Should().BeFalse();
                usuarioService.MensagensValidacao.Should().NotBeNull();
                usuarioService.MensagensValidacao.Any(c => c == UsuarioMessage.SenhaNaoPodeConterEspacoEmBranco).Should().BeTrue();
            }
        }

        [Test]
        public async Task DeveEditarUsuarioValido()
        {
            using (IUsuarioService usuarioService = _serviceProvider.GetRequiredService<IUsuarioService>())
            {
                var usuario = await usuarioService.Editar(new Usuario
                {
                    Id = 1,
                    Nome = "Teste",
                    Login = "Teste",
                    Password = StringHelper.CalculateMD5Hash("Teste"),
                    Perfil = UsuarioPerfil.Garcom,
                    Comissao = 10,
                    Ativo = true
                });
                usuario.Should().NotBeNull();
            }
        }

        [Test]
        public async Task NaoDeveEditarCasoUsuarioNulo()
        {
            using (IUsuarioService usuarioService = _serviceProvider.GetRequiredService<IUsuarioService>())
            {
                var usuario = await usuarioService.Editar(null);
                usuario.Should().BeNull();
                usuarioService.MensagensValidacao.Should().NotBeNull();
                usuarioService.MensagensValidacao.Any(c => c == UsuarioMessage.UsuarioNaoPodeSerNulo).Should().BeTrue();
            }
        }

        [Test]
        public async Task NaoDeveEditarCasoUsuarioInvalido()
        {
            using (IUsuarioService usuarioService = _serviceProvider.GetRequiredService<IUsuarioService>())
            {
                var usuario = await usuarioService.Editar(new Usuario());
                usuario.Should().BeNull();
                usuarioService.MensagensValidacao.Should().NotBeNull();
            }
        }

        [Test]
        public async Task NaoDeveEditarCasoNomeNulo()
        {
            using (IUsuarioService usuarioService = _serviceProvider.GetRequiredService<IUsuarioService>())
            {
                var usuario = await usuarioService.Editar(new Usuario
                {
                    Id = 1,
                    Nome = null,
                    Login = "Teste",
                    Password = StringHelper.CalculateMD5Hash("Teste1"),
                    Perfil = UsuarioPerfil.Garcom,
                    Comissao = 10,
                    Ativo = true
                });
                usuario.Should().BeNull();
                usuarioService.MensagensValidacao.Should().NotBeNull();
                usuarioService.MensagensValidacao.Any(c => c == UsuarioMessage.NomeObrigatorio).Should().BeTrue();
            }
        }

        [Test]
        public async Task NaoDeveEditarPerfilInvalido()
        {
            using (IUsuarioService usuarioService = _serviceProvider.GetRequiredService<IUsuarioService>())
            {
                var usuario = await usuarioService.Editar(new Usuario
                {
                    Id = 1,
                    Nome = "Teste",
                    Login = "Teste",
                    Password = "TESTE 123",
                    Perfil = default(UsuarioPerfil),
                    Comissao = 10,
                    Ativo = true
                });
                usuario.Should().BeNull();
                usuarioService.MensagensValidacao.Should().NotBeNull();
                usuarioService.MensagensValidacao.Any(c => c == UsuarioMessage.PerfilInvalido).Should().BeTrue();
            }
        }
    }
}