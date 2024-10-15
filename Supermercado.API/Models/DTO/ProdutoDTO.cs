namespace Supermercado.API.Models.DTO
{
  public class ProdutoDTO
  {
    public int Id { get; set; }
    public string? Codigo { get; set; }
    public string? Descricao { get; set; }
    public decimal Preco { get; set; }
    public int SecaoId { get; set; }
    public string? SecaoDescricao { get; set; }
  }
}
