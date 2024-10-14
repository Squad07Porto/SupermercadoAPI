using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Supermercado.API.Models;
using Supermercado.API.Models.DTO;
using Supermercado.API.Services.Interfaces;

namespace Supermercado.API.Controllers
{
    [ApiController]
    public class SecaoController(ISecaoService secaoService, IMapper mapper) : ControllerBase
    {
        private readonly ISecaoService _secaoService = secaoService;
        private readonly IMapper _mapper = mapper;

        [HttpGet("BuscarSecoes")]
        public async Task<IActionResult> GetAll()
        {
            var secoes = await _secaoService.GetAll();
            var secoesDTO = _mapper.Map<IEnumerable<SecaoDTO>>(secoes);
            return Ok(secoesDTO);
        }

        [HttpGet("BuscarSecao/{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var secao = await _secaoService.GetById(id);
            if (secao == null)
            {
                return NotFound();
            }
            var secaoDTO = _mapper.Map<SecaoDTO>(secao);
            return Ok(secaoDTO);
        }

        [HttpPost("AdicionarSecao")]
        public async Task<IActionResult> Add([FromBody] SecaoDTO secaoDTO)
        {
            var secao = _mapper.Map<Secao>(secaoDTO);
            await _secaoService.Add(secao);
            return CreatedAtAction(nameof(Add), new { Mensagem = "Seção adicionada com sucesso" });
        }

        [HttpPatch("AtualizarSecao/{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] SecaoDTO secaoDTO)
        {
            var secao = _mapper.Map<Secao>(secaoDTO);
            secao.Id = id;
            await _secaoService.Update(secao);
            return Ok(new { Mensagem = "Seção atualizada com sucesso" });
        }

        [HttpDelete("DeletarSecao/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _secaoService.Delete(id);
            return Ok(new { Mensagem = "Seção deletada com sucesso" });
        }
    }
}