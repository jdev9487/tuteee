using Microsoft.EntityFrameworkCore;
using TutorTracker.Api.Context;
using TutorTracker.Api.Controllers;
using TutorTracker.Api.Managers;
using TutorTracker.Api.Mapping;
using TutorTracker.Api.Parsing;
using TutorTracker.Api.Repositories;

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
var allowedOrigin = configuration.GetSection("AllowedOrigins").Get<string[]>();
services.AddDbContext<ApplicationContext>(options => options.UseNpgsql(connStr));
services.AddCors(opt =>
{
    opt.AddPolicy(name: "policy", policy =>
    {
        policy.WithOrigins(allowedOrigin!).AllowAnyMethod().AllowAnyHeader();
    });
});
services.AddControllers();

var app = builder.Build();
app.UseCors("policy");
app.UseRouting();
app.UseEndpoints(ep => ep.MapControllers());

app.Run();