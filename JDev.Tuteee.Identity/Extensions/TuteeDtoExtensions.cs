namespace JDev.Tuteee.Identity.Extensions;

using Rest.ApiClient.DTOs;

public static class TuteeDtoExtensions
{
     public static bool IsSelfPaying(this TuteeDto tuteeDto) => tuteeDto.StakeholderId == tuteeDto.Client?.StakeholderId;
     public static string GetFirstName(this TuteeDto tuteeDto) => tuteeDto.IsSelfPaying() ? "SELF" : tuteeDto.FirstName;
     public static string GetLastName(this TuteeDto tuteeDto) => tuteeDto.IsSelfPaying() ? "" : tuteeDto.LastName;
     public static string ToActiveRateCurrency(this TuteeDto tuteeDto) => (tuteeDto.GetActiveRate() / 100).ToString("0.00");

     public static int GetActiveRate(this TuteeDto dto) => dto.Rates
         .Where(r => r.DateEnabled <= DateOnly.FromDateTime(DateTime.Today))
         .MaxBy(r => r.DateEnabled).PencePerHour;
}