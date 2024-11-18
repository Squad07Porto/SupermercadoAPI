namespace Supermercado.API.Models
{
    public class Secao
    {
        public int Id { get; set; }
        public string Descricao { get; set; } = string.Empty;
        public int FilialId { get; set; }
        public virtual Filial Filial { get; set; } = default!;
        public virtual ICollection<Produto> Produtos { get; set; } = [];
        public virtual ICollection<Equipamento> Equipamentos { get; set; } = [];
    }
}	