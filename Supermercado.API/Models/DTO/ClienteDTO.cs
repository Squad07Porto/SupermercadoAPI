namespace Supermercado.API.Models.DTO
{
    public class ClienteDTO : PessoaDTO
    {
        public string? Email { get; set; }
        public string? Telefone { get; set; }
    }
}