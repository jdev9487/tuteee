using JDev.Tuteee.Api.DB;
using JDev.Tuteee.Api.DTOs;
using JDev.Tuteee.Api.Endpoints;
using JDev.Tuteee.Api.Entities;
using JDev.Tuteee.Api.Mapping;
using Microsoft.AspNetCore.Http.HttpResults;
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

app.RegisterTuteeEndpoints();
app.RegisterGuardianEndpoints();

app.Run();