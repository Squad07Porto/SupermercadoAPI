using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Supermercado.API.Models;
using Supermercado.API.Models.DTO;
using Supermercado.API.Services.Interfaces;

namespace Supermercado.API.Controllers
{
    [ApiController]
    public class ClienteController(IClienteService clienteService, IMapper mapper) : ControllerBase
    {
        private readonly IClienteService _clienteService = clienteService;
        private readonly IMapper _mapper = mapper;

        [HttpGet("BuscarClientes")]
        public async Task<IActionResult> GetAll()
        {
            var clientes = await _clienteService.GetAll();
            // return Ok(_mapper.Map<IEnumerable<ClienteDTO>>(clientes));
            return Ok(clientes);
        }

        [HttpGet("BuscarCliente/{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var cliente = await _clienteService.GetById(id);
            if (cliente is null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<ClienteDTO>(cliente));
        }

        [HttpPost("AdicionarCliente")]
        public async Task<IActionResult> Add([FromBody] ClienteDTO clienteDTO)
        {
            var cliente = _mapper.Map<Cliente>(clienteDTO);
            await _clienteService.Add(cliente);
            return CreatedAtAction(nameof(Add), new { Mensagem = "Cliente adicionado com sucesso" });
        }

        [HttpPatch("AtualizarCliente/{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] ClienteDTO clienteDTO)
        {
            var cliente = _mapper.Map<Cliente>(clienteDTO);
            cliente.Id = id;
            await _clienteService.Update(cliente);
            return Ok(new { Mensagem = "Cliente atualizado com sucesso" });
        }

        [HttpDelete("RemoverCliente/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _clienteService.Delete(id);
            return Ok(new { Mensagem = "Cliente removido com sucesso" });
        }
    }
}