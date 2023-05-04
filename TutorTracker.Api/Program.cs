using AutoMapper;
using TutorTracker.Api.Mapping;
using TutorTracker.Api.Repositories;
using M = TutorTracker.Model;
using E = TutorTracker.Api.Entities;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IRepository, Repository>();
builder.Services.AddAutoMapper(typeof(ModelMapperProfile));

var app = builder.Build();

var repo = app.Services.GetRequiredService<IRepository>();
var mapper = app.Services.GetRequiredService<IMapper>();

app.MapPost("/students",
    (M.Student student) =>
    {
        try
        {
            return repo.SaveStudent(mapper.Map<E.Student>(student)) ? Results.Ok() : Results.BadRequest();
        }
        catch (Exception ex)
        {
            var x = ex.Message;
            return Results.Problem(x);
        }
    });

app.MapGet("/lessons/{invoiceeId:int}", (int invoiceeId, string month) => "h");

app.Run();