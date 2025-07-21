namespace JDev.Tuteee.Api.Mapping;

using ApiClient.DTOs;
using Entities;

public static class ClientMap
{
    public static ClientDto Map(Client entity) =>
        new()
        {
            ClientId = entity.ClientId,
            HolderFirstName = entity.HolderFirstName,
            HolderLastName = entity.HolderLastName,
            EmailAddress = entity.EmailAddress,
            PhoneNumber = entity.PhoneNumber,
            Tutees = entity.Tutees.Select(TuteeMap.Map)
        };

    public static Client Map(ClientDto dto) =>
        new()
        {
            HolderFirstName = dto.HolderFirstName,
            HolderLastName = dto.HolderLastName,
            EmailAddress = dto.EmailAddress,
            PhoneNumber = dto.PhoneNumber
        };
}