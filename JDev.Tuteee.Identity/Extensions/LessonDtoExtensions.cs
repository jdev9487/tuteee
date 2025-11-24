namespace JDev.Tuteee.Identity.Extensions;

using Rest.ApiClient.DTOs;

public static class LessonDtoExtensions
{
    public static TimeOnly GetEnd(this LessonDto dto) => dto.Start.Add(dto.Duration);
    public static decimal GetCost(this LessonDto dto) => (decimal)dto.CostAsDouble() / 100;
    public static string GetCostString(this LessonDto dto) => dto.GetCost().ToString("0.00");
    
    private static double CostAsDouble(this LessonDto dto)
    {
        if (dto.Tutee is null) return 0;
        var rates = dto.Tutee.Rates;
        var activeRate = rates
            .Where(r => r.DateEnabled <= dto.Date)
            .MaxBy(r => r.DateEnabled);
        return 
            activeRate.PencePerHour * dto.Duration.TotalHours;
    }
}