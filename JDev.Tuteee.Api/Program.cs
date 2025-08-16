using JDev.Tuteee.Api.DB;
using JDev.Tuteee.Api.Extensions;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<Context>(ServiceLifetime.Transient);

builder.Services.AddEndpoints();

var app = builder.Build();
var context = app.Services.GetRequiredService<Context>();
await context.Database.MigrateAsync();

if (app.Environment.IsDevelopment())
{
    await context.SeedDevelopmentDataAsync(default);
}

app.RegisterEndpoints();

app.Run();