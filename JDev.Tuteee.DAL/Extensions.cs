namespace JDev.Tuteee.DAL;

using Core.EfCore.Repository;
using CustomTypes;
using Entities;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

public static class Extensions
{
    public static IServiceCollection AddDataAccess(this IServiceCollection services)
    {
        services.AddDbContext<Context>(ServiceLifetime.Transient);
        services.AddOptions<DbConfig>().BindConfiguration("DbConfig");
        services.AddTransient<IGenericRepository, GenericRepository<Context>>();
        return services;
    }

    public static async Task<WebApplication> MigrateAsync(this WebApplication app)
    {
        var context = app.Services.GetRequiredService<Context>();
        await context.Database.MigrateAsync();

        if (app.Environment.IsDevelopment())
        {
            await context.SeedDevelopmentDataAsync(default);
        }

        return app;
    }

    private static async Task SeedDevelopmentDataAsync(this Context context, CancellationToken token)
    {
        var referenceDateTime = DateTimeOffset.Now.AddMonths(-1);
        if (!context.Stakeholders.Where(tsh => tsh.FirstName == "Tom").Any(tsh => tsh.LastName == "Rogers"))
        {
            var tomStakeholder = new Stakeholder
            {
                FirstName = "Tom",
                LastName = "Rogers",
                EmailAddress = "tr@mail.com",
                PhoneNumber = new PhoneNumber { Raw = "7123456789" }
            };
            var tomClient = new ClientRole { Stakeholder = tomStakeholder };
            var lisaStakeholder = new Stakeholder
            {
                FirstName = "Lisa",
                LastName = "Simpson",
                EmailAddress = "ls@mail.com",
                PhoneNumber = new PhoneNumber { Raw = "7273899045" }
            };
            var lisaRate = new Rate { PencePerHour = 4500, ActiveFrom = DateTimeOffset.MinValue };
            var lisaUpdatedRate = new Rate { PencePerHour = 5000, ActiveFrom = referenceDateTime.AddDays(4)};
            var lisaTutee = new TuteeRole
            {
                Stakeholder = lisaStakeholder,
                Rates = [lisaRate, lisaUpdatedRate],
                ClientRole = tomClient
            };
            var lisaFirstLesson = new Lesson
            {
                StartTime = referenceDateTime,
                EndTime = referenceDateTime.AddHours(1),
                TuteeRole = lisaTutee
            };
            var lisaSecondLesson = new Lesson
            {
                StartTime = referenceDateTime.AddDays(7),
                EndTime = referenceDateTime.AddDays(7).AddHours(1),
                TuteeRole = lisaTutee
            };
            var tomInvoice = new Invoice
            {
                ClientRole = tomClient,
                Lessons = [lisaFirstLesson, lisaSecondLesson],
                Paid = false
            };
            await context.Invoices.AddAsync(tomInvoice, token);
            await context.Lessons.AddRangeAsync(lisaFirstLesson, lisaSecondLesson);
            await context.Stakeholders.AddRangeAsync(tomStakeholder, lisaStakeholder);
            await context.ClientRoles.AddAsync(tomClient, token);
            await context.TuteeRoles.AddAsync(lisaTutee, token);

            var benStakeholder = new Stakeholder
            {
                FirstName = "Ben",
                LastName = "Bennyson",
                EmailAddress = "bb@gun.com",
                PhoneNumber = new PhoneNumber { Raw = "7894677222" }
            };
            var benClient = new ClientRole { Stakeholder = benStakeholder };
            var benRate = new Rate { PencePerHour = 4500, ActiveFrom = DateTimeOffset.MinValue };
            var benUpdatedRate = new Rate { PencePerHour = 5000, ActiveFrom = referenceDateTime.AddDays(4)};
            var benUpdatedAgainRate = new Rate { PencePerHour = 6000, ActiveFrom = referenceDateTime.AddDays(10)};
            var benTutee = new TuteeRole
            {
                Stakeholder = benStakeholder,
                Rates = [benRate, benUpdatedRate, benUpdatedAgainRate],
                ClientRole = benClient
            };
            await context.Stakeholders.AddAsync(benStakeholder, token);
            await context.ClientRoles.AddAsync(benClient, token);
            await context.TuteeRoles.AddAsync(benTutee, token);
            
            await context.SaveChangesAsync(token);
        }
    }
}