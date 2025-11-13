namespace JDev.Tuteee.Identity;

using Rest.ApiClient.DTOs;

public static class Extensions
{
    public static string ToCurrency(this int pence) => (pence / 100).ToString("0.00");
    public static string GetFirstName(this TuteeDto tuteeDto) => tuteeDto.IsSelfPaying ? "SELF" : tuteeDto.FirstName;
    public static string GetLastName(this TuteeDto tuteeDto) => tuteeDto.IsSelfPaying ? "" : tuteeDto.LastName;
}
