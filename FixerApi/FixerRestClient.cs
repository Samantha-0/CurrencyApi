using Common.Config;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RestSharp;

namespace FixerApi;

public class FixerRestClient : IFixerRestClient
{
    readonly FixerApiOptions _fixerApiOptions;
    readonly ILogger<FixerRestClient> _logger;

    public FixerRestClient(ILogger<FixerRestClient> logger, IOptions<FixerApiOptions> fixerApiOptions)
    {
        _fixerApiOptions = fixerApiOptions.Value;
        _logger = logger;
    }

    public string? ExecuteRequest(string resource)
    {
        var client = new RestClient(_fixerApiOptions.BaseUrl);

        var request = new RestRequest(resource);
        request.AddHeader("apikey", _fixerApiOptions.ApiKey);

        var response = client.Execute(request);

        if (response.IsSuccessful)
            return response.Content;

        _logger.LogError(response.ErrorException, "Unsuccessful request to {Resource}", resource);
        throw new Exception($"Unsuccessful request to {resource}");
    }
}

public interface IFixerRestClient
{
    public string? ExecuteRequest(string resource);
}
