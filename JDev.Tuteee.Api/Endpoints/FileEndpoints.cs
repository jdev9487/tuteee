namespace JDev.Tuteee.Api.Endpoints;

using DB;
using ApiClient.DTOs;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

public class FileEndpoints : IEndpoints
{
    public const string TempDirectory = "/Users/johngould/Desktop";
    public const string AttachmentDirectory = "/Users/johngould/temp";
    public void MapRoutes(IEndpointRouteBuilder routeBuilder)
    {
        routeBuilder.MapPost("/homework-files",
            async (HomeworkAttachmentDto dto, Context context, CancellationToken token) =>
            {
                var lesson = await context.Lessons
                    .SingleOrDefaultAsync(l => l.LessonId == dto.LessonId, cancellationToken: token);
                Directory.CreateDirectory(Path.Join(AttachmentDirectory, lesson.LessonId.ToString()));
                File.Move(Path.Join(TempDirectory, dto.TemporaryFileName),
                    Path.Join(AttachmentDirectory, lesson.LessonId.ToString(), dto.FileName));
                return TypedResults.Created();
            });

        routeBuilder.MapGet("/homework-files/{lessonId:int}",
            async Task<Results<Ok<IEnumerable<HomeworkAttachmentDto>>, NotFound>> (int lessonId) =>
        {
            var attemptedDirectory = Path.Join(AttachmentDirectory, lessonId.ToString());
            Directory.CreateDirectory(attemptedDirectory);
            return await Task.FromResult(
                TypedResults.Ok(Directory.EnumerateFiles(attemptedDirectory)
                    .Select(file => new HomeworkAttachmentDto
                    {
                        FileName = file
                    })));
        });
        
        routeBuilder.MapPost("/temporary-files",
            async (TemporaryFileDto dto, CancellationToken token) =>
            {
                await File.WriteAllBytesAsync(Path.Join(TempDirectory, dto.Filename), dto.Contents, token);
                return TypedResults.Created();
            });
        routeBuilder.MapGet("/temporary-files/{filename}",
            async (string filename, CancellationToken token) =>
            {
                var bytes = await File.ReadAllBytesAsync(Path.Join(TempDirectory, filename), token);
                return TypedResults.Ok(new TemporaryFileDto
                {
                    Contents = bytes,
                    Filename = filename
                });
            });
    }
}