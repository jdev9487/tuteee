namespace JDev.Tuteee.Api.Mapping;

using ApiClient.DTOs;
using Entities;

public static class TuteeMap
{
    public static TuteeDto Map(Tutee entity) =>
        new()
        {
            TuteeId = entity.TuteeId,
            ClientId = entity.ClientId,
            FirstName = entity.FirstName,
            LastName = entity.LastName,
            EmailAddress = entity.EmailAddress,
            Lessons = entity.Lessons.Select(LessonMap.Map),
            RateIds = entity.Rates.Select(l => l.RateId)
        };
    
    public static Tutee Map(TuteeDto dto) =>
        new()
        {
            ClientId = dto.ClientId,
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            EmailAddress = dto.EmailAddress,
        };
}