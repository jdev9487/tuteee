namespace JDev.Tuteee.Rest.ApiClient;

using ApiClients;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Extensions.DependencyInjection;

public static class Extensions
{
    public static IServiceCollection AddRestApiClient(this IServiceCollection services, string uri)
    {
        services.AddScoped<JsonSerializerOptions>(_ => new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            ReferenceHandler = ReferenceHandler.Preserve
        });

        services.AddHttpClient<IClientRestApiClient, ClientRestApiClient>(ConfigureClient);
        services.AddHttpClient<IFileRestApiClient, FileRestApiClient>(ConfigureClient);
        services.AddHttpClient<IHomeworkAttachmentRestApiClient, HomeworkAttachmentRestApiClient>(ConfigureClient);
        services.AddHttpClient<IInvoiceRestApiClient, InvoiceRestApiClient>(ConfigureClient);
        services.AddHttpClient<ILessonRestApiClient, LessonRestApiClient>(ConfigureClient);
        services.AddHttpClient<ITuteeRestApiClient, TuteeRestApiClient>(ConfigureClient);
        
        return services;

        void ConfigureClient(HttpClient client) => client.BaseAddress = new Uri(uri);
    }
}