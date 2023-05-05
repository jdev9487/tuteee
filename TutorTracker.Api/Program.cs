using TutorTracker.Api.Mapping;
using TutorTracker.Api.Repositories;
using TutorTracker.Api.Routing;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IRepository, Repository>();
builder.Services.AddAutoMapper(typeof(ModelMapperProfile));

var app = builder.Build();
app.MapCustomerEndpoints();
app.MapStudentEndpoints();
app.MapLessonEndpoints();

app.Run();