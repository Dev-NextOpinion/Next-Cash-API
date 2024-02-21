using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace API_Financeiro_Next.Services;

public class EmailService
{
    private readonly string _smtpServer = "seu_servidor_smtp";
    private readonly int _smtpPort = 587; // Porta do servidor SMTP
    private readonly string _smtpUsername = "seu_email";
    private readonly string _smtpPassword = "sua_senha";

    public async Task EnviarEmailBoasVindas(string destinatario, string nomeUsuario)
    {
        var smtpClient = new SmtpClient(_smtpServer)
        {
            Port = _smtpPort,
            Credentials = new NetworkCredential(_smtpUsername, _smtpPassword),
            EnableSsl = true,
        };

        var mensagem = new MailMessage
        {
            From = new MailAddress(_smtpUsername),
            Subject = "Bem-vindo ao NextCash.",
            Body = $"Olá {nomeUsuario},\n\nBem-vindo ao NextCash! Agradecemos por se cadastrar!",
            IsBodyHtml = false,
        };

        mensagem.To.Add(destinatario);

        await smtpClient.SendMailAsync(mensagem);
    }
}
