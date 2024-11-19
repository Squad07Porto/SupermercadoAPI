namespace Supermercado.API.Models
{
    public class Promocao
    {
        public int Id { get; set; }
        public int ProdutoId { get; set; }
        public int PrecoOriginal { get; set; }
        public int Desconto { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public Produto Produto { get; set; } = new Produto();
    }
}