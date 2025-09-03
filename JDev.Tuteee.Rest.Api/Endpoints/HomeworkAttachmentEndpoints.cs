namespace JDev.Tuteee.Rest.Api.Endpoints;

using ApiClient;
using ApiClient.DTOs;
using DAL;
using Microsoft.AspNetCore.Http.HttpResults;

public class HomeworkAttachmentEndpoints : IEndpoints
{
    public void MapRoutes(IEndpointRouteBuilder routeBuilder)
    {
        var group = routeBuilder.MapGroup($"/{Endpoint.HomeworkAttachmentsBase}");
        group.MapGet("/{homeworkAttachmentId:int}",
            async Task<Results<Ok<FileDto>, NotFound>>(int homeworkAttachmentId, Context context, CancellationToken token) =>
            {
                var homeworkAttachment =
                    await context.HomeworkAttachments.FindAsync([homeworkAttachmentId], cancellationToken: token);
                if (homeworkAttachment is null) return TypedResults.NotFound();
                if (File.Exists(homeworkAttachment.Path))
                    return TypedResults.Ok(new FileDto
                    {
                        FileName = homeworkAttachment.Path,
                        Contents = await File.ReadAllBytesAsync(homeworkAttachment.Path, token)
                    });
                return TypedResults.NotFound();
            });
        
        group.MapDelete("/{homeworkAttachmentId:int}",
            async Task<Results<NoContent, NotFound>>(int homeworkAttachmentId, Context context, CancellationToken token) =>
            {
                var homeworkAttachment =
                    await context.HomeworkAttachments.FindAsync([homeworkAttachmentId], cancellationToken: token);
                if (homeworkAttachment is null) return TypedResults.NotFound();
                context.HomeworkAttachments.Remove(homeworkAttachment);
                File.Delete(homeworkAttachment.Path);
                await context.SaveChangesAsync(token);
                return TypedResults.NoContent();
            });
        
    }
}