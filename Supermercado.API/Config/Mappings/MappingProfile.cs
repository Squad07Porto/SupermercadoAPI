using AutoMapper;
using Supermercado.API.Models;
using Supermercado.API.Models.DTO;

namespace Supermercado.API.Config.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Cliente, ClienteDTO>().ReverseMap();
            CreateMap<Funcionario, FuncionarioDTO>().ReverseMap();
            CreateMap<Secao, SecaoDTO>().ReverseMap();
            CreateMap<Produto, ProdutoDTO>().ReverseMap();
            CreateMap<Filial, FilialDTO>().ReverseMap();
            CreateMap<Equipamento, EquipamentoDTO>().ReverseMap();
            CreateMap<Usuario, UsuarioDTO>().ReverseMap();
            CreateMap<Usuario, UsuarioLoginDTO>().ReverseMap();
        }
    }
}