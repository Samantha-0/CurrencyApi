using Common.Input;
using Common.Models;
using FixerApi;

namespace CurrencyApiTest;

public class MockFixerApiClient : IFixerApiClient
{
    public List<CurrencySymbol> GetSymbols()
    {
        return new List<CurrencySymbol>
        {
            new() { Code = "EUR", Name = "Euro" },
            new() { Code = "NOK", Name = "Norwegian Krone" }
        };
    }

    public List<CurrencyOnDate> GetLatest(ExchangeRateInput input)
    {
        return new List<CurrencyOnDate>
        {
            new()
            {
                FromCurrency = "EUR", ToCurrency = "NOK", Date = new DateTime(2022, 06, 12), Value = (decimal)10.208836
            }
        };
    }
}