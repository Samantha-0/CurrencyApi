using Common.Input;
using Common.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace CurrencyApi.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class CurrencyController : ControllerBase
{
    readonly ICurrencyApiService _currencyApiService;

    public CurrencyController(ICurrencyApiService currencyApiService)
    {
        _currencyApiService = currencyApiService;
    }

    [SwaggerOperation(Summary = " Retrieve all available Symbols.")]
    [HttpGet(Name = "symbols")]
    public List<CurrencySymbol> GetSymbols()
    {
        return _currencyApiService.GetSymbols();
    }

    [SwaggerOperation(Summary =
        "Calculate the exchange rate for a given amount from one currency to another currency.")]
    [HttpGet("calculate")]
    public decimal CalculateExchangeRate([FromQuery] string from, [FromQuery] string to, [FromQuery] DateTime date,
        [FromQuery] decimal amount)
    {
        return _currencyApiService.CalculateExchangeRate(new ExchangeRateInput
            { FromCurrency = from, ToCurrency = to, Date = date, Amount = amount });
    }
}
