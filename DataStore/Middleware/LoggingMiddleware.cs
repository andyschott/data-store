using System.Diagnostics;

namespace data_store;

public class LoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<LoggingMiddleware> _logger;

    public LoggingMiddleware(RequestDelegate next,
        ILogger<LoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        var stopWatch = Stopwatch.StartNew();
        try
        {
            await _next(context);
        }
        finally
        {
            stopWatch.Stop();

            _logger.LogInformation("{RequestMethod} {RequestPath} returned {StatusCode} in {Elapsed} ms",
                context.Request.Method,
                context.Request.Path,
                context.Response.StatusCode,
                stopWatch.ElapsedMilliseconds);
        }
    }
}
