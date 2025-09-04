using JDev.Tuteee.DAL;
using JDev.Tuteee.Rest.Api;
using System.Text.Json.Serialization;
using JDev.Tuteee.Rest.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDataAccess();

builder.Services.AddOptions<AppSettings>()
    .BindConfiguration("AppSettings");

builder.Services.ConfigureHttpJsonOptions(opts =>
{
    opts.SerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
});

builder.Services.AddEndpoints();
builder.Services.AddAutoMapper(config =>
{
    config.AddMaps(typeof(ClientProfile));
});

var app = builder.Build();

await app.MigrateAsync();

app.RegisterEndpoints();

app.Run();