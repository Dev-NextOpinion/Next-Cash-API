using API_Financeiro_Next.Data.Dto;
using API_Financeiro_Next.Services;
using Microsoft.AspNetCore.Mvc;

namespace API_Financeiro_Next.Controllers;

[ApiController]
[Route("[controller]")]
public class UsuariosController : ControllerBase
{
    private UsuarioService _usuarioService;

    public UsuariosController(UsuarioService usuarioService)
    {
        _usuarioService = usuarioService;
    }

    [HttpPost("cadastro")]
    public async Task<IActionResult> CadastroAsync(CreateUsuarioDto cadastroDto)
    {
        await _usuarioService.RegisterUser(cadastroDto);
        return Ok("Usuário cadastrado com sucesso!");

    }

    [HttpPost("login")]
    public async Task<IActionResult> LoginAsync(LoginUsuarioDto loginDto)
    {
        var token = await _usuarioService.LoginUser(loginDto);
        return Ok(token);
    }

}
