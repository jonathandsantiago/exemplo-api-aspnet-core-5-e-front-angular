namespace FavoDeMel.Domain.Models.Routes
{
    public class Endpoints
    {
        public static class Rotas
        {
            public const string Cadastrar = "cadastrar";
            public const string Editar = "editar";
            public const string ObterTodosPaginado = "listar-paginado";
            public const string ObterTodos = "listar-todos";
            public const string ObterPorId = "{id}";
            public const string TesteMessage = "teste-message";
        }

        public static class Recursos
        {
            public const string Comandas = "comandas";
            public const string Usuarios = "usuarios";
            public const string Produtos = "produtos";
        }

        public static class UsuarioApi
        {
            public const string Login = "login";
            public const string AlterarSenha = "alterar-senha";
            public const string ObterTodosPorPerfil = "listar-por-perfil/{perfil}";
        }

        public static class ComandaApi
        {
            public const string Confirmar = "confirmar";
            public const string Fechar = "fechar";
            public const string ObterTodosPorSituacao = "listar-por-situacao/{situacao}";
            public const string ObterPaginadoPorSituacaoAsync = "listar-paginado-por-situacao";
        }
    }
}