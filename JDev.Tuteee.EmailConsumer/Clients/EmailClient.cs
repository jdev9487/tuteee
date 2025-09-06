namespace JDev.Tuteee.EmailConsumer.Clients;

using MailKit.Security;
using Microsoft.Extensions.Options;

public class EmailClient(IOptions<Auth.Email> options) : BaseClient(options)
{
    protected override SecureSocketOptions SecureSocketOptions => SecureSocketOptions.StartTls;
}