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
services.AddTransient<IDateParser, DateParser>();
services.AddTransient<ICustomerManager, CustomerManager>();
services.AddTransient<ILessonManager, LessonManager>();
services.AddTransient<IStudentManager, StudentManager>();
services.AddTransient<CustomerController>();
services.AddTransient<LessonController>();
services.AddTransient<StudentController>();
builder.Services.AddAutoMapper(typeof(ModelMapperProfile));
var connStr = configuration.GetConnectionString("db");
builder.Services.AddDbContext<ApplicationContext>(options => options.UseNpgsql(connStr), ServiceLifetime.Transient,
    ServiceLifetime.Transient);

var app = builder.Build();
app.UseDefaultFiles();
app.UseStaticFiles();
app.MapCustomerEndpoints();
app.MapStudentEndpoints();
app.MapLessonEndpoints();

app.Run();