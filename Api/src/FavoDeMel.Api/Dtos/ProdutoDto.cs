namespace FavoDeMel.Api.Dtos
{
    public class ProdutoDto : DtoBase<int>
    {
        public string Nome { get; set; }
        public decimal Preco { get; set; }
    }
}
