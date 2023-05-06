using TutorTracker.Api.Entities;
using TutorTracker.Api.Repositories;

namespace TutorTracker.Api.Routing;

using M = Model;
using AutoMapper;

internal static partial class WebApplicationExtensions
{
    internal static void MapStudentEndpoints(this WebApplication app)
    {
        var repo = app.Services.GetRequiredService<IRepository>();
        var mapper = app.Services.GetRequiredService<IMapper>();

        app.MapPost("/students", async (M.Student student, CancellationToken token) =>
        {
            try
            {
                var invoiceeTask = repo.GetCustomerAsync(student.InvoiceeId, token);
                var studentEntity = mapper.Map<Student>(student);
                var invoicee = await invoiceeTask;
                if (invoicee is null) return Results.BadRequest();
                studentEntity.Invoicee = invoicee;
                return await repo.SaveStudentAsync(studentEntity, token) ? Results.Ok() : Results.BadRequest();
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        });
    }
}