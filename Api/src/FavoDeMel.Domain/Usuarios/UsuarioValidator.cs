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
            if (await _repository.ExistsLogin(usuario.Login))
            {
                AddMensagem("Login já cadastrado");
            }

            if (!Enum.IsDefined(typeof(UsuarioPerfil), usuario.Perfil))
            {
                AddMensagem("Perfil do usuário invalído.");
            }

            if (string.IsNullOrEmpty(usuario.Password))
            {
                AddMensagem("Senha é obrigatória.");
            }
            else if (usuario.Password.Contains(" "))
            {
                AddMensagem("A senha não pode conter espaço em branco.");
            }
            else if (usuario.Password.Length < 6)
            {
                AddMensagem("A senha deve conter no mínimo 6 caracteres.");
            }

            return await base.Validar(usuario);
        }

        public bool PermiteEditarSenha(string senhaAtual, string novaSenha)
        {
            if (senhaAtual == novaSenha)
            {
                AddMensagem("Nova senha não pode ser igual a senha atual.");
            }
            else
            {
                if (string.IsNullOrEmpty(novaSenha))
                {
                    AddMensagem("Senha é obrigatória.");
                }
                else if (novaSenha.Contains(" "))
                {
                    AddMensagem("A senha não pode conter espaço em branco.");
                }
                else if (novaSenha.Length < 6)
                {
                    AddMensagem("A senha deve conter no mínimo 6 caracteres.");
                }
            }

            return IsValido;
        }
    }
}