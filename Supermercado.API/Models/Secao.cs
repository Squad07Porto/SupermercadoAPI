using Microsoft.AspNetCore.SignalR;

namespace Supermercado.API.Models
{
    public class Secao
    {
        public int Id { get; set; }
        public string Descricao { get; set; } = string.Empty;
        public virtual ICollection<Produto> Produtos { get; set; } = [];
        public virtual ICollection<Equipamento> Equipamentos { get; set; } = [];
    }
}	