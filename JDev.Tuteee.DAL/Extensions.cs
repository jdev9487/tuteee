namespace JDev.Tuteee.DAL;

using Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

public static class Extensions
{
    public static IServiceCollection AddDataAccess(this IServiceCollection services)
    {
        return services.AddDbContext<Context>(ServiceLifetime.Transient);
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
        var tomRogers =
            await context.Clients.FirstOrDefaultAsync(g => g.HolderFirstName == "Tom" && g.HolderLastName == "Rogers",
                cancellationToken: token);
        if (tomRogers is null)
        {
            tomRogers = new Client
            {
                HolderFirstName = "Tom",
                HolderLastName = "Rogers",
                EmailAddress = "tr@mail.com",
                PhoneNumber = "07123456789"
            };
            await context.Clients.AddAsync(tomRogers, token);
            var lucyRate = new Rate
            {
                PencePerHour = 4500,
                ActiveFrom = DateTimeOffset.MinValue
            };
            var lucyBassett = new Tutee
            {
                Client = tomRogers,
                FirstName = "Lucy",
                LastName = "Bassett",
                EmailAddress = "lb@mail.com",
                Rates = [lucyRate]
            };
            await context.Tutees.AddAsync(lucyBassett, token);
            var zaraRate = new Rate
            {
                PencePerHour = 5500,
                ActiveFrom = DateTimeOffset.MinValue
            };
            var zaraAhmed = new Tutee
            {
                Client = tomRogers,
                FirstName = "Zara",
                LastName = "Ahmed",
                EmailAddress = "za@mail.com",
                Rates = [zaraRate]
            };
            await context.Tutees.AddAsync(zaraAhmed, token);
            var lucyLesson1 = new Lesson
            {
                Tutee = lucyBassett,
                StartTime = new DateTimeOffset(2025, 1, 1, 9, 0, 0, TimeSpan.Zero),
                EndTime = new DateTimeOffset(2025, 1, 1, 10, 0, 0, TimeSpan.Zero),
                HomeworkInstructions = "Do questions in book!"
            };
            var lucyLesson2 = new Lesson
            {
                Tutee = lucyBassett,
                StartTime = new DateTimeOffset(2025, 1, 8, 9, 0, 0, TimeSpan.Zero),
                EndTime = new DateTimeOffset(2025, 1, 8, 10, 0, 0, TimeSpan.Zero),
                HomeworkInstructions = "Do questions from paper!"
            };
            await context.Lessons.AddRangeAsync(lucyLesson1, lucyLesson2);
            var tomInvoice = new Invoice
            {
                Client = tomRogers,
                Paid = false,
                Lessons = [lucyLesson1, lucyLesson2]
            };
            await context.Invoices.AddAsync(tomInvoice, token);
        }
        var xuexueXiang =
            await context.Clients.FirstOrDefaultAsync(g => g.HolderFirstName == "Xuexue" && g.HolderLastName == "Xiang",
                cancellationToken: token);
        if (xuexueXiang is null)
        {
            xuexueXiang = new Client
            {
                HolderFirstName = "Xuexue",
                HolderLastName = "Xiang",
                EmailAddress = "xx@mail.com",
                PhoneNumber = "07999999999"
            };
            await context.Clients.AddAsync(xuexueXiang, token);
            var yaraRate = new Rate
            {
                PencePerHour = 5000,
                ActiveFrom = DateTimeOffset.MinValue
            };
            var yaraGrant = new Tutee
            {
                Client = xuexueXiang,
                FirstName = "Yara",
                LastName = "Grant",
                EmailAddress = "yg@mail.com",
                Rates = [yaraRate]
            };
            await context.Tutees.AddAsync(yaraGrant, token);
            var johnRate = new Rate
            {
                PencePerHour = 1000,
                ActiveFrom = DateTimeOffset.MinValue
            };
            var johnGraham = new Tutee
            {
                Client = xuexueXiang,
                FirstName = "John",
                LastName = "Graham",
                EmailAddress = "jg@mail.com",
                Rates = [johnRate]
            };
            await context.Tutees.AddAsync(johnGraham, token);
            var yaraLesson1 = new Lesson
            {
                Tutee = yaraGrant,
                StartTime = new DateTimeOffset(2025, 1, 12, 17, 0, 0, TimeSpan.Zero),
                EndTime = new DateTimeOffset(2025, 1, 12, 18, 0, 0, TimeSpan.Zero)
            };
            var yaraLesson2 = new Lesson
            {
                Tutee = yaraGrant,
                StartTime = new DateTimeOffset(2025, 1, 19, 17, 0, 0, TimeSpan.Zero),
                EndTime = new DateTimeOffset(2025, 1, 19, 18, 0, 0, TimeSpan.Zero)
            };
            await context.Lessons.AddRangeAsync(yaraLesson1, yaraLesson2);
        }

        await context.SaveChangesAsync(token);
    }
}