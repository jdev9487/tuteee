namespace JDev.Tuteee.CustomTypes;

public class PhoneNumber
{
    private readonly string _digits = string.Empty;
    private const int MaxDigits = 10;

    public required string Raw
    {
        get => _digits;
        init => _digits = CleanseDigits(value);
    }

    private static string CleanseDigits(string digits)
    {
        var cleansedDigits = string.Concat(digits
            .Where(c => !char.IsWhiteSpace(c))
            .Where(char.IsNumber)
            .SkipWhile(c => c == '0'));
        
        if (cleansedDigits.Length != MaxDigits)
            throw new ArgumentException("Incorrect number of digits");

        return cleansedDigits;
    }

    public string ToDisplay() => $"+44 {Raw[..4]} {Raw[4..7]} {Raw[7..]}";
}