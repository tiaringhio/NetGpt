using System.Text;
using NetGpt.Api.Models;
using Newtonsoft.Json;

namespace NetGpt.Api.Services.Completion;

public sealed class CompletionService : ICompletionService
{
    private readonly IHttpClientFactory _httpClientFactory;

    public CompletionService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }
    
    public async Task<string> GetCompletion(string query)
    {
        var client = _httpClientFactory.CreateClient("openApi");

        var request = new CompletionRequest
        {
            Prompt = query
        };
        
        try
        {
            var serializedRequest = JsonConvert.SerializeObject(request);
            var mycontent = new StringContent(serializedRequest, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("", mycontent);

            if (!response.IsSuccessStatusCode)
            {
                return Errors.Codes[response.StatusCode.ToString()];
            }

            var responseString = await response.Content.ReadAsStringAsync();
            
            var completionResponse = JsonConvert.DeserializeObject<CompletionResponse>(responseString);

            return completionResponse!.Choices[0].Text;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[Error] {nameof(GetCompletion)} - {ex.Message}");
            return Errors.Codes["500"];
        }
    }
}