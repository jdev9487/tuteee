using JDev.Tuteee.ApiClient;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using JDev.Tuteee.UI;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddApiClient(builder.Configuration.GetSection("ApiUrl").Value);

builder.Services.AddBlazorBootstrap();

await builder.Build().RunAsync();