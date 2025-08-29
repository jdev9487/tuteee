using System.Text.Json.Serialization;
using JDev.Tuteee.Rest.Api.DB;
using JDev.Tuteee.Rest.Api.Entities;
using JDev.Tuteee.Rest.Api.Extensions;
using JDev.Tuteee.ApiClient.DTOs;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<Context>(ServiceLifetime.Transient);

builder.Services.ConfigureHttpJsonOptions(opts =>
{
    opts.SerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
});

builder.Services.AddEndpoints();
// builder.Services.AddGrpc();
builder.Services.AddAutoMapper(config =>
{
    config.CreateMap<Client, ClientDto>().ReverseMap();
    config.CreateMap<Invoice, InvoiceDto>().ReverseMap();
    config.CreateMap<Lesson, LessonDto>().ReverseMap();
    config.CreateMap<Rate, RateDto>().ReverseMap();
    config.CreateMap<Tutee, TuteeDto>().ReverseMap();
});

var app = builder.Build();
var context = app.Services.GetRequiredService<Context>();
await context.Database.MigrateAsync();

if (app.Environment.IsDevelopment())
{
    await context.SeedDevelopmentDataAsync(default);
}

app.RegisterEndpoints();
// app.UseGrpcWeb();
// app.MapGrpcService<InvoiceService>();

app.Run();