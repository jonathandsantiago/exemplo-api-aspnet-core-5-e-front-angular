using FavoDeMel.Domain.Common;
using System;
using System.Threading.Tasks;

namespace FavoDeMel.Domain.Usuarios
{
    public class UsuarioValidator : ValidatorBase<int, Usuario, IUsuarioRepository>
    {
        public UsuarioValidator(IUsuarioRepository repository) :
            base(repository)
        { }

        public override async Task<bool> Validar(Usuario usuario)
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

            if (usuario.Id == 0)
            {
                if (await _repository.ExistsLogin(usuario.Login))
                {
                    AddMensagem(UsuarioMessage.LoginJaCadastrado);
                }

                if (string.IsNullOrWhiteSpace(usuario.Login))
                {
                    AddMensagem(UsuarioMessage.LoginObrigatorio);
                }

                if (string.IsNullOrEmpty(usuario.Password))
                {
                    AddMensagem(UsuarioMessage.SenhaObrigatoria);
                }
                else if (usuario.Password.Contains(" "))
                {
                    AddMensagem(UsuarioMessage.SenhaNaoPodeConterEspacoEmBranco);
                }
                else if (usuario.Password.Length < 6)
                {
                    AddMensagem(UsuarioMessage.SenhaDeConterMinioCaracters(6));
                }
            }

            return await base.Validar(usuario);
        }

        public bool PermiteEditarSenha(string senhaAtual, string novaSenha)
        {
            if (senhaAtual == novaSenha)
            {
                AddMensagem(UsuarioMessage.NovaSenhaNaoPodeSerIgualAtual);
            }
            else
            {
                if (string.IsNullOrEmpty(novaSenha))
                {
                    AddMensagem(UsuarioMessage.SenhaObrigatoria);
                }
                else if (novaSenha.Contains(" "))
                {
                    AddMensagem(UsuarioMessage.SenhaNaoPodeConterEspacoEmBranco);
                }
                else if (novaSenha.Length < 6)
                {
                    AddMensagem(UsuarioMessage.SenhaDeConterMinioCaracters(6));
                }
            }

            return IsValido;
        }
    }
}