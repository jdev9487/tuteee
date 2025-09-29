using JDev.Core.RabbitMQ;
using JDev.Tuteee.EmailConsumer;
using JDev.Tuteee.EmailConsumer.Auth;
using JDev.Tuteee.EmailConsumer.Clients;
using MailKit.Security;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

var auth = new Auth
{
    Username = Environment.GetEnvironmentVariable("RABBIT_MQ_USERNAME") ?? "",
    Password = Environment.GetEnvironmentVariable("RABBIT_MQ_PASSWORD") ?? ""
};
builder.Configuration.GetSection("RabbitMQ").Bind(auth);

builder.Services.AddRabbitMqConsumer<EmailHomeworkHandler>(auth);

builder.Services.AddOptions<Email>()
    .Configure(x =>
    {
        x.Username = Environment.GetEnvironmentVariable("PROTONMAIL_USERNAME") ?? "";
        x.Password = Environment.GetEnvironmentVariable("PROTONMAIL_PASSWORD") ?? "";
    })
    .BindConfiguration("Email");

builder.Services.AddTransient<IEmailClient, EmailClient>();

var app = builder.Build();

app.Run();