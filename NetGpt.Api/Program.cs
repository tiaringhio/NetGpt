using System.Threading.RateLimiting;
using NetGpt.Api.Models;
using NetGpt.Api.Services.Completion;

var builder = WebApplication.CreateBuilder(args);


// Rate Limiting
builder.Services.AddRateLimiter(options =>
{
    options.RejectionStatusCode = 429;
    options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(httpContext =>
        RateLimitPartition.GetFixedWindowLimiter(
            partitionKey: httpContext.Request.Headers.Host.ToString(),
            factory: _ => new FixedWindowRateLimiterOptions
            {
                AutoReplenishment = true,
                PermitLimit = 1,
                QueueLimit = 0,
                Window = TimeSpan.FromMinutes(1)
            }));
    
    options.OnRejected = async (context, token) =>
    {
        context.HttpContext.Response.StatusCode = 429;
        if (context.Lease.TryGetMetadata(MetadataName.RetryAfter, out var retryAfter))
        {
            await context.HttpContext.Response.WriteAsync(
                $"Too many requests. Please try again after {retryAfter.TotalSeconds} second(s). ", cancellationToken: token);
        }
        else
        {
            await context.HttpContext.Response.WriteAsync(
                "Too many requests. Please try again later. ", cancellationToken: token);
        }
    };
});

var appSettingsSection = builder.Configuration.GetSection("AppSettings");

var appSettings = appSettingsSection.Get<AppSettings>();

builder.Services.AddTransient<ICompletionService, CompletionService>();

builder.Services.AddHttpClient("openApi", o =>
{
    o.BaseAddress = new Uri(appSettings!.OpenApiUrl);
    o.DefaultRequestHeaders.Add("authorization", $"Bearer {appSettings.OpenApiKey}");
});


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseRateLimiter();

app.Run();