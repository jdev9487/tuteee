namespace JDev.Tuteee.Api.Mapping;

using ApiClient.DTOs;
using Entities;

public static class HomeworkMap
{
    public static Homework Map(HomeworkDto dto)
    {
        var h = new Homework
        {
            Instructions = dto.Instructions,
            LessonId = dto.LessonId
        };
        if (dto.HomeworkAttachments is not null)
            h.HomeworkAttachments = dto.HomeworkAttachments.Select(HomeworkAttachmentMap.Map).ToList();
        return h;
    }
}