namespace JDev.Tuteee.Api.Mapping;

using DTOs;
using Entities;

public static class LessonMap
{
    public static Lesson Map(LessonDto dto) =>
        new()
        {
            StartTime = dto.StartTime,
            EndTime = dto.EndTime,
            TuteeId = dto.TuteeId,
        };
}