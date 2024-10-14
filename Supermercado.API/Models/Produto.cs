namespace Supermercado.API.Models
{
    public class Produto
    {
        public int Id { get; set; }
        public string Codigo { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public decimal Preco { get; set; }
        public int SecaoId { get; set; }
        public virtual Secao Secao { get; set; } = new Secao();
    }
}