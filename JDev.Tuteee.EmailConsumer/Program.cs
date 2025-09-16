using JDev.Core.RabbitMQ;
using JDev.Tuteee.EmailConsumer;
using JDev.Tuteee.EmailConsumer.Auth;
using JDev.Tuteee.EmailConsumer.Clients;

var builder = WebApplication.CreateBuilder(args);

var auth = new Auth();
builder.Configuration.GetSection("RabbitMQ").Bind(auth);

builder.Services.AddRabbitMqConsumer<EmailHomeworkHandler>(auth);

builder.Services.AddOptions<Email>()
    .BindConfiguration("Email");

if (builder.Environment.IsDevelopment())
    builder.Services.AddTransient<IEmailClient, FakeClient>();
else
    builder.Services.AddTransient<IEmailClient, EmailClient>();

var app = builder.Build();

app.Run();