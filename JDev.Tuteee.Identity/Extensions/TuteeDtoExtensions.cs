namespace JDev.Tuteee.Identity.Extensions;

using Rest.ApiClient.DTOs;

public static class TuteeDtoExtensions
{
     private static bool IsSelfPaying(this TuteeDto tuteeDto) => tuteeDto.StakeholderId == tuteeDto.Client?.StakeholderId;
     public static string GetFirstName(this TuteeDto tuteeDto) => tuteeDto.IsSelfPaying() ? "SELF" : tuteeDto.FirstName;
     public static string GetLastName(this TuteeDto tuteeDto) => tuteeDto.IsSelfPaying() ? "" : tuteeDto.LastName;
     public static string ToActiveRateCurrency(this TuteeDto tuteeDto) => (tuteeDto.ActiveRate / 100).ToString("0.00");
}