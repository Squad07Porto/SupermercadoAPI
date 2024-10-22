namespace Supermercado.API.Models.DTO
{
  public class ProdutoDTO
  {
    public string? Codigo { get; set; }
    public string? Nome { get; set; }
    public decimal Preco { get; set; }
    public int Quantidade { get; set; }
    public int? SecaoId { get; set; }
  }
}
