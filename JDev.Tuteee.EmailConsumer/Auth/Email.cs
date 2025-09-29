namespace JDev.Tuteee.EmailConsumer.Auth;

using MailKit.Security;

public class Email
{
    public SecureSocketOptions Security { get; set; }
    public string Server { get; set; } = "";
    public int Port { get; set; }
    public string Username { get; set; } = "";
    public string Password { get; set; } = "";
}