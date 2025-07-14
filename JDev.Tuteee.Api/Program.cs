using JDev.Tuteee.Api.DB;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<Context>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/tutees/{id:int}", (int id, Context context) =>
    {
        var tutee =  context.Tutees.Include(t => t.Guardian).FirstOrDefault();
        return "hello again";
    })
    .WithOpenApi();

app.Run();