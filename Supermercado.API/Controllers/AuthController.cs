using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Supermercado.API.Models;
using Supermercado.API.Models.DTO;
using Supermercado.API.Services.Interfaces;

namespace Supermercado.API.Controllers
{
    [ApiController]
    [Route("Auth")]
    public class AuthController(IUsuarioService usuarioService, IMapper mapper) : ControllerBase
    {
        private readonly IUsuarioService _usuarioService = usuarioService;
        private readonly IMapper _mapper = mapper;

        [HttpPost("Cadastrar")]
        public async Task<IActionResult> Cadastrar([FromBody] UsuarioDTO usuarioDTO)
        {
            var usuario = _mapper.Map<Usuario>(usuarioDTO);
            await _usuarioService.RegisterUserAsync(usuario);
            return Ok(new { Mensagem = "Usuário cadastrado com sucesso" });
        }

        [HttpPost("Login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Login([FromBody] UsuarioDTO usuarioDTO)
        {
            var usuario = _mapper.Map<Usuario>(usuarioDTO);
            var token = await _usuarioService.AuthenticateUserAsync(usuario);

            if (token == null)
                return Unauthorized(new { Erro = "Usuário ou senha inválidos" });

            return Ok(new { Token = token });
        }
    }
}