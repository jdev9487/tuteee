namespace JDev.Tuteee.Api.Mapping;

using DTOs;
using Entities;

public static class TuteeMap
{
    public static TuteeDto Map(Tutee entity) =>
        new()
        {
            TuteeId = entity.TuteeId,
            GuardianId = entity.GuardianId,
            FirstName = entity.FirstName,
            LastName = entity.LastName,
            EmailAddress = entity.EmailAddress,
            LessonIds = entity.Lessons.Select(l => l.LessonId),
            RateIds = entity.Rates.Select(l => l.RateId)
        };
}