using API_Financeiro_Next.Data;
using API_Financeiro_Next.Data.Dto;
using API_Financeiro_Next.Services;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace API_Financeiro_Next.Controllers;

[ApiController]
[Route("[controller]")]
public class UsuariosController : ControllerBase
{
    private UsuarioService _usuarioService;
    private IMapper _mapper;
    private UsuariosContext _context;

    public UsuariosController(UsuarioService usuarioService, 
        IMapper mapper, UsuariosContext context)
    {
        _usuarioService = usuarioService;
        _mapper = mapper;
        _context = context;
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

    [HttpPost("reset-password/")]
    public async Task<IActionResult> ResetPasswordAsync(string email)
    {
        var token = await _usuarioService.PasswordReset(email);
        return Ok(new { Message = "Email de recuperação de senha enviado com sucesso!", Token = token });
    }

    [HttpPost("reset-password-confirm")]
    public async Task<IActionResult> ResetPasswordConfirmAsync(ResetPasswordDto resetPasswordDto)
    {
        await _usuarioService.ChangePassword(resetPasswordDto.Email,
            resetPasswordDto.Token, resetPasswordDto.NewPassword);
        return Ok("Senha alterada com sucesso!");
    }

    [HttpGet]
    public IEnumerable<ReadUsuariosDto> GetUsuarios()
    {
        return _mapper.Map<List<ReadUsuariosDto>>(
          _context.Users.ToList());
    }

    [HttpPut("{id}")]
    public IActionResult UpdateCadastro(string id,
        [FromBody] UpdateUsuarioDto updateUsuarioDto)
    {
        var user = _context.Users.FirstOrDefault(
            user =>  user.Id == id);
        if (user == null) NotFound();

        _mapper.Map(updateUsuarioDto, user);
        _context.SaveChanges();
        return NoContent();
    }


   
}
