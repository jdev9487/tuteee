namespace JDev.Tuteee.EmailConsumer;

using Clients;
using MassTransit;
using Grpc.Api.Messages;

public class EmailHomeworkHandler(
    IEmailClient emailClient) : IConsumer<EmailHomeworkEvent>
{
    public Task Consume(ConsumeContext<EmailHomeworkEvent> context) =>
        emailClient.SendAsync(new SendEmailRequest
        {
            ToAddress = context.Message.To,
            Body = context.Message.Body,
            Subject = $"Homework for {context.Message.Date}"
        }, context.CancellationToken);
}