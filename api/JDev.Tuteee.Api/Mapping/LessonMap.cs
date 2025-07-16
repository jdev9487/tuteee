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
    
    public static LessonDto Map(Lesson entity) =>
        new()
        {
            LessonId = entity.LessonId,
            StartTime = entity.StartTime,
            EndTime = entity.EndTime,
            TuteeId = entity.TuteeId
        };
}