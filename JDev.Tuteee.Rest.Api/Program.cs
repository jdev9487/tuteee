using System.Text.Json.Serialization;
using JDev.Tuteee.DAL;
using JDev.Tuteee.DAL.Entities;
using JDev.Tuteee.Rest.Api.Extensions;
using JDev.Tuteee.Rest.ApiClient.DTOs;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDataAccess();

builder.Services.ConfigureHttpJsonOptions(opts =>
{
    opts.SerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
});

builder.Services.AddEndpoints();
builder.Services.AddAutoMapper(config =>
{
    config.CreateMap<Client, ClientDto>().ReverseMap();
    config.CreateMap<Invoice, InvoiceDto>().ReverseMap();
    config.CreateMap<Lesson, LessonDto>().ReverseMap();
    config.CreateMap<Rate, RateDto>().ReverseMap();
    config.CreateMap<Tutee, TuteeDto>().ReverseMap();
});

var app = builder.Build();

await app.MigrateAsync();

app.RegisterEndpoints();

app.Run();