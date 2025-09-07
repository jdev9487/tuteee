using JDev.RabbitMQ;
using JDev.Tuteee.DAL;
using JDev.Tuteee.Grpc.Api.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGrpc();
builder.Services.AddGrpcReflection();

var auth = new Auth();
builder.Configuration.GetSection("RabbitMQ").Bind(auth);

builder.Services.AddRabbitMqPublisher(auth);

builder.Services.AddDataAccess();

builder.Services.AddRazorTemplating();

var app = builder.Build();

app.MapGrpcService<InvoiceService>();
app.MapGrpcService<HomeworkService>();

if (app.Environment.IsDevelopment())
{
    app.MapGrpcReflectionService();
}

app.Run();