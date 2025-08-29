using JDev.Tuteee.DAL;
using JDev.Tuteee.Grpc.Api;
using JDev.Tuteee.Grpc.Api.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGrpc();
builder.Services.AddGrpcReflection();

builder.Services.AddDataAccess();
builder.Services.AddTransient<IRepository, Repository>();

var app = builder.Build();

app.MapGrpcService<InvoiceCreatorService>();

if (app.Environment.IsDevelopment())
{
    app.MapGrpcReflectionService();
}

app.Run();