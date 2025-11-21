namespace JDev.Tuteee.EmailConsumer;

using Clients;
using MassTransit;
using Grpc.Api.Messages;
using Razor.Templating.Core;
using Rest.ApiClient.ApiClients;

public class EmailHomeworkHandler(
    IRestApiClient restApiClient,
    IRazorTemplateEngine razorTemplateEngine,
    IEmailClient emailClient) : IConsumer<EmailHomeworkEvent>
{
    public async Task Consume(ConsumeContext<EmailHomeworkEvent> context)
    {
        var lesson = await restApiClient.GetLessonAsync(context.Message.LessonId, context.CancellationToken);
        var attachments =
            await restApiClient.GetHomeworkAttachmentsAsync(context.Message.LessonId, context.CancellationToken);
        var htmlTask = razorTemplateEngine.RenderAsync("EmailTemplates/Homework.cshtml", new EmailTemplates.Homework
        {
            FirstName = lesson.Tutee.FirstName,
            Instructions = lesson.HomeworkInstructions
        });
        await emailClient.SendAsync(new SendEmailRequest
        {
            ToAddress = lesson.Tutee.EmailAddress,
            CopyAddress = lesson.Tutee.Client.EmailAddress,
            Body = await htmlTask,
            Subject = $"Homework for {lesson.Date:D}",
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