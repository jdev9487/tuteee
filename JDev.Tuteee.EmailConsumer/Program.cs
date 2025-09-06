using JDev.RabbitMQ;
using JDev.Tuteee.EmailConsumer;
using JDev.Tuteee.EmailConsumer.Auth;
using JDev.Tuteee.EmailConsumer.Clients;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRabbitMqConsumer<EmailHomeworkHandler>();

builder.Services.AddOptions<Email>()
    .BindConfiguration("Email");

builder.Services.AddTransient<IEmailClient, EmailClient>();

var app = builder.Build();

app.Run();