using System.Net;
using System.Text;
using System.Text.Json;
using NetGpt.Api.Models;

namespace NetGpt.Api.Services.Completion;

public sealed class CompletionService : ICompletionService
{
    private readonly IHttpClientFactory _httpClientFactory;

    public CompletionService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }
    
    public async Task<CompletionResponse?> GetCompletion(string query)
    {
        var client = _httpClientFactory.CreateClient("openApi");

        var request = new CompletionRequest
        {
            Prompt = query
        };

        CompletionResponse? completionResponse = new();
        
        try
        {
            var serializedRequest = JsonSerializer.Serialize(request);
            var mycontent = new StringContent(serializedRequest, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("", mycontent);

            var responseString = await response.Content.ReadAsStringAsync();
            
            completionResponse = JsonSerializer.Deserialize<CompletionResponse>(responseString);

            completionResponse!.StatusCode = response.StatusCode;
            return completionResponse;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[Error] {nameof(GetCompletion)} - {ex.Message}");
            completionResponse!.StatusCode = HttpStatusCode.InternalServerError;
            return completionResponse;
        }
    }
}