using Common.Input;
using Common.Models;

namespace CurrencyDb;

/// <summary>
/// TODO: Class/project intended to access the database.
/// </summary>
public class CurrencyDbClient : ICurrencyDbClient
{
    public List<CurrencySymbol>? GetSymbols()
    {
        return null;
    }

    public void SaveSymbols(List<CurrencySymbol> symbolList)
    {
    }

    public CurrencyOnDate? GetLatest(ExchangeRateInput exchangeRateInput)
    {
        return null;
    }

    public void SaveLatest(List<CurrencyOnDate> latestCurrencies)
    {
    }
}

public interface ICurrencyDbClient
{
    List<CurrencySymbol>? GetSymbols();
    void SaveSymbols(List<CurrencySymbol> symbolList);
    CurrencyOnDate? GetLatest(ExchangeRateInput exchangeRateInput);
    void SaveLatest(List<CurrencyOnDate> latestCurrencies);
}