using Common.Input;
using Common.Models;
using Common.ResponseModel;
using Newtonsoft.Json;

namespace FixerApi;

public class FixerApiClient : IFixerApiClient
{
    readonly IFixerRestClient _fixerRestClient;

    public FixerApiClient(IFixerRestClient fixerRestClient)
    {
        _fixerRestClient = fixerRestClient;
    }

    public List<CurrencySymbol> GetSymbols()
    {
        var response = _fixerRestClient.ExecuteRequest("symbols");
        var symbolResponse = JsonConvert.DeserializeObject<SymbolResponse>(response);

        var symbolList = new List<CurrencySymbol>();
        foreach (var item in symbolResponse.Symbols)
            symbolList.Add(new CurrencySymbol
            {
                Code = item.Name,
                Name = (string)item.First
            });

        return symbolList;
    }

    public List<CurrencyOnDate> GetLatest(ExchangeRateInput input)
    {
        var response = _fixerRestClient.ExecuteRequest($"latest?symbols={input.ToCurrency}&base={input.FromCurrency}");
        var latestResponse = JsonConvert.DeserializeObject<LatestResponse>(response);

        var symbolList = new List<CurrencyOnDate>();
        foreach (var item in latestResponse.Rates)
            symbolList.Add(new CurrencyOnDate
                {
                    FromCurrency = item.Name,
                    ToCurrency = input.ToCurrency,
                    Date = input.Date,
                    Value = (decimal)item.First
                }
            );

        return symbolList;
    }
    
    // TODO - Part 5 - would be a new function here
}

public interface IFixerApiClient
{
    List<CurrencySymbol> GetSymbols();
    List<CurrencyOnDate> GetLatest(ExchangeRateInput input);
}
