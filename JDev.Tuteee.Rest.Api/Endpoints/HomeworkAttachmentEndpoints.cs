namespace JDev.Tuteee.Rest.Api.Endpoints;

using ApiClient;
using ApiClient.DTOs;
using Core.EfCore.Repository;
using DAL.Entities;
using Microsoft.AspNetCore.Http.HttpResults;

public class HomeworkAttachmentEndpoints : IEndpoints
{
    public void MapRoutes(IEndpointRouteBuilder routeBuilder)
    {
        var group = routeBuilder.MapGroup($"/{Endpoint.HomeworkAttachmentsBase}");
        group.MapGet("/{homeworkAttachmentId:int}",
            async Task<Results<Ok<FileDto>, NotFound>>(int homeworkAttachmentId, IGenericRepository repo, CancellationToken token) =>
            {
                var homeworkAttachment = await repo.FindAsync<HomeworkAttachment>(homeworkAttachmentId, token);
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
            async Task<Results<NoContent, NotFound>>(int homeworkAttachmentId, IGenericRepository repo, CancellationToken token) =>
            {
                var deleted = await repo.DeleteAsync<HomeworkAttachment>(homeworkAttachmentId, token);
                if (deleted is null) return TypedResults.NotFound();
                File.Delete(deleted.Path);
                await repo.SaveChangesAsync(token);
                return TypedResults.NoContent();
            });
        
    }
}