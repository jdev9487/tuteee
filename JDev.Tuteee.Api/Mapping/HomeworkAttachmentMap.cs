namespace JDev.Tuteee.Api.Mapping;

using ApiClient.DTOs;
using Entities;

public static class HomeworkAttachmentMap
{
    public static HomeworkAttachment Map(HomeworkAttachmentDto dto)
    {
        var ha = new HomeworkAttachment
        {
            FileName = dto.FileName,
        };
        if (dto.HomeworkId is not null) ha.HomeworkId = dto.HomeworkId.Value;
        return ha;
    }
}