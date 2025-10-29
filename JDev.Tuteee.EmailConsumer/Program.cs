using JDev.Core.RabbitMQ;
using JDev.Tuteee.EmailConsumer;
using JDev.Tuteee.EmailConsumer.Auth;
using JDev.Tuteee.EmailConsumer.Clients;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("appsettings.Secret.json", optional: true);

var auth = new Auth();
builder.Configuration.GetSection("RabbitMQ").Bind(auth);

builder.Services.AddRabbitMqConsumer<EmailHomeworkHandler>(auth);

builder.Services.AddOptions<Email>()
    .BindConfiguration("Email");

builder.Services.AddTransient<IEmailClient, EmailClient>();

var app = builder.Build();

app.Run();