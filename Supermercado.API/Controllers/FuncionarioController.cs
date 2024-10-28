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
    public class FuncionarioController(IFuncionarioService funcionarioService, IMapper mapper) : ControllerBase
    {
        private readonly IFuncionarioService _funcionarioService = funcionarioService;
        private readonly IMapper _mapper = mapper;

        [HttpGet("BuscarFuncionarios")]
        public async Task<IActionResult> GetAll()
        {
            var funcionarios = await _funcionarioService.GetAll();
            return Ok(_mapper.Map<IEnumerable<FuncionarioDTO>>(funcionarios));
        }

        [HttpGet("BuscarFuncionario/{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var funcionario = await _funcionarioService.GetById(id);
            if (funcionario is null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<FuncionarioDTO>(funcionario));
        }

        [HttpPost("AdicionarFuncionario")]
        public async Task<IActionResult> Add([FromBody] FuncionarioDTO funcionarioDTO)
        {
            var funcionario = _mapper.Map<Funcionario>(funcionarioDTO);
            await _funcionarioService.Add(funcionario);
            return Ok(new { Mensagem = "Funcionário adicionado com sucesso!" });
        }

        [HttpPut("AtualizarFuncionario/{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] FuncionarioDTO funcionarioDTO)
        {
            var funcionario = _mapper.Map<Funcionario>(funcionarioDTO);
            funcionario.Id = id;
            await _funcionarioService.Update(funcionario);
            return Ok(new { Mensagem = "Funcionário atualizado com sucesso!" });
        }

        [HttpDelete("DeletarFuncionario/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _funcionarioService.Delete(id);
            return Ok(new { Mensagem = "Funcionário deletado com sucesso!" });
        }
    }
}