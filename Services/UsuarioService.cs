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
    //private EmailService _emailService;

    public UsuarioService(IMapper mapper, 
        UserManager<Usuario> userManager, 
        SignInManager<Usuario> signInManager, 
        TokenService tokenService)
    {
        _mapper = mapper;
        _userManager = userManager;
        _signInManager = signInManager;
        _tokenService = tokenService;
    }

    public async Task RegisterUser(CreateUsuarioDto createUsuarioDto)
    {
        // Mapeando a classe de usuário
        Usuario user = _mapper.Map<Usuario>(createUsuarioDto);

        // Cadastrando um usuário no banco
        IdentityResult result = await
            _userManager.CreateAsync(user, createUsuarioDto.Password);

        // Se o resultado tiver sucesso
        if (!result.Succeeded)
        {
            throw new ApplicationException("Erro ao cadastrar usuário..");
        }

        // Mapeando a classe de usuário
        //Usuario user = _mapper.Map<Usuario>(createUsuarioDto);

        //// Cadastrando um usuário no banco
        //IdentityResult result = await _userManager.CreateAsync(user, createUsuarioDto.Password);

        //// Se o resultado tiver sucesso
        //if (result.Succeeded)
        //{
        //    // Envie o e-mail de boas-vindas
        //    await _emailService.EnviarEmailBoasVindas(createUsuarioDto.Email, createUsuarioDto.Username);
        //}
        //else
        //{
        //    throw new ApplicationException("Erro ao cadastrar usuário..");
        //}
    }

    public async Task<string> LoginUser(LoginUsuarioDto loginUsuarioDto)
    {
        var user = await _signInManager.UserManager
            .FindByEmailAsync(loginUsuarioDto.Email);

        if (user == null)
        {
            throw new ApplicationException("Erro ao acessar sua conta, verifique seus dados de email e senha..");
        }

        // se os dados estiverem ok, geramos um token
        var token = _tokenService.GenerateToken(user);

        return token;

    }
}
