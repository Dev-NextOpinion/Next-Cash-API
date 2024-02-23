using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace API_Financeiro_Next.Services;

public class EmailService
{
    private readonly string _smtpServer = "mail.nextopinion.com.pt";
    private readonly int _smtpPort = 587; // Porta do servidor SMTP
    private readonly string _smtpUsername = "dev@nextopinion.com.pt";
    private readonly string _smtpPassword = "@Next2024";

    public async Task WelcomeEmail(string destinatario, string nomeUsuario)
    {
        // Configurando SMPT
        var smtpClient = new SmtpClient(_smtpServer)
        {
            Port = _smtpPort,
            Credentials = new NetworkCredential(_smtpUsername, _smtpPassword),
            EnableSsl = true,
        };

        // Mensagem do corpo do email
        var mensagem = new MailMessage
        {
            From = new MailAddress(_smtpUsername),
            Subject = "Bem-vindo ao NextCash.",
            Body = $"Olá {nomeUsuario},\n\nBem-vindo ao NextCash! Agradecemos por se cadastrar!",
            IsBodyHtml = false,
        };

        // enviando email para o destinatário
        mensagem.To.Add(destinatario);
        
        await smtpClient.SendMailAsync(mensagem);
    }

    public async Task PasswordResetEmail(string destinatario, string token)
    {
        // Configurando SMPT
        var smtpClient = new SmtpClient(_smtpServer)
        {
            Port = _smtpPort,
            Credentials = new NetworkCredential(_smtpUsername, _smtpPassword),
            EnableSsl = true,
        };

        // Link para acesso de reset por token
        var resetUrl = $"https://localhost:7204/reset-password/?token={WebUtility.UrlEncode(token)}";



        // Mensagem do corpo do email
        var mensagem = new MailMessage
        {
            From = new MailAddress(_smtpUsername),
            Subject = "Recuperação de Senha - NextCash",
            Body = $"Olá,\n\nVocê solicitou a recuperação de senha para a sua conta no NextCash. Clique no link a seguir para redefinir sua senha:\n{resetUrl}\n\nSe você não solicitou esta recuperação, ignore este e-mail.",
            IsBodyHtml = false,
        };

        // enviando email para o destinatário
        mensagem.To.Add(destinatario);

        await smtpClient.SendMailAsync(mensagem);


    }
}
