using JDev.Tuteee.Api;
using JDev.Tuteee.Api.DB;
using JDev.Tuteee.Api.Extensions;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var authConfigSection = builder.Configuration.GetSection("Auth");
var auth = authConfigSection.Get<Auth>();
builder.Services.Configure<Auth>(authConfigSection);
builder.Services.AddJwtAuth(auth!.SymmetricSecurityKey);

builder.Services.AddDbContext<Context>(ServiceLifetime.Transient);

builder.Services.AddEndpoints();

var app = builder.Build();
var context = app.Services.GetRequiredService<Context>();
await context.Database.MigrateAsync();

if (app.Environment.IsDevelopment())
{
    await context.SeedDevelopmentDataAsync(default);
}

app.UseAuthentication();
app.UseAuthorization();
app.RegisterEndpoints();

await app.SeedAdminAsync(auth.AdminUsername, auth.AdminPassword);

app.Run();