using FavoDeMel.Domain.Common;
using FavoDeMel.Domain.Dtos;
using System;
using System.Threading.Tasks;

namespace FavoDeMel.Domain.Entities.Usuarios
{
    public class UsuarioValidator : ValidatorBase<UsuarioDto>
    {
        private readonly IUsuarioRepository _repository;

        public UsuarioValidator(IUsuarioRepository repository)
        {
            _repository = repository;
        }

        public override async Task<bool> Validar(UsuarioDto usuario)
        {
            if (usuario == null)
            {
                AddMensagem(UsuarioMessage.UsuarioNaoPodeSerNulo);
                return false;
            }

            if (string.IsNullOrEmpty(usuario.Nome))
            {
                AddMensagem(UsuarioMessage.NomeObrigatorio);
            }

            if (!Enum.IsDefined(typeof(UsuarioPerfil), usuario.Perfil))
            {
                AddMensagem(UsuarioMessage.PerfilInvalido);
            }

            if (usuario.Id == null || usuario.Id == Guid.Empty)
            {
                if (await _repository.ExistsLogin(usuario.Login))
                {
                    AddMensagem(UsuarioMessage.LoginJaCadastrado);
                }

                if (string.IsNullOrWhiteSpace(usuario.Login))
                {
                    AddMensagem(UsuarioMessage.LoginObrigatorio);
                }

                ValidarSenha(usuario.Password);
            }

            return await base.Validar(usuario);
        }

        public async Task<bool> ValidarLogin(LoginDto loginDto)
        {
            if (string.IsNullOrWhiteSpace(loginDto.Login))
            {
                AddMensagem(UsuarioMessage.LoginJaCadastrado);
            }

            if (string.IsNullOrWhiteSpace(loginDto.Password))
            {
                AddMensagem(UsuarioMessage.SenhaObrigatoria);
            }
            else if (!await _repository.UsuarioSenhaValido(loginDto.Login, loginDto.PasswordHash))
            {
                AddMensagem(UsuarioMessage.UsuarioOuSenhaInvalida);
            }

            return IsValido;
        }

        public bool PermiteEditarSenha(string senhaAtual, string novaSenha)
        {
            if (senhaAtual == novaSenha)
            {
                AddMensagem(UsuarioMessage.NovaSenhaNaoPodeSerIgualAtual);
            }
            else
            {
                ValidarSenha(novaSenha);
            }

            return IsValido;
        }

        private void ValidarSenha(string senha)
        {
            if (string.IsNullOrEmpty(senha))
            {
                AddMensagem(UsuarioMessage.SenhaObrigatoria);
            }
            else if (senha.Contains(" "))
            {
                AddMensagem(UsuarioMessage.SenhaNaoPodeConterEspacoEmBranco);
            }
            else if (senha.Length < 6)
            {
                AddMensagem(UsuarioMessage.SenhaDeConterMinioCaracters(6));
            }
        }
    }
}