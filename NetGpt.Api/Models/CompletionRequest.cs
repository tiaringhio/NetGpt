using Newtonsoft.Json;

namespace NetGpt.Api.Models;

public sealed class CompletionRequest
{
    private const string DefaultModel = "text-davinci-001";

    [JsonProperty("model")] public string Model { get; } = DefaultModel;
    [JsonProperty("prompt")] public string Prompt { get; set; } = string.Empty;
    [JsonProperty("temperature")] public int Temperature { get; } = 1;
    [JsonProperty("max_tokens")] public int MaxTokens { get; } = 100;
}