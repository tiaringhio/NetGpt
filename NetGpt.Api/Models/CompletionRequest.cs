using System.Text.Json.Serialization;

namespace NetGpt.Api.Models;

public sealed class CompletionRequest
{
    private const string DefaultModel = "text-davinci-001";

    [JsonPropertyName("model")] public string Model { get; } = DefaultModel;
    [JsonPropertyName("prompt")] public string Prompt { get; set; } = string.Empty;
    [JsonPropertyName("temperature")] public int Temperature { get; } = 1;
    [JsonPropertyName("max_tokens")] public int MaxTokens { get; } = 100;
}