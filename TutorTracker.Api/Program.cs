using Microsoft.EntityFrameworkCore;
using TutorTracker.Api.Context;
using TutorTracker.Api.Controllers;
using TutorTracker.Api.Managers;
using TutorTracker.Api.Mapping;
using TutorTracker.Api.Parsing;
using TutorTracker.Api.Repositories;
using TutorTracker.Api.Routing;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
var services = builder.Services;

services.AddTransient<IRepository, Repository>();
services.AddSingleton<IDateParser, DateParser>();
services.AddSingleton<ICustomerManager, CustomerManager>();
services.AddSingleton<ILessonManager, LessonManager>();
services.AddSingleton<IStudentManager, StudentManager>();
services.AddSingleton<CustomerController>();
services.AddSingleton<LessonController>();
services.AddSingleton<StudentController>();
builder.Services.AddAutoMapper(typeof(ModelMapperProfile));
var connStr = configuration.GetConnectionString("db");
builder.Services.AddDbContext<ApplicationContext>(options => options.UseNpgsql(connStr),
    ServiceLifetime.Transient, ServiceLifetime.Transient);

var app = builder.Build();
app.MapCustomerEndpoints();
app.MapStudentEndpoints();
app.MapLessonEndpoints();

app.Run();