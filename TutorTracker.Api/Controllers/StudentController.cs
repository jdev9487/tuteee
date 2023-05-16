namespace TutorTracker.Api.Controllers;

using Managers;
using AutoMapper;
using M = Model;
using E = Entities;
using CR = CustomResults;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("/[controller]s")]
public class StudentController
{
    private readonly IStudentManager _studentManager;
    private readonly IMapper _mapper;

    public StudentController(IStudentManager studentManager, IMapper mapper)
    {
        _studentManager = studentManager;
        _mapper = mapper;
    }

    [HttpGet("")]
    public async Task<IResult> GetStudentsAsync(CancellationToken token)
    {
        try
        {
            return Results.Ok(
                (await _studentManager.GetStudentsAsync(token)).Select(x => _mapper.Map<M.StudentResult>(x)));
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }

    [HttpGet("{studentId:guid}")]
    public async Task<IResult> GetStudentAsync(Guid studentId, CancellationToken token)
    {
        try
        {
            var student = await _studentManager.GetStudentAsync(studentId, token);
            return student is null ? Results.BadRequest() : Results.Ok(_mapper.Map<M.StudentResult>(student));
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }

    [HttpGet("{studentId:guid}/lessons")]
    public async Task<IResult> GetLessonsAssociatedWithStudent(Guid studentId, CancellationToken token)
    {
        try
        {
            return Results.Ok(
                (await _studentManager.GetLessonsAssociatedWithStudent(studentId, token)).Select(x =>
                    _mapper.Map<M.LessonResult>(x)));
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }

    [HttpPost("")]
    public async Task<IResult> CreateStudentAsync(M.CreateStudent createStudent, CancellationToken token)
    {
        try
        {
            var studentEntity = _mapper.Map<E.Student>(createStudent);
            var id = await _studentManager.CreateStudentAsync(studentEntity, createStudent.InvoiceeId, token);
            return id is null ? Results.BadRequest() : Results.Ok(id);
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }
}