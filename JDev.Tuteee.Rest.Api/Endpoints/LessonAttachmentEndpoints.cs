namespace JDev.Tuteee.Rest.Api.Endpoints;

using ApiClient;
using ApiClient.DTOs;
using Core.EfCore.Repository;
using DAL.Entities;
using Microsoft.AspNetCore.Http.HttpResults;

public class LessonAttachmentEndpoints : IEndpoints
{
    public void MapRoutes(IEndpointRouteBuilder routeBuilder)
    {
        var group = routeBuilder.MapGroup($"/{Endpoint.LessonAttachmentsBase}");
        group.MapGet("/{lessonAttachmentId:int}",
            async Task<Results<Ok<FileDto>, NotFound>>(int lessonAttachmentId, IGenericRepository repo, CancellationToken token) =>
            {
                var lessonAttachment = await repo.FindAsync<LessonAttachment>(lessonAttachmentId, token);
                if (lessonAttachment is null) return TypedResults.NotFound();
                if (File.Exists(lessonAttachment.Path))
                    return TypedResults.Ok(new FileDto
                    {
                        FileName = lessonAttachment.Path,
                        Contents = await File.ReadAllBytesAsync(lessonAttachment.Path, token)
                    });
                return TypedResults.NotFound();
            });
        
        group.MapDelete("/{lessonAttachmentId:int}",
            async Task<Results<NoContent, NotFound>>(int lessonAttachmentId, IGenericRepository repo, CancellationToken token) =>
            {
                var deleted = await repo.DeleteAsync<LessonAttachment>(lessonAttachmentId, token);
                if (deleted is null) return TypedResults.NotFound();
                File.Delete(deleted.Path);
                await repo.SaveChangesAsync(token);
                return TypedResults.NoContent();
            });
        
    }
}