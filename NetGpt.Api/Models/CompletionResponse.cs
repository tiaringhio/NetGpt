using Newtonsoft.Json;

namespace NetGpt.Api.Models;

public sealed class CompletionResponse
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("object")]
    public string Object { get; set; }

    [JsonProperty("created")]
    public int Created { get; set; }

    [JsonProperty("model")]
    public string Model { get; set; }

    [JsonProperty("choices")]
    public List<Choice> Choices { get; set; }

    [JsonProperty("usage")]
    public Usage Usage { get; set; }
}

public sealed class Choice
{
    [JsonProperty("text")]
    public string Text { get; set; }

    [JsonProperty("index")]
    public int Index { get; set; }

    [JsonProperty("logprobs")]
    public object Logprobs { get; set; }

    [JsonProperty("finish_reason")]
    public string FinishReason { get; set; }
}



public sealed class Usage
{
    [JsonProperty("prompt_tokens")]
    public int PromptTokens { get; set; }

    [JsonProperty("completion_tokens")]
    public int CompletionTokens { get; set; }

    [JsonProperty("total_tokens")]
    public int TotalTokens { get; set; }
}
