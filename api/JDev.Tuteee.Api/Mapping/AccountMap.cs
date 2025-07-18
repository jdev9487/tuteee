namespace JDev.Tuteee.Api.Mapping;

using DTOs;
using Entities;

public static class AccountMap
{
    public static AccountDto Map(Account entity) =>
        new()
        {
            AccountId = entity.AccountId,
            HolderFirstName = entity.HolderFirstName,
            HolderLastName = entity.HolderLastName,
            EmailAddress = entity.EmailAddress,
            PhoneNumber = entity.PhoneNumber,
            Tutees = entity.Tutees.Select(TuteeMap.Map)
        };

    public static Account Map(AccountDto dto) =>
        new()
        {
            HolderFirstName = dto.HolderFirstName,
            HolderLastName = dto.HolderLastName,
            EmailAddress = dto.EmailAddress,
            PhoneNumber = dto.PhoneNumber
        };
}