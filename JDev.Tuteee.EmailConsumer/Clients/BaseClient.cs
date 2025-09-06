namespace JDev.Tuteee.EmailConsumer.Clients;

using MailKit.Net.Smtp;
using MimeKit;
using MailKit.Security;
using Microsoft.Extensions.Options;

public abstract class BaseClient(IOptions<Auth.Email> options) : IEmailClient
{
    private readonly Auth.Email _settings = options.Value;
    
    protected abstract SecureSocketOptions SecureSocketOptions { get; }

    public async Task SendAsync(SendEmailRequest request, CancellationToken token)
    {
        var email = new MimeMessage
        {
            Subject = request.Subject
        };
        email.From.Add(MailboxAddress.Parse(_settings.Username));
        email.To.Add(MailboxAddress.Parse(request.ToAddress));
        var bodyBuilder = new BodyBuilder { HtmlBody = request.Body };
        foreach (var attachment in request.Attachments)
        {
            await bodyBuilder.Attachments.AddAsync(attachment.FileName, attachment.Stream, token);
        }

        email.Body = bodyBuilder.ToMessageBody();

        using var smtp = new SmtpClient { CheckCertificateRevocation = false };
        await smtp.ConnectAsync(_settings.Server, _settings.Port, SecureSocketOptions, token);
        await smtp.AuthenticateAsync(_settings.Username, _settings.Password, token);
        await smtp.SendAsync(email, token);
        await smtp.DisconnectAsync(true, token);
    }
}