namespace JDev.Tuteee.Identity;

public static class Extensions
{
    public static string ToCurrency(this int pence) => (pence / 100).ToString("0.00");
}
