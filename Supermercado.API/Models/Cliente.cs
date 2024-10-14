namespace Supermercado.API.Models
{
    public class Cliente : Pessoa
    {
        public string Email { get; set; } = string.Empty;
        public string Telefone { get; set; } = string.Empty;
    }
}