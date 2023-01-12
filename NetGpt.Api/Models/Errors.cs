namespace NetGpt.Api.Models;

public static class Errors
{
    public static Dictionary<string, string> Codes { get; }

    static Errors()
    {
        Codes = new Dictionary<string, string>
        {
            {"BadRequest", "Request was not formatted properly."},
            {"Unauthorized", "Authorization Error, maybe check your API Key?"},
            {"NotFound", "Request was made to the wrong address, how did that happen?"},
            {"TooManyRequests", "You've made too many requests."},
            {"InternalServerError", "Something went wrong on our end, please try again later."}
        };
    }
}