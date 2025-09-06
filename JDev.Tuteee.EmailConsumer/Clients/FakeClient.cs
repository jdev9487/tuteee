namespace JDev.Tuteee.EmailConsumer.Clients;

using Auth;
using MailKit.Security;
using Microsoft.Extensions.Options;

public class FakeClient(IOptions<Email> options) : BaseClient(options)
{
    protected override SecureSocketOptions SecureSocketOptions => SecureSocketOptions.None;
}