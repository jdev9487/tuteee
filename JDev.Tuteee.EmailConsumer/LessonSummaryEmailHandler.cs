namespace JDev.Tuteee.EmailConsumer;

using Clients;
using MassTransit;
using Grpc.Api.Messages;
using Razor.Templating.Core;
using Rest.ApiClient.ApiClients;

public class LessonSummaryEmailHandler(
    IRestApiClient restApiClient,
    IRazorTemplateEngine razorTemplateEngine,
    IEmailClient emailClient) : IConsumer<LessonSummaryEvent>
{
    public async Task Consume(ConsumeContext<LessonSummaryEvent> context)
    {
        var lesson = await restApiClient.GetLessonAsync(context.Message.LessonId, context.CancellationToken);
        var attachments =
            await restApiClient.GetLessonAttachmentsAsync(context.Message.LessonId, context.CancellationToken);
        var htmlTask = razorTemplateEngine.RenderAsync("EmailTemplates/LessonSummary.cshtml",
            new EmailTemplates.LessonSummary
            {
                FirstName = lesson.Tutee.FirstName,
                Instructions = lesson.HomeworkInstructions
            });
        await emailClient.SendAsync(new SendEmailRequest
        {
            ToAddress = lesson.Tutee.EmailAddress,
            CopyAddress = lesson.Tutee.Client.EmailAddress,
            Body = await htmlTask,
            Subject = $"Lesson on {lesson.Date:D}",
            Attachments = await Task.WhenAll(attachments.Select(async a =>
            {
                var bytes = (await restApiClient.GetLessonAttachmentAsync(a.LessonAttachmentId,
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