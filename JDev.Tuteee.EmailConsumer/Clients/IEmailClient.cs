namespace JDev.Tuteee.EmailConsumer.Clients;

public interface IEmailClient
{
    Task SendAsync(SendEmailRequest request, CancellationToken token);
}

public class SendEmailRequest
{
    public required string ToAddress { get; init; } = "";
    public required string Subject { get; init; } = "";
    public required string Body { get; init; } = "";
    public IEnumerable<Attachment> Attachments { get; init; } = [];
}

public class Attachment
{
    public string FileName { get; init; } = "";
    public MemoryStream Stream { get; init; } = default!;
}