using Common.Input;
using Common.Models;
using CurrencyDb;
using FixerApi;

namespace CurrencyApi;

public class CurrencyApiService : ICurrencyApiService
{
    readonly IFixerApiClient _fixerApiClient;
    readonly ICurrencyDbClient _currencyDbClient;

    public CurrencyApiService(IFixerApiClient fixerApiClient, ICurrencyDbClient currencyDbClient)
    {
        _fixerApiClient = fixerApiClient;
        _currencyDbClient = currencyDbClient;
    }

    public List<CurrencySymbol> GetSymbols()
    {
        var symbols = _currencyDbClient.GetSymbols();
        if (symbols != null && symbols.Any())
            return symbols;

        symbols = _fixerApiClient.GetSymbols();

        _currencyDbClient.SaveSymbols(symbols);

        return symbols;
    }

    public decimal CalculateExchangeRate(ExchangeRateInput input)
    {
        var latestCurrency = RetrieveLatestCurrency(input);

        if (latestCurrency == null)
            // TODO change in proper error handling and do not use exception for normal flow
            throw new Exception("Latest exchange rate could not be retrieved");

        var calculatedValue = input.Amount * latestCurrency.Value;
        return calculatedValue;
    }

    CurrencyOnDate? RetrieveLatestCurrency(ExchangeRateInput input)
    {
        var latestCurrency = _currencyDbClient.GetLatest(input);
        if (latestCurrency != null)
            return latestCurrency;

        var listOfLatestCurrencies = _fixerApiClient.GetLatest(input);

        _currencyDbClient.SaveLatest(listOfLatestCurrencies);

        latestCurrency = listOfLatestCurrencies.FirstOrDefault(e => e.ToCurrency == input.ToCurrency);
        return latestCurrency;
    }
}

public interface ICurrencyApiService
{
    public List<CurrencySymbol> GetSymbols();
    public decimal CalculateExchangeRate(ExchangeRateInput input);
}