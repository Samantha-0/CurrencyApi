using Common.Input;
using Common.Models;
using CurrencyApi;
using CurrencyDb;
using Moq;

namespace CurrencyApiTest;

public class CurrencyApiServiceTests
{
    [Fact]
    public void GetSymbols_Simple_Successful()
    {
        var mockCurrencyDbClient = new Mock<ICurrencyDbClient>();
        mockCurrencyDbClient.Setup(rc => rc.GetSymbols()).Returns(new List<CurrencySymbol>());
        mockCurrencyDbClient.Setup(rc => rc.SaveSymbols(It.IsAny<List<CurrencySymbol>>()));

        var service = new CurrencyApiService(new MockFixerApiClient(), mockCurrencyDbClient.Object);

        var result = service.GetSymbols();
        Assert.Equal(2, result.Count);
    }

    [Theory]
    [InlineData(1, 10.208836)]
    [InlineData(2, 20.417672)]
    public void CalculateExchangeRate_Simple_Successful(decimal inputAmount, decimal expectedOutput)
    {
        var mockCurrencyDbClient = new Mock<ICurrencyDbClient>();
        mockCurrencyDbClient.Setup(rc => rc.GetLatest(It.IsAny<ExchangeRateInput>())).Returns((CurrencyOnDate?)null);
        mockCurrencyDbClient.Setup(rc => rc.SaveLatest(It.IsAny<List<CurrencyOnDate>>()));

        var service = new CurrencyApiService(new MockFixerApiClient(), mockCurrencyDbClient.Object);

        var result = service.CalculateExchangeRate(new ExchangeRateInput
            { Date = new DateTime(), FromCurrency = "EUR", ToCurrency = "NOK", Amount = inputAmount });
        Assert.Equal(expectedOutput, result);
    }
}