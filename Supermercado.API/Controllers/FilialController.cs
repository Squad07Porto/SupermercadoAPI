using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Supermercado.API.Models;
using Supermercado.API.Models.DTO;
using Supermercado.API.Services.Interfaces;

namespace Supermercado.API.Controllers
{
    [ApiController]
    [Authorize]
    public class FilialController(IFilialService filialService, IMapper mapper) : ControllerBase
    {
        private readonly IFilialService _filialService = filialService;
        private readonly IMapper _mapper = mapper;

        [HttpGet("/BuscarFiliais")]
        public async Task<IActionResult> GetAll()
        {
            var filiais = await _filialService.GetAll();
            return Ok(filiais);
        }

        [HttpGet("/BuscarFilial/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var filial = await _filialService.GetById(id);
            if (filial is null)
            {
                return NotFound();
            }
            return Ok(filial);
        }

        [HttpPost("/AdicionarFilial")]
        public async Task<IActionResult> Add(FilialDTO filialDTO)
        {
            var filial = _mapper.Map<Filial>(filialDTO);
            await _filialService.Add(filial);
            return Ok();
        }

        [HttpPatch("/AtualizarFilial/{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] FilialDTO filialDTO)
        {
            var filial = _mapper.Map<Filial>(filialDTO);
            filial.Id = id;
            await _filialService.Update(filial);
            return Ok();
        }

        [HttpDelete("/DeletarFilial/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _filialService.Delete(id);
            return Ok();
        }
    }
}