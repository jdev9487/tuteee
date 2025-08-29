using JDev.Tuteee.Rest.ApiClient;
using JDev.Tuteee.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using JDev.Tuteee.Identity.Components;
using JDev.Tuteee.Identity.Components.Account;
using JDev.Tuteee.Identity.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.Options;

const string basePath = "tutoring";

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<AdminAuth>(builder.Configuration.GetSection("AdminAuth"));

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<IdentityRedirectManager>();

builder.Services.AddAuthentication(options =>
    {
        options.DefaultScheme = IdentityConstants.ApplicationScheme;
        options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
    })
    .AddIdentityCookies(config =>
    {
        config.ApplicationCookie =
            new OptionsBuilder<CookieAuthenticationOptions>(builder.Services, IdentityConstants.ApplicationScheme);
        config.ApplicationCookie.Configure(opt => opt.LoginPath = $"/{basePath}/{Routing.Login}");
    });

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ??
                       throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentityCore<ApplicationUser>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddSignInManager()
    .AddRoleManager<RoleManager<IdentityRole>>()
    .AddRoleStore<RoleStore<IdentityRole, ApplicationDbContext>>()
    .AddDefaultTokenProviders();

var apiUrl = builder.Configuration.GetSection("ApiUrl").Value;
if (apiUrl is null) throw new InvalidOperationException("API config missing");
builder.Services.AddApiClient(apiUrl);
builder.Services.AddBlazorBootstrap();

var app = builder.Build();

app.UsePathBase($"/{basePath}");
app.UseAuthorization();
app.UseAntiforgery();

await using var scope = app.Services.CreateAsyncScope();
var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
await context.Database.MigrateAsync();

await using var roleScope = app.Services.CreateAsyncScope();
var roleManager = roleScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
if (!await roleManager.RoleExistsAsync("Admin"))
    await roleManager.CreateAsync(new IdentityRole("Admin"));

await using var userScope = app.Services.CreateAsyncScope();
var adminAuth = userScope.ServiceProvider.GetRequiredService<IOptions<AdminAuth>>().Value;
var userManager = userScope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
if (await userManager.FindByEmailAsync(adminAuth.Username) is null)
{
    var user = new ApplicationUser { Email = adminAuth.Username, UserName = adminAuth.Username };
    await userManager.CreateAsync(user, adminAuth.Password);
    await userManager.AddToRoleAsync(user, "Admin");
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
}

app.UseStaticFiles();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.MapAdditionalIdentityEndpoints();

app.Run();