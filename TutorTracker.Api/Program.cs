using Microsoft.EntityFrameworkCore;
using TutorTracker.Api.Context;
using TutorTracker.Api.Mapping;
using TutorTracker.Api.Parsing;
using TutorTracker.Api.Repositories;
using TutorTracker.Api.Routing;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddTransient<IRepository, Repository>();
builder.Services.AddSingleton<IDateParser, DateParser>();
builder.Services.AddAutoMapper(typeof(ModelMapperProfile));
var connStr = configuration.GetConnectionString("db");
builder.Services.AddDbContext<ApplicationContext>(options => options.UseNpgsql(connStr),
    ServiceLifetime.Transient, ServiceLifetime.Transient);

var app = builder.Build();
app.MapCustomerEndpoints();
app.MapStudentEndpoints();
app.MapLessonEndpoints();

app.Run();