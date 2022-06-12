using Common.Input;
using FixerApi;
using Moq;

namespace FixerApiTests;

public class FixerApiClientTests
{
    FixerApiClient MockFixerApiClient => GetClient();

    [Fact]
    public void GetSymbols()
    {
        var result = MockFixerApiClient.GetSymbols();
        Assert.NotNull(result);
        Assert.Equal(168, result.Count);
    }

    [Fact]
    public void GetLatest()
    {
        var result = MockFixerApiClient.GetLatest(new ExchangeRateInput
            { FromCurrency = "EUR", ToCurrency = "NOK", Date = new DateTime(2022, 06, 11) });
        Assert.NotNull(result);

        var output = result.FirstOrDefault(e => e.ToCurrency == "NOK");
        Assert.NotNull(output);
        Assert.Equal((decimal)10.208836, output?.Value);
    }

    #region Helpers

    static string LoadContentFromJson(string filename)
    {
        if (filename.Contains('?'))
            filename = filename.Substring(0, Math.Max(filename.IndexOf('?'), 0));

        var path = Path.Combine(Directory.GetCurrentDirectory(), "ResponsesFromApi", filename + ".json");
        return File.ReadAllText(path);
    }

    static FixerApiClient GetClient()
    {
        var mockRestClient = new Mock<IFixerRestClient>();
        mockRestClient.Setup(rc => rc.ExecuteRequest(It.IsAny<string>()))
            .Returns((string? resource) => LoadContentFromJson(resource));

        var client = new FixerApiClient(mockRestClient.Object);
        return client;
    }

    #endregion Helpers
}