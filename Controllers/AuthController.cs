using BibliotecaApi.DTOs;
using BibliotecaApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace BibliotecaApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly UsuarioService _usuarioService;
    private readonly TokenService _tokenService;

    public AuthController(
        UsuarioService usuarioService,
        TokenService tokenService)
    {
        _usuarioService = usuarioService;
        _tokenService = tokenService;
    }

    [HttpPost("registro")]
    public async Task<IActionResult> Registrar(RegistroUsuarioDto registroDto)
    {
        var sucesso = await _usuarioService.RegistrarAsync(registroDto);

        if (!sucesso)
        {
            return BadRequest("E-mail já cadastrado.");
        }

        return Ok("Usuário cadastrado com sucesso.");
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto loginDto)
    {
        var usuario = await _usuarioService.LoginAsync(loginDto);

        if (usuario is null)
        {
            return Unauthorized("Usuário ou senha inválidos.");
        }

        var token = _tokenService.GerarToken(usuario);

        return Ok(new
        {
            token,
            usuario.Nome,
            usuario.Email,
            usuario.Perfil
        });
    }
}