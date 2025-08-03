using JDev.Tuteee.Api.DB;
using JDev.Tuteee.Api.Entities;
using JDev.Tuteee.Api.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(o =>
    {
        o.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "yourdomain.com",
            ValidAudience = "yourdomain.com",
            IssuerSigningKey =
                new SymmetricSecurityKey("your_super_secret_key_that_cannot_be_discovered_ever"u8.ToArray())
        };
    });
builder.Services.AddAuthorization();
builder.Services.AddIdentityCore<User>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<IdentityContext>();
builder.Services.AddDbContext<Context>(ServiceLifetime.Transient);
builder.Services.AddDbContext<IdentityContext>(ServiceLifetime.Transient);
builder.Services.AddEndpoints();

var app = builder.Build();

app.UseAuthentication();

// app.MapGet("/", [Authorize] () => "Hello World!");

app.UseAuthentication();
app.UseAuthorization();

app.RegisterEndpoints();

app.Run();

internal class UserLogin
{
    public string Username { get; init; } = default!;
    public string Password { get; init; } = default!;
}
// using JDev.Tuteee.Api.DB;
// using JDev.Tuteee.Api.Entities;
// using JDev.Tuteee.Api.Extensions;
// using Microsoft.AspNetCore.Identity;
// using Microsoft.EntityFrameworkCore;
//
// var builder = WebApplication.CreateBuilder(args);
//
// builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen();
//
// builder.Services.AddAuthentication().AddCookie(IdentityConstants.ApplicationScheme);
// builder.Services.AddAuthorizationBuilder();
//
// // builder.Services.AddIdentityCore<User>()
// //     .AddRoles<IdentityRole>()
// //     .AddEntityFrameworkStores<IdentityContext>()
// //     .AddApiEndpoints();
// //
// // builder.Services.AddCors();
// //
// // builder.Services.AddDbContext<Context>(ServiceLifetime.Transient);
// // builder.Services.AddDbContext<IdentityContext>(ServiceLifetime.Transient);
//
// builder.Services.AddEndpoints();
//
// var app = builder.Build();
//
// // var context = app.Services.GetRequiredService<Context>();
// // var idContext = app.Services.GetRequiredService<IdentityContext>();
// // await context.Database.MigrateAsync();
// // await idContext.Database.MigrateAsync();
//
// if (app.Environment.IsDevelopment())
// {
//     // await context.SeedDevelopmentDataAsync(default);
//     app.UseSwagger();
//     app.UseSwaggerUI();
// }
//
// // app.UseCors(cpb =>
// // {
// //     cpb.AllowAnyOrigin();
// //     cpb.AllowAnyMethod();
// //     cpb.AllowAnyHeader();
// // });
//
// app.UseAuthentication();
// app.UseAuthorization();
//
// app.MapGet("hello", () => "sup").RequireAuthorization();
//
// // app.MapIdentityApi<User>();
// // app.RegisterEndpoints();
//
// // await using var roleScope = app.Services.CreateAsyncScope();
// // var roleManager = roleScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
// // if (!await roleManager.RoleExistsAsync("Admin"))
// //     await roleManager.CreateAsync(new IdentityRole("Admin"));
// //
// // await using var userScope = app.Services.CreateAsyncScope();
// // var userManager = roleScope.ServiceProvider.GetRequiredService<UserManager<User>>();
// // if (await userManager.FindByEmailAsync("admin@admin.com") is null)
// // {
// //     var user = new User { Email = "admin@admin.com", UserName = "admin@admin.com" };
// //     await userManager.CreateAsync(user, "SuperSafe1!");
// //     await userManager.AddToRoleAsync(user, "Admin");
// // }
//
// app.Run();