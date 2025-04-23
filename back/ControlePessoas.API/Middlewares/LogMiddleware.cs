namespace ControlePessoas.API.Middlewares;

public class LogMiddleware(RequestDelegate next, ILogger<LogMiddleware> logger)
{
    private readonly RequestDelegate _next = next;
    private readonly ILogger<LogMiddleware> _logger = logger;

    public async Task InvokeAsync(HttpContext context)
    {
        var request = context.Request;
        var startTime = DateTime.UtcNow;

        await _next(context);

        var endTime = DateTime.UtcNow;
        var response = context.Response;

        _logger.LogInformation
        (
            "HTTP {Method} {Path} responded {StatusCode} in {Duration}ms",
            request.Method,
            request.Path,
            response.StatusCode,
            (endTime - startTime).TotalMilliseconds
        );
    }
}
