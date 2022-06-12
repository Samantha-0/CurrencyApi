namespace Common.Models;

/// <summary>
/// Object containing information about the currency value on a specific date.
///
/// Example:
///   FromCurrencyCode: EUR
///   ToCurrencyCode: NOK
///   Date: 2022-06-11
///   Value: 10.208836
/// </summary>
public class CurrencyOnDate
{
    /// <summary>
    /// From which currency the conversion value is - code consisting of 3 characters
    /// </summary>
    public string FromCurrency { get; set; }

    /// <summary>
    /// To which currency the conversion value is - code consisting of 3 characters
    /// </summary>
    public string ToCurrency { get; set; }

    /// <summary>
    /// Date of value retrieval
    /// </summary>
    public DateTime Date { get; set; }

    /// <summary>
    /// Conversion value in decimal
    /// </summary>
    public decimal Value { get; set; }
}