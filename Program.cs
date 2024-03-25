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
using System;
using System.IO;
using System.Text;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption.ConfigurationModel;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption;
using System.Security.Cryptography.X509Certificates;

var builder = WebApplication.CreateBuilder(args);

QuestPDF.Settings.License = LicenseType.Community;

// Buscando conexão com database para as tabelas das entidades
var entidadesConnectionString = builder.Configuration["ConnectionStrings:EntidadesConnection"];

builder.Services.AddDbContext<EntidadesContext>(opt =>
    opt.UseLazyLoadingProxies().UseMySql(entidadesConnectionString, ServerVersion.AutoDetect(entidadesConnectionString)));

// Buscando conexão com database para as tabelas de usuários
var usersConnectionString = builder.Configuration["ConnectionStrings:UsuariosConnection"];

builder.Services.AddDbContext<UsuariosContext>(opt =>
{
    opt.UseMySql(usersConnectionString, ServerVersion.AutoDetect(usersConnectionString));
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

// Adicionando serviço de autenticação por JWT 
builder.Services.AddAuthentication(opts =>
{
    opts.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(opts =>
{
    var symmetricKey = builder.Configuration["SymmetricSecurityKey_ApiFinanceiro"];
    if (symmetricKey != null)
    {
        opts.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(symmetricKey)),
            ValidateAudience = false,
            ValidateIssuer = false,
            ClockSkew = TimeSpan.Zero
        };
    }
    else
    {
        // Lidar com a falta da chave de segurança simétrica
        Console.WriteLine("A chave de segurança simétrica não foi encontrada nas configurações.");
    }

    // Imprimir a chave JWT para verificar se está sendo lida corretamente
    Console.WriteLine("Symmetric Security Key: " + symmetricKey);
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

// Configurando proteção de dados
builder.Services.AddDataProtection()
    .PersistKeysToFileSystem(new DirectoryInfo("/app/ExternalDataProtectionKeys"))
    .UseCryptographicAlgorithms(new AuthenticatedEncryptorConfiguration
    {
        EncryptionAlgorithm = EncryptionAlgorithm.AES_256_CBC,
        ValidationAlgorithm = ValidationAlgorithm.HMACSHA256
    });


//// Carregar o certificado pelo caminho do arquivo
//var certificatePath = "/etc/ssl/certs/certificado-autoassinado.pem";
//if (File.Exists(certificatePath))
//{
//    try
//    {
//        var certificate = new X509Certificate2(certificatePath);

//        // Use o certificado carregado para proteger as chaves de proteção de dados
//        builder.Services.AddDataProtection()
//            .PersistKeysToFileSystem(new DirectoryInfo("/app/ExternalDataProtectionKeys"))
//            .UseCryptographicAlgorithms(new AuthenticatedEncryptorConfiguration
//            {
//                EncryptionAlgorithm = EncryptionAlgorithm.AES_256_CBC,
//                ValidationAlgorithm = ValidationAlgorithm.HMACSHA256
//            })
//            .ProtectKeysWithCertificate(certificate);
//    }
//    catch (Exception ex)
//    {
//        Console.WriteLine($"Erro ao carregar o certificado: {ex.Message}");
//    }
//}
//else
//{
//    Console.WriteLine("O arquivo do certificado não foi encontrado no caminho especificado.");
//}





// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(opt =>
{
    opt.AddPolicy("AllowSpecificOrigin",
        builder =>
        {
            builder.WithOrigins("https://api2.stepone.com.br")
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

var app = builder.Build();

// CONFIGURAÇÃO DE PROXY REVERSO
app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});

app.UseCors(builder => builder
       .AllowAnyHeader()
       .AllowAnyMethod()
       .AllowAnyOrigin());

// Ambiente de produção
if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
