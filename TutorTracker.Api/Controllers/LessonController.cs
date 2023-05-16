namespace TutorTracker.Api.Controllers;

using Managers;
using M = Model;
using AutoMapper;
using E = Entities;
using CR = CustomResults;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("/[controller]s")]
public class LessonController
{
    private readonly IMapper _mapper;
    private readonly ILessonManager _lessonManager;

    public LessonController(IMapper mapper, ILessonManager lessonManager)
    {
        _mapper = mapper;
        _lessonManager = lessonManager;
    }

    [HttpPost("")]
    public async Task<IResult> CreateLessonAsync(M.Lesson lesson, CancellationToken token)
    {
        try
        {
            var lessonEntity = _mapper.Map<E.Lesson>(lesson);
            var id = await _lessonManager.CreateLessonAsync(lessonEntity, lesson.StudentId, token);
            return id is null ? Results.BadRequest() : Results.Ok(id);
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }

    [HttpPatch("")]
    public async Task<IResult> UpdateLessonAsync(M.UpdateLesson updateLesson, CancellationToken token)
    {
        try
        {
            var updated = await _lessonManager.UpdateLessonAsync(updateLesson, token);
            return updated is null ? Results.BadRequest() : Results.Ok(_mapper.Map<M.LessonResult>(updated));    
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }
    
    [HttpDelete("{lessonId:guid}")]
    public async Task<IResult> DeleteLessonAsync(Guid lessonId, CancellationToken token)
    {
        try
        {
            var deleted = await _lessonManager.DeleteLessonAsync(lessonId, token);
            return deleted is null ? Results.NotFound() : Results.Ok();
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }
}