namespace JDev.Tuteee.Api.Mapping;

using DTOs;
using Entities;

public static class GuardianMap
{
    public static GuardianDto Map(Guardian entity) =>
        new()
        {
            GuardianId = entity.GuardianId,
            FirstName = entity.FirstName,
            LastName = entity.LastName,
            EmailAddress = entity.EmailAddress,
            PhoneNumber = entity.PhoneNumber,
            TuteeIds = entity.Tutees.Select(t => t.TuteeId)
        };

    public static Guardian Map(GuardianDto dto) =>
        new()
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            EmailAddress = dto.EmailAddress,
            PhoneNumber = dto.PhoneNumber
        };
}