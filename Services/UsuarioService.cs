using API_Financeiro_Next.Data.Dto;
using API_Financeiro_Next.Models;
using AutoMapper;
using Microsoft.AspNetCore.Identity;

namespace API_Financeiro_Next.Services;

public class UsuarioService
{
    private IMapper _mapper;
    private UserManager<Usuario> _userManager;
    private  SignInManager<Usuario> _signInManager;
    private TokenService _tokenService;
    private EmailService _emailService;

    public UsuarioService(IMapper mapper,
        UserManager<Usuario> userManager,
        SignInManager<Usuario> signInManager,
        TokenService tokenService,
        EmailService emailService)
    {
        _mapper = mapper;
        _userManager = userManager;
        _signInManager = signInManager;
        _tokenService = tokenService;
        _emailService = emailService;
    }

    public async Task RegisterUser(CreateUsuarioDto createUsuarioDto)
    {
       // Mapeando a classe de usuário
       Usuario user = _mapper.Map<Usuario>(createUsuarioDto);

        // Cadastrando um usuário no banco
        IdentityResult result = await _userManager.CreateAsync(user, createUsuarioDto.Password);

        // Se o resultado tiver sucesso
        if (result.Succeeded)
        {
            // Envie o e-mail de boas-vindas
            await _emailService.WelcomeEmail(createUsuarioDto.Email, createUsuarioDto.Username);
        }
        else
        {
            throw new ApplicationException("Erro ao cadastrar usuário..");
        }
    }

    public async Task<string> LoginUser(LoginUsuarioDto loginUsuarioDto)
    {
        // verificando email do usuário
        var user = await _signInManager.UserManager
            .FindByEmailAsync(loginUsuarioDto.Email);

        // Se o email do user for nulo/não existir
        if (user == null)
        {
            throw new ApplicationException("Erro ao acessar sua conta, verifique seus dados de email e senha..");
        }

        // se os dados estiverem ok, geramos um token
        var token = _tokenService.GenerateToken(user);

        return token;

    }

    public async Task PasswordReset(string email)
    {
        // verificando email do usuário
        var user = await _userManager.FindByEmailAsync(email);

        // se o email do user for nulo/não existir
        if(user == null)
        {
            throw new ApplicationException("Usuário não encontrado");
        }

        // gerando um token para reset de senha
        var token = await _userManager.GeneratePasswordResetTokenAsync(user);

        await _emailService.PasswordResetEmail(email, token);
    }
}
