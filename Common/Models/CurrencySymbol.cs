namespace Common.Models;

/// <summary>
/// Symbol containing information about the Currency.
///
/// For example:
///   Code "NOK",
///   Name "Norwegian Krone"
/// </summary>
public class CurrencySymbol
{
    /// <summary>
    /// Code consisting of 3 characters
    /// </summary>
    public string Code { get; set; }

    /// <summary>
    /// Full name of currency
    /// </summary>
    public string Name { get; set; }
}