namespace TutorTracker.Api.Controllers;

using Managers;
using AutoMapper;
using M = Model;
using E = Entities;

public class StudentController
{
    private readonly IStudentManager _studentManager;
    private readonly IMapper _mapper;

    public StudentController(IStudentManager studentManager, IMapper mapper)
    {
        _studentManager = studentManager;
        _mapper = mapper;
    }

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