namespace NetGpt.Api.Services.Completion;

public interface ICompletionService
{
    Task<string> GetCompletion(string query);
}