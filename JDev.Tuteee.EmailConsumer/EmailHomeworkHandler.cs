namespace JDev.Tuteee.EmailConsumer;

using Clients;
using MassTransit;
using Grpc.Api.Messages;

public class EmailHomeworkHandler(IEmailClient emailClient) : IConsumer<EmailHomeworkEvent>
{
    public async Task Consume(ConsumeContext<EmailHomeworkEvent> context)
    {
        await emailClient.SendAsync(new SendEmailRequest
        {
            ToAddress = context.Message.To,
            Body = "Hello",
            Subject = "testing"
        }, context.CancellationToken);
    }
}