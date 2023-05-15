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
services.AddAutoMapper(typeof(ModelMapperProfile));
var connStr = configuration.GetConnectionString("db");
var allowedOrigin = configuration.GetSection("AllowedOrigins").Value;
services.AddDbContext<ApplicationContext>(options => options.UseNpgsql(connStr), ServiceLifetime.Transient,
    ServiceLifetime.Transient);
services.AddCors(opt =>
{
    opt.AddPolicy(name: "policy", policy =>
    {
        policy.WithOrigins(allowedOrigin!).AllowAnyMethod().AllowAnyHeader();
    });
});

var app = builder.Build();
app.UseCors("policy");
app.MapCustomerEndpoints();
app.MapStudentEndpoints();
app.MapLessonEndpoints();

app.Run();