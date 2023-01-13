using NetGpt.Api.Models;

namespace NetGpt.Api.Services.Completion;

public interface ICompletionService
{
    Task<CompletionResponse?> GetCompletion(string query);
}