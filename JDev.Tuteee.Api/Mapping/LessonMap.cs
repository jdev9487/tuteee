namespace JDev.Tuteee.Api.Mapping;

using ApiClient.DTOs;
using Entities;

public static class LessonMap
{
    public static Lesson Map(LessonDto dto)
    {
        var l = new Lesson
        {
            StartTime = dto.StartTime,
            EndTime = dto.EndTime,
            TuteeId = dto.TuteeId,
            HomeworkInstructions = dto.HomeworkInstructions
        };
        
        return l;
    }

    public static LessonDto Map(Lesson entity) =>
        new()
        {
            LessonId = entity.LessonId,
            StartTime = entity.StartTime,
            EndTime = entity.EndTime,
            TuteeId = entity.TuteeId,
            HomeworkInstructions = entity.HomeworkInstructions,
        };
}