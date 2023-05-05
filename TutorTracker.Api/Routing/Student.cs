namespace TutorTracker.Api.Routing;

using M = Model;
using E = Entities;
using AutoMapper;
using Repositories;

internal static partial class WebApplicationExtensions
{
    internal static RouteHandlerBuilder MapStudentEndpoints(this WebApplication app)
    {
        var repo = app.Services.GetRequiredService<IRepository>();
        var mapper = app.Services.GetRequiredService<IMapper>();

        return app.MapPost("/students", (M.Student student) =>
        {
            try
            {
                var studentEntity = mapper.Map<E.Student>(student);
                var invoicee = repo.GetCustomer(student.InvoiceeId);
                studentEntity.Invoicee = invoicee;
                return repo.SaveStudent(studentEntity) ? Results.Ok() : Results.BadRequest();
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        });
    }
}