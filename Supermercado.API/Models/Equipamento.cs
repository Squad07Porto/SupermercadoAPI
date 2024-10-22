namespace Supermercado.API.Models
{
    public class Equipamento
    {
        public int Id { get; set; }
        public string TipoEquipamento { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public int SecaoId { get; set; }
        public virtual Secao Secao { get; set; } = new Secao();
    }
}