using JDev.RabbitMQ;
using JDev.Tuteee.DAL;
using JDev.Tuteee.Grpc.Api.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGrpc();
builder.Services.AddGrpcReflection();

builder.Services.AddRabbitMqPublisher();

builder.Services.AddDataAccess();

var app = builder.Build();

app.MapGrpcService<InvoiceService>();
app.MapGrpcService<HomeworkService>();

if (app.Environment.IsDevelopment())
{
    app.MapGrpcReflectionService();
}

app.Run();