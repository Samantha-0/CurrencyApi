namespace Common.Input;

/// <summary>
/// Input class for the exchange rate execution.
/// </summary>
public class ExchangeRateInput
{
    public DateTime Date { get; set; }
    public string FromCurrency { get; set; }
    public string ToCurrency { get; set; }
    public decimal Amount { get; set; }
}