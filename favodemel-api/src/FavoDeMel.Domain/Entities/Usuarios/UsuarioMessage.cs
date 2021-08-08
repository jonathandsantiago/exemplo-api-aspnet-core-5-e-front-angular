namespace FavoDeMel.Domain.Entities.Usuarios
{
    public static class UsuarioMessage
    {
        public static string NomeObrigatorio => "Nome é obrigatório.";
        public static string LoginObrigatorio => "Login é obrigatório.";
        public static string LoginJaCadastrado => "Login já cadastrado";
        public static string PerfilInvalido => "Perfil do usuário invalído";
        public static string SenhaObrigatoria => "Senha é obrigatória.";
        public static string SenhaNaoPodeConterEspacoEmBranco => "A senha não pode conter espaço em branco.";
        public static string SenhaDeConterMinioCaracters(int minimo) => $"A senha deve conter no mínimo {minimo} caracteres.";
        public static string NovaSenhaNaoPodeSerIgualAtual => "Nova senha não pode ser igual a senha atual.";
        public static string UsuarioNaoPodeSerNulo => "Usuário Não pode ser nulo.";
        public static string UsuarioOuSenhaInvalida => "Usuário ou Senha inválido.";
    }
}
