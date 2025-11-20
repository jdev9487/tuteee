namespace JDev.Tuteee.Rest.Api.Endpoints;

using ApiClient;
using AutoMapper;
using DAL.Entities;
using ApiClient.DTOs;
using Core.EfCore.Repository;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

public class LessonEndpoints(
    IMapper mapper,
    IOptions<AppSettings> options) : IEndpoints
{
    private readonly AppSettings _appSettings = options.Value;
    
    public void MapRoutes(IEndpointRouteBuilder routeBuilder)
    {
        var groupBuilder = routeBuilder.MapGroup($"/{Endpoint.LessonBase}");
        MapHomeworkAttachments(groupBuilder.MapGroup("/{lessonId:int}/homework-attachments"));
        
        groupBuilder.MapGet("",
            async (IGenericRepository repo, [FromQuery]DateOnly start, [FromQuery]DateOnly end, CancellationToken token) =>
            {
                var entities = (await repo.GetListAsync<Lesson>(token))
                    .Where(l => l.Date <= end)
                    .Where(l => l.Date >= start);
                return TypedResults.Ok(entities.Select(mapper.Map<LessonDto>));
            });
        
        groupBuilder.MapGet("/{id:int}",
            async Task<Results<Ok<LessonDto>, NotFound>> (int id, IGenericRepository repo, CancellationToken token) =>
            {
                var lesson = await repo.FindAsync<Lesson>(id, token);
                return lesson is null ? TypedResults.NotFound() : TypedResults.Ok(mapper.Map<LessonDto>(lesson));
            });
        
        groupBuilder.MapPost("",
            async (LessonDto dto, IGenericRepository repo, CancellationToken token) =>
            {
                var entity = mapper.Map<Lesson>(dto);
                await repo.AddAsync(entity, token);
                await repo.SaveChangesAsync(token);
                return TypedResults.Created();
            });
        
        groupBuilder.MapPatch("",
            async Task<Results<Ok, NotFound>>(LessonDto dto, IGenericRepository repo, CancellationToken token) =>
            {
                if (dto.LessonId is null) return TypedResults.NotFound();
                var existing = await repo.FindAsync<Lesson>(dto.LessonId.Value, token);
                if (existing is null) return TypedResults.NotFound();
                existing.HomeworkInstructions = dto.HomeworkInstructions;
                await repo.SaveChangesAsync(token);
                return TypedResults.Ok();
            });

        groupBuilder.MapDelete("/{id:int}",
            async Task<Results<NoContent, NotFound>>(int id, IGenericRepository repo, CancellationToken token) =>
            {
                var deletedLesson = await repo.DeleteAsync<Lesson>(id, token);
                await repo.SaveChangesAsync(token);
                return deletedLesson is null ? TypedResults.NotFound() : TypedResults.NoContent();
            });
    }

    private void MapHomeworkAttachments(IEndpointRouteBuilder group)
    {
        group.MapGet("",
            async Task<Results<Ok<IEnumerable<HomeworkAttachmentDto>>, NotFound>> (int lessonId, IGenericRepository repo, CancellationToken token) =>
            {
                var lesson = await repo.FindAsync<Lesson>(lessonId, token);
                if (lesson is not null)
                {
                    return TypedResults.Ok(lesson.HomeworkAttachments.Select(ha => new HomeworkAttachmentDto
                    {
                        HomeworkAttachmentId = ha.HomeworkAttachmentId,
                        FileName = Path.GetFileName(ha.Path),
                        LessonId = lessonId
                    }));
                }
                return TypedResults.NotFound();
            });
        
        group.MapPost("",
            async (int lessonId, HomeworkAttachmentDto dto, IGenericRepository repo, CancellationToken token) =>
            {
                Directory.CreateDirectory(Path.Join(_appSettings.AttachmentDirectory, lessonId.ToString()));
                var newFileName = Path.Join(_appSettings.AttachmentDirectory, lessonId.ToString(), dto.FileName);
                File.Move(Path.Join(Path.GetTempPath(), dto.TemporaryFileName), newFileName);
                var homeworkAttachment = new HomeworkAttachment
                {
                    LessonId = lessonId,
                    Path = newFileName
                };
                await repo.AddAsync(homeworkAttachment, token);
                await repo.SaveChangesAsync(token);
                return TypedResults.Created();
            });
    }
}