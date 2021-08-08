namespace FavoDeMel.Tests.Mocks.Parameters
{
    public class ServiceParameter
    {
        public MockUsuarioParameter UsuarioParameter { get; set; }
        public MockProdutoParameter ProdutoParameter { get; set; }
        public MockComandaParameter ComandaParameter { get; set; }

        public ServiceParameter(MockUsuarioParameter usuarioParameter)
        {
            UsuarioParameter = usuarioParameter;
        }

        public ServiceParameter(MockProdutoParameter produtoParameter)
        {
            ProdutoParameter = produtoParameter;
        }

        public ServiceParameter(MockComandaParameter comandaParameter)
        {
            ComandaParameter = comandaParameter;
        }
    }
}