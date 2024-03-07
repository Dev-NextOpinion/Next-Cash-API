using API_Financeiro_Next.Authorization;
using API_Financeiro_Next.Data;
using API_Financeiro_Next.Models;
using API_Financeiro_Next.Services;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using QuestPDF.Infrastructure;
using System.Text;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

QuestPDF.Settings.License = LicenseType.Community;

// Buscando conexão com database para as tabelas das entidades
var entidadesConnectionString = builder.Configuration["ConnectionStrings:EntidadesConnection"];

builder.Services.AddDbContext<EntidadesContext>(opt =>
opt.UseLazyLoadingProxies().UseMySql(entidadesConnectionString,
ServerVersion.AutoDetect(entidadesConnectionString)));

// Buscando conexão com database para as tabelas de usuários
var usersConnectionString = builder.Configuration["ConnectionStrings:UsuariosConnection"];

builder.Services.AddDbContext<UsuariosContext>(opt =>
{
    opt.UseMySql(usersConnectionString, 
        ServerVersion.AutoDetect(usersConnectionString));
});

builder.Services
    .AddIdentity<Usuario, IdentityRole>()
    .AddEntityFrameworkStores<UsuariosContext>()
    .AddDefaultTokenProviders();

// Adicionando o services
builder.Services.AddScoped<UsuarioService>();

builder.Services.AddScoped<TokenService>();

builder.Services.AddScoped<EmailService>();

builder.Services.AddScoped<CategoriaService>();

// Adicionando o reset de senha
builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequiredLength = 11;

});

// CONFIGURAÇÃO DE PROXY REVERSO - HEADERS
builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    options.KnownProxies.Add(IPAddress.Parse("135.181.28.87"));
});

// Adicionando serviço de autenticação por JWT 
builder.Services.AddAuthentication(opts =>
{
    opts.DefaultAuthenticateScheme =
    JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(opts =>
{
    opts.TokenValidationParameters = new
    Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey
            (Encoding.UTF8.GetBytes(builder.Configuration["SymmetricSecurityKey_ApiFinanceiro"])),
        ValidateAudience = false,
        ValidateIssuer = false,
        ClockSkew = TimeSpan.Zero
    };

});


// Adicionando autorizações 
builder.Services.AddAuthorization(opts =>
{
    opts.AddPolicy("AuthenticationUser", policy =>
    {
        policy.AddRequirements(new AuthenticationUser());
    });
});

builder.Services.AddSingleton<IAuthorizationHandler, CategoriasAuthorization>();

// Adicionando o mapeamento de DTOs
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddControllers().AddNewtonsoftJson();

builder.Services.AddHttpContextAccessor();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


// CONFIGURAÇÃO DE PROXY REVERSO
app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});

/*
 O CORS (Cross-origin Resource Sharing) é um mecanismo utilizado pelos navegadores
para compartilhar recursos entre diferentes origens. O CORS é uma especificação do 
W3C e faz uso de headers do HTTP para informar aos navegadores se determinado recurso 
pode ser ou não acessado.
  */

app.UseCors(builder => builder
       .AllowAnyHeader()
       .AllowAnyMethod()
       .AllowAnyOrigin()
    );

//// Ambiente de desenvolvimento
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();

}

//// Ambiente de produção
//if (!app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//    app.UseDeveloperExceptionPage();
//    app.UseHttpsRedirection();

//}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.MapGet("/", () => "135.181.28.87");

app.Run();
