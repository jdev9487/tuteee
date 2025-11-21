using JDev.Core.RabbitMQ;
using JDev.Tuteee.EmailConsumer;
using JDev.Tuteee.EmailConsumer.Auth;
using JDev.Tuteee.EmailConsumer.Clients;
using JDev.Tuteee.Rest.ApiClient;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("appsettings.Secret.json", optional: true);

var auth = new Auth();
builder.Configuration.GetSection("RabbitMQ").Bind(auth);

builder.Services.AddRabbitMqConsumer<EmailHomeworkHandler>(auth);

builder.Services.AddRazorTemplating();

var restApiUrl = builder.Configuration.GetSection("RestApiUrl").Value;
if (restApiUrl is null) throw new InvalidOperationException("REST API config missing");
builder.Services.AddRestApiClient(restApiUrl);

builder.Services.AddOptions<Email>()
    .BindConfiguration("Email");

builder.Services.AddTransient<IEmailClient, EmailClient>();

var app = builder.Build();

app.Run();