namespace JDev.Tuteee.EmailConsumer;

using Clients;
using MassTransit;
using Grpc.Api.Messages;
using MassTransit.Initializers;
using Rest.ApiClient.ApiClients;

public class EmailHomeworkHandler(
    IRestApiClient restApiClient,
    IEmailClient emailClient) : IConsumer<EmailHomeworkEvent>
{
    public async Task Consume(ConsumeContext<EmailHomeworkEvent> context)
    {
        var attachments =
            await restApiClient.GetHomeworkAttachmentsAsync(context.Message.LessonId, context.CancellationToken);
        await emailClient.SendAsync(new SendEmailRequest
        {
            ToAddress = context.Message.To,
            CopyAddress = context.Message.Copy,
            Body = context.Message.Body,
            Subject = $"Homework for {context.Message.Date}",
            Attachments = await Task.WhenAll(attachments.Select(async a =>
            {
                var bytes = (await restApiClient.GetHomeworkAttachmentAsync(a.HomeworkAttachmentId,
                    context.CancellationToken)).Contents;
                return new Attachment
                {
                    FileName = a.FileName,
                    Stream = new MemoryStream(bytes)
                };
            }))
        }, context.CancellationToken);
    }
}