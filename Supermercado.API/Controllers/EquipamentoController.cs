using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Supermercado.API.Models;
using Supermercado.API.Models.DTO;
using Supermercado.API.Services.Interfaces;

namespace Supermercado.API.Controllers
{
    [Authorize]
    [ApiController]
    public class EquipamentoController(IEquipamentoService equipamentoService, IMapper mapper) : ControllerBase
    {
        private readonly IEquipamentoService _equipamentoService = equipamentoService;
        private readonly IMapper _mapper = mapper;

        [HttpGet("BuscarEquipamentos")]
        public async Task<IActionResult> GetAll()
        {
            var equipamentos = await _equipamentoService.GetAll();
            var equipamentosDTO = _mapper.Map<IEnumerable<EquipamentoDTO>>(equipamentos);
            return Ok(equipamentosDTO);
        }

        [HttpGet("BuscarEquipamento/{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var equipamento = await _equipamentoService.GetById(id);
            if (equipamento == null)
            {
                return NotFound();
            }
            var equipamentoDTO = _mapper.Map<EquipamentoDTO>(equipamento);
            return Ok(equipamentoDTO);
        }

        [HttpPost("AdicionarEquipamento")]
        public async Task<IActionResult> Add([FromBody] EquipamentoDTO equipamentoDTO)
        {
            var equipamento = _mapper.Map<Equipamento>(equipamentoDTO);
            await _equipamentoService.Add(equipamento);
            return CreatedAtAction(nameof(Add), new { Mensagem = "Equipamento adicionado com sucesso" });
        }

        [HttpPatch("AtualizarEquipamento/{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] EquipamentoDTO equipamentoDTO)
        {
            var equipamento = _mapper.Map<Equipamento>(equipamentoDTO);
            equipamento.Id = id;
            await _equipamentoService.Update(equipamento);
            return Ok(new { Mensagem = "Equipamento atualizado com sucesso" });
        }

        [HttpDelete("DeletarEquipamento/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _equipamentoService.Delete(id);
            return Ok(new { Mensagem = "Equipamento deletado com sucesso" });
        }
    }

}