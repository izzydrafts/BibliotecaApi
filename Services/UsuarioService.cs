using BibliotecaApi.DTOs;
using BibliotecaApi.Models;
using BibliotecaApi.Repositories;
using System.Security.Cryptography;
using System.Text;

namespace BibliotecaApi.Services;

public class UsuarioService
{
    private readonly UsuarioRepository _usuarioRepository;

    public UsuarioService(UsuarioRepository usuarioRepository)
    {
        _usuarioRepository = usuarioRepository;
    }

    public async Task<bool> RegistrarAsync(RegistroUsuarioDto registroDto)
    {
        var usuarioExistente = await _usuarioRepository.GetByEmailAsync(registroDto.Email);

        if (usuarioExistente is not null)
        {
            return false;
        }

        var usuario = new Usuario
        {
            Nome = registroDto.Nome,
            Email = registroDto.Email,
            SenhaHash = GerarHashSenha(registroDto.Senha),
            Perfil = registroDto.Perfil
        };

        await _usuarioRepository.CreateAsync(usuario);

        return true;
    }

    public async Task<Usuario?> LoginAsync(LoginDto loginDto)
    {
        var usuario = await _usuarioRepository.GetByEmailAsync(loginDto.Email);

        if (usuario is null)
        {
            return null;
        }

        var senhaHash = GerarHashSenha(loginDto.Senha);

        if (usuario.SenhaHash != senhaHash)
        {
            return null;
        }

        return usuario;
    }

    private string GerarHashSenha(string senha)
    {
        using var sha256 = SHA256.Create();

        var bytes = Encoding.UTF8.GetBytes(senha);

        var hash = sha256.ComputeHash(bytes);

        return Convert.ToBase64String(hash);
    }
}