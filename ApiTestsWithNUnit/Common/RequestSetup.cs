using System.Text.Json;
using RestSharp;

namespace ApiTestsWithNUnit.Common;

public class Requests(string baseUrl)
{
    private readonly RestClient _client = new(baseUrl);

    public async Task<RestResponse<T>> GetAsync<T>(
        string endpoint,
        Dictionary<string, string>? headers = null) where T : notnull
    {
        var request = new RestRequest(endpoint, Method.Get);

        if (headers != null)
        {
            foreach (var header in headers)
                request.AddHeader(header.Key, header.Value);
        }

        return await _client.ExecuteAsync<T>(request);
    }
    
    public async Task<RestResponse<T>> PostAsync<T>(
        string endpoint,
        object? body = null,
        Dictionary<string, string>? headers = null) where T : notnull
    {
        var request = new RestRequest(endpoint, Method.Post);

        if (body != null)
            request.AddJsonBody(JsonSerializer.Serialize(body));

        if (headers != null)
        {
            foreach (var header in headers)
                request.AddHeader(header.Key, header.Value);
        }
        
        return await _client.ExecuteAsync<T>(request);
    }
}