using FavoDeMel.Domain.Dtos;
using FavoDeMel.Domain.Helpers;
using FavoDeMel.Domain.Entities.Usuarios;
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
    public class UsuarioServiceTest
    {
        private ServiceProvider _serviceProvider;

        [SetUp]
        public void Setup()
        {
            MockUsuarioParameter parameter = new MockUsuarioParameter()
            {
                Exists = false,
                Usuario = UsuarioMock.ObterUsuarioAdmin(),
                Usuarios = MockHelper.ObterTodos<Usuario>(),
                ExistsLogin = false,
            };
            _serviceProvider = Startup.GetServiceProvider(new ServiceParameter(parameter));
        }

        [Test]
        public async Task DeveCadastarUsuarioValido()
        {
            IUsuarioService usuarioService = _serviceProvider.GetRequiredService<IUsuarioService>();
            var usuario = await usuarioService.Inserir(new UsuarioDto
            {
                Nome = "Teste",
                Login = "Teste",
                Password = StringHelper.CalculateMD5Hash("Teste"),
                Perfil = UsuarioPerfil.Garcom,
                Comissao = 10,
                Ativo = true
            });
            usuario.Should().NotBeNull(StringHelper.JoinHtmlMensagem(usuarioService.MensagensValidacao));
        }

        [Test]
        public async Task NaoDeveCadastarCasoUsuarioNulo()
        {
            IUsuarioService usuarioService = _serviceProvider.GetRequiredService<IUsuarioService>();
            var usuario = await usuarioService.Inserir(null);
            usuario.Should().BeNull();
            usuarioService.MensagensValidacao.Should().NotBeNull(StringHelper.JoinHtmlMensagem(usuarioService.MensagensValidacao));
            usuarioService.MensagensValidacao.Any(c => c == UsuarioMessage.UsuarioNaoPodeSerNulo).Should().BeTrue();
        }

        [Test]
        public async Task NaoDeveCadastarCasoLoginJaCadastrado()
        {
            MockUsuarioParameter parameter = new MockUsuarioParameter()
            {
                Exists = false,
                Usuario = UsuarioMock.ObterUsuarioAdmin(),
                Usuarios = MockHelper.ObterTodos<Usuario>(),
                ExistsLogin = true,
            };

            IUsuarioService usuarioService = Startup.GetServiceProvider(new ServiceParameter(parameter)).GetRequiredService<IUsuarioService>();
            var usuario = await usuarioService.Inserir(new UsuarioDto
            {
                Nome = "Teste",
                Login = "Teste",
                Password = StringHelper.CalculateMD5Hash("Teste"),
                Perfil = UsuarioPerfil.Garcom,
                Comissao = 10,
                Ativo = true
            });
            usuario.Should().BeNull();
            usuarioService.MensagensValidacao.Should().NotBeNull(StringHelper.JoinHtmlMensagem(usuarioService.MensagensValidacao));
            usuarioService.MensagensValidacao.Any(c => c == UsuarioMessage.LoginJaCadastrado).Should().BeTrue();
        }

        [Test]
        public async Task NaoDeveCadastarCasoUsuarioInvalido()
        {
            IUsuarioService usuarioService = _serviceProvider.GetRequiredService<IUsuarioService>();
            var usuario = await usuarioService.Inserir(new UsuarioDto());
            usuario.Should().BeNull();
            usuarioService.MensagensValidacao.Should().NotBeNull(StringHelper.JoinHtmlMensagem(usuarioService.MensagensValidacao));
        }

        [Test]
        public async Task NaoDeveCadastarCasoNomeNulo()
        {
            IUsuarioService usuarioService = _serviceProvider.GetRequiredService<IUsuarioService>();
            var usuario = await usuarioService.Inserir(new UsuarioDto
            {
                Nome = null,
                Login = "Teste",
                Password = "Teste1",
                Perfil = UsuarioPerfil.Garcom,
                Comissao = 10,
                Ativo = true
            });
            usuario.Should().BeNull();
            usuarioService.MensagensValidacao.Should().NotBeNull(StringHelper.JoinHtmlMensagem(usuarioService.MensagensValidacao));
            usuarioService.MensagensValidacao.Any(c => c == UsuarioMessage.NomeObrigatorio).Should().BeTrue();
        }

        [Test]
        public async Task NaoDeveCadastarCasoSenhaLoginNuLo()
        {
            IUsuarioService usuarioService = _serviceProvider.GetRequiredService<IUsuarioService>();
            var usuario = await usuarioService.Inserir(new UsuarioDto
            {
                Nome = "Teste",
                Login = null,
                Password = null,
                Perfil = UsuarioPerfil.Garcom,
                Comissao = 10,
                Ativo = true
            });
            usuario.Should().BeNull();
            usuarioService.MensagensValidacao.Should().NotBeNull(StringHelper.JoinHtmlMensagem(usuarioService.MensagensValidacao));
            usuarioService.MensagensValidacao.Any(c => c == UsuarioMessage.SenhaObrigatoria).Should().BeTrue(); ;
            usuarioService.MensagensValidacao.Any(c => c == UsuarioMessage.LoginObrigatorio).Should().BeTrue(); ;
        }

        [Test]
        public async Task NaoDeveCadastarCasoSenhaNaoCumpraRequisitoMinimo()
        {
            IUsuarioService usuarioService = _serviceProvider.GetRequiredService<IUsuarioService>();
            var usuario = await usuarioService.Inserir(new UsuarioDto
            {
                Nome = "Teste",
                Login = "Teste",
                Password = "123",
                Perfil = UsuarioPerfil.Garcom,
                Comissao = 10,
                Ativo = true
            });
            usuario.Should().BeNull();
            usuarioService.MensagensValidacao.Should().NotBeNull(StringHelper.JoinHtmlMensagem(usuarioService.MensagensValidacao));
            usuarioService.MensagensValidacao.Any(c => c == UsuarioMessage.SenhaDeConterMinioCaracters(6)).Should().BeTrue(); ;
        }

        [Test]
        public async Task NaoDeveCadastarCasoSenhaContenhaEspacoEmBranco()
        {
            IUsuarioService usuarioService = _serviceProvider.GetRequiredService<IUsuarioService>();
            var usuario = await usuarioService.Inserir(new UsuarioDto
            {
                Nome = "Teste",
                Login = "Teste",
                Password = "TESTE 123",
                Perfil = UsuarioPerfil.Garcom,
                Comissao = 10,
                Ativo = true
            });
            usuario.Should().BeNull();
            usuarioService.MensagensValidacao.Should().NotBeNull(StringHelper.JoinHtmlMensagem(usuarioService.MensagensValidacao));
            usuarioService.MensagensValidacao.Any(c => c == UsuarioMessage.SenhaNaoPodeConterEspacoEmBranco).Should().BeTrue(); ;
        }

        [Test]
        public async Task NaoDeveCadastarPerfilInvalido()
        {
            IUsuarioService usuarioService = _serviceProvider.GetRequiredService<IUsuarioService>();
            var usuario = await usuarioService.Inserir(new UsuarioDto
            {
                Nome = "Teste",
                Login = "Teste",
                Password = "TESTE 123",
                Perfil = default(UsuarioPerfil),
                Comissao = 10,
                Ativo = true
            });
            usuario.Should().BeNull();
            usuarioService.MensagensValidacao.Should().NotBeNull(StringHelper.JoinHtmlMensagem(usuarioService.MensagensValidacao));
            usuarioService.MensagensValidacao.Any(c => c == UsuarioMessage.PerfilInvalido).Should().BeTrue();
        }

        [Test]
        public async Task NaoDevePermitirEditarSenhaCasoNovaSenhaNula()
        {
            IUsuarioService usuarioService = _serviceProvider.GetRequiredService<IUsuarioService>();
            var usuario = await usuarioService.AlterarSenha(Guid.NewGuid(), null);
            usuario.Should().BeFalse();
            usuarioService.MensagensValidacao.Should().NotBeNull(StringHelper.JoinHtmlMensagem(usuarioService.MensagensValidacao));
            usuarioService.MensagensValidacao.Any(c => c == UsuarioMessage.SenhaObrigatoria).Should().BeTrue();
        }

        [Test]
        public async Task NaoDevePermitirEditarSenhaCasoNovaSejaIgualAnterior()
        {
            IUsuarioService usuarioService = _serviceProvider.GetRequiredService<IUsuarioService>();
            var usuario = await usuarioService.AlterarSenha(Guid.NewGuid(), StringHelper.CalculateMD5Hash("Admin"));
            usuario.Should().BeFalse();
            usuarioService.MensagensValidacao.Should().NotBeNull(StringHelper.JoinHtmlMensagem(usuarioService.MensagensValidacao));
            usuarioService.MensagensValidacao.Any(c => c == UsuarioMessage.NovaSenhaNaoPodeSerIgualAtual).Should().BeTrue();
        }

        [Test]
        public async Task NaoDevePermitirEditarSenhaCasoNaoCumpraRequisitoMinimo()
        {
            IUsuarioService usuarioService = _serviceProvider.GetRequiredService<IUsuarioService>();
            var usuario = await usuarioService.AlterarSenha(Guid.NewGuid(), "123");
            usuario.Should().BeFalse();
            usuarioService.MensagensValidacao.Should().NotBeNull(StringHelper.JoinHtmlMensagem(usuarioService.MensagensValidacao));
            usuarioService.MensagensValidacao.Any(c => c == UsuarioMessage.SenhaDeConterMinioCaracters(6)).Should().BeTrue();
        }

        [Test]
        public async Task NaoDevePermitirEditarSenhaCasoContenhaEspacoEmBranco()
        {
            IUsuarioService usuarioService = _serviceProvider.GetRequiredService<IUsuarioService>();
            var usuario = await usuarioService.AlterarSenha(Guid.NewGuid(), "teste 123");
            usuario.Should().BeFalse();
            usuarioService.MensagensValidacao.Should().NotBeNull(StringHelper.JoinHtmlMensagem(usuarioService.MensagensValidacao));
            usuarioService.MensagensValidacao.Any(c => c == UsuarioMessage.SenhaNaoPodeConterEspacoEmBranco).Should().BeTrue();
        }

        [Test]
        public async Task DeveEditarUsuarioValido()
        {
            IUsuarioService usuarioService = _serviceProvider.GetRequiredService<IUsuarioService>();
            var usuario = await usuarioService.Editar(new UsuarioDto
            {
                Id = Guid.NewGuid(),
                Nome = "Teste",
                Login = "Teste",
                Password = "Teste",
                Perfil = UsuarioPerfil.Garcom,
                Comissao = 10,
                Ativo = true
            });
            usuario.Should().NotBeNull(StringHelper.JoinHtmlMensagem(usuarioService.MensagensValidacao));
        }

        [Test]
        public async Task NaoDeveEditarCasoUsuarioNulo()
        {
            IUsuarioService usuarioService = _serviceProvider.GetRequiredService<IUsuarioService>();
            var usuario = await usuarioService.Editar(null);
            usuario.Should().BeNull();
            usuarioService.MensagensValidacao.Should().NotBeNull(StringHelper.JoinHtmlMensagem(usuarioService.MensagensValidacao));
            usuarioService.MensagensValidacao.Any(c => c == UsuarioMessage.UsuarioNaoPodeSerNulo).Should().BeTrue();
        }

        [Test]
        public async Task NaoDeveEditarCasoUsuarioInvalido()
        {
            IUsuarioService usuarioService = _serviceProvider.GetRequiredService<IUsuarioService>();
            var usuario = await usuarioService.Editar(new UsuarioDto());
            usuario.Should().BeNull();
            usuarioService.MensagensValidacao.Should().NotBeNull(StringHelper.JoinHtmlMensagem(usuarioService.MensagensValidacao));
        }

        [Test]
        public async Task NaoDeveEditarCasoNomeNulo()
        {
            IUsuarioService usuarioService = _serviceProvider.GetRequiredService<IUsuarioService>();
            var usuario = await usuarioService.Editar(new UsuarioDto
            {
                Id = Guid.NewGuid(),
                Nome = null,
                Login = "Teste",
                Password = StringHelper.CalculateMD5Hash("Teste1"),
                Perfil = UsuarioPerfil.Garcom,
                Comissao = 10,
                Ativo = true
            });
            usuario.Should().BeNull();
            usuarioService.MensagensValidacao.Should().NotBeNull(StringHelper.JoinHtmlMensagem(usuarioService.MensagensValidacao));
            usuarioService.MensagensValidacao.Any(c => c == UsuarioMessage.NomeObrigatorio).Should().BeTrue();
        }

        [Test]
        public async Task NaoDeveEditarPerfilInvalido()
        {
            IUsuarioService usuarioService = _serviceProvider.GetRequiredService<IUsuarioService>();
            var usuario = await usuarioService.Editar(new UsuarioDto
            {
                Id = Guid.NewGuid(),
                Nome = "Teste",
                Login = "Teste",
                Password = "TESTE 123",
                Perfil = default(UsuarioPerfil),
                Comissao = 10,
                Ativo = true
            });
            usuario.Should().BeNull();
            usuarioService.MensagensValidacao.Should().NotBeNull(StringHelper.JoinHtmlMensagem(usuarioService.MensagensValidacao));
            usuarioService.MensagensValidacao.Any(c => c == UsuarioMessage.PerfilInvalido).Should().BeTrue();
        }
    }
}