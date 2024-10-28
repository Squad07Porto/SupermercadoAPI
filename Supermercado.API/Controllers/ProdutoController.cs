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
    public class ProdutoController(IProdutoService produtoService, IMapper mapper) : ControllerBase
    {
        private readonly IProdutoService _produtoService = produtoService;
        private readonly IMapper _mapper = mapper;

        [HttpGet("BuscarProdutos")]
        public async Task<IActionResult> GetAll()
        {
            var produtos = await _produtoService.GetAll();
            return Ok(_mapper.Map<IEnumerable<ProdutoDTO>>(produtos));

        }

        [HttpGet("BuscarProduto/{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var produto = await _produtoService.GetById(id);
            if (produto is null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<ProdutoDTO>(produto));
        }

        [HttpPost("AdicionarProduto")]
        public async Task<IActionResult> Add([FromBody] ProdutoDTO produtoDTO)
        {
            var produto = _mapper.Map<Produto>(produtoDTO);
            await _produtoService.Add(produto);
            return CreatedAtAction(nameof(Add), new { Mensagem = "Produto adicionado com sucesso" });
        }

        [HttpPatch("AtualizarProduto/{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] ProdutoDTO produtoDTO)
        {
            var produto = _mapper.Map<Produto>(produtoDTO);
            produto.Id = id;
            await _produtoService.Update(produto);
            return Ok(new { Mensagem = "Produto atualizado com sucesso" });
        }

        [HttpDelete("RemoverProduto/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _produtoService.Delete(id);
            return Ok(new { Mensagem = "Produto removido com sucesso" });
        }
    }
}
