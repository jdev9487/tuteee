namespace JDev.Tuteee.EmailConsumer.Clients;

using MimeKit;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;

public class EmailClient(IOptions<Auth.Email> options) : IEmailClient
{
    private readonly Auth.Email _settings = options.Value;

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
        await smtp.ConnectAsync(_settings.Server, _settings.Port, _settings.Security, token);
        await smtp.AuthenticateAsync(_settings.Username, _settings.Password, token);
        await smtp.SendAsync(email, token);
        await smtp.DisconnectAsync(true, token);
    }
}