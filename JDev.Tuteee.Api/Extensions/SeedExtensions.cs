namespace JDev.Tuteee.Api.Extensions;

using DB;
using Entities;
using Microsoft.EntityFrameworkCore;

public static class SeedExtensions
{
    public static async Task SeedDevelopmentDataAsync(this Context context, CancellationToken token)
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
            var lucyBassett = new Tutee
            {
                Client = tomRogers,
                FirstName = "Lucy",
                LastName = "Bassett",
                EmailAddress = "lb@mail.com"
            };
            await context.Tutees.AddAsync(lucyBassett, token);
            var zaraAhmed = new Tutee
            {
                Client = tomRogers,
                FirstName = "Zara",
                LastName = "Ahmed",
                EmailAddress = "za@mail.com"
            };
            await context.Tutees.AddAsync(zaraAhmed, token);
            var lucyLesson1 = new Lesson
            {
                Tutee = lucyBassett,
                StartTime = new DateTimeOffset(2025, 1, 1, 9, 0, 0, TimeSpan.Zero),
                EndTime = new DateTimeOffset(2025, 1, 1, 10, 0, 0, TimeSpan.Zero)
            };
            var lucyLesson2 = new Lesson
            {
                Tutee = lucyBassett,
                StartTime = new DateTimeOffset(2025, 1, 8, 9, 0, 0, TimeSpan.Zero),
                EndTime = new DateTimeOffset(2025, 1, 8, 10, 0, 0, TimeSpan.Zero)
            };
            await context.Lessons.AddRangeAsync(lucyLesson1, lucyLesson2);
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
            var yaraGrant = new Tutee
            {
                Client = xuexueXiang,
                FirstName = "Yara",
                LastName = "Grant",
                EmailAddress = "yg@mail.com"
            };
            await context.Tutees.AddAsync(yaraGrant, token);
            var johnGraham = new Tutee
            {
                Client = xuexueXiang,
                FirstName = "John",
                LastName = "Graham",
                EmailAddress = "jg@mail.com"
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