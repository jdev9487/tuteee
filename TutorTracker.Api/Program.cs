using Microsoft.EntityFrameworkCore;
using TutorTracker.Api.Mapping;
using TutorTracker.Api.Parsing;
using TutorTracker.Api.Routing;
using TutorTracker.Persistence.Context;
using TutorTracker.Persistence.Repositories;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddTransient<IRepository, Repository>();
builder.Services.AddSingleton<IDateParser, DateParser>();
builder.Services.AddAutoMapper(typeof(ModelMapperProfile));
builder.Services.AddDbContext<Context>(options => options.UseNpgsql(configuration.GetConnectionString("")),
    ServiceLifetime.Transient, ServiceLifetime.Transient);

var app = builder.Build();
app.MapCustomerEndpoints();
app.MapStudentEndpoints();
app.MapLessonEndpoints();

app.Run();