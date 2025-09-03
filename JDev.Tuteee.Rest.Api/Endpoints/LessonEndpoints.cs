namespace JDev.Tuteee.Rest.Api.Endpoints;

using ApiClient;
using AutoMapper;
using ApiClient.DTOs;
using DAL;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Options;

public class LessonEndpoints(IMapper mapper, IOptions<AppSettings> options) : IEndpoints
{
    private readonly AppSettings _appSettings = options.Value;
    
    public void MapRoutes(IEndpointRouteBuilder routeBuilder)
    {
        var groupBuilder = routeBuilder.MapGroup($"/{Endpoint.LessonBase}");
        MapHomeworkAttachments(groupBuilder.MapGroup("/{lessonId:int}/homework-attachments"));
        
        groupBuilder.MapGet("/{id:int}",
            async Task<Results<Ok<LessonDto>, NotFound>> (int id, Context context, CancellationToken token) =>
            {
                var lesson = await context.Lessons
                    .SingleOrDefaultAsync(l => l.LessonId == id, cancellationToken: token);
                return lesson is null ? TypedResults.NotFound() : TypedResults.Ok(mapper.Map<LessonDto>(lesson));
            });
        
        groupBuilder.MapPost("",
            async (LessonDto dto, Context context, CancellationToken token) =>
            {
                var entity = mapper.Map<Lesson>(dto);
                await context.Lessons.AddAsync(entity, token);
                await context.SaveChangesAsync(token);
                return TypedResults.Created();
            });
    }

    private void MapHomeworkAttachments(IEndpointRouteBuilder group)
    {
        group.MapGet("",
            async Task<Results<Ok<IEnumerable<HomeworkAttachmentDto>>, NotFound>> (int lessonId, Context context, CancellationToken token) =>
            {
                var lesson = await context.Lessons.FindAsync([lessonId], cancellationToken: token);
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
            async (int lessonId, HomeworkAttachmentDto dto, Context context, CancellationToken token) =>
            {
                Directory.CreateDirectory(Path.Join(_appSettings.AttachmentDirectory, lessonId.ToString()));
                var newFileName = Path.Join(_appSettings.AttachmentDirectory, lessonId.ToString(), dto.FileName);
                File.Move(Path.Join(_appSettings.TempDirectory, dto.TemporaryFileName), newFileName);
                var homeworkAttachment = new HomeworkAttachment
                {
                    LessonId = lessonId,
                    Path = newFileName
                };
                await context.HomeworkAttachments.AddAsync(homeworkAttachment, token);
                await context.SaveChangesAsync(token);
                return TypedResults.Created();
            });
    }
}