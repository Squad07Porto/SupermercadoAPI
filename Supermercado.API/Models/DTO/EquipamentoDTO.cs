namespace Supermercado.API.Models.DTO
{
    public class EquipamentoDTO
    {
        public int Id { get; set; }
        public string? TipoEquipamento { get; set; }
        public string? Descricao { get; set; }
        public int SecaoId { get; set; }
        public string? SecaoDescricao { get; set; }
    }
}