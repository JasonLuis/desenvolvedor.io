using Serilog;
using System.Diagnostics;

namespace PorBaixoDosPanos;

public class TemplateMiddleware
{
    private readonly RequestDelegate _next;
    public TemplateMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        // Faz algo antes

        // Chama o próximo middleware no pipelane.
        await _next(httpContext);

        // Faz algo depois
    }
}

public class LogTempoMiddleware
{
    private readonly RequestDelegate _next;
    public LogTempoMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        // Faz algo antes
        var sw = Stopwatch.StartNew();

        // Chama o próximo middleware no pipelane.
        await _next(httpContext);

        sw.Stop();

        Log.Logger = new LoggerConfiguration().WriteTo.Console().CreateLogger();
        Log.Information($"A execução demorou {sw.Elapsed.TotalMilliseconds}ms ({sw.Elapsed.TotalSeconds} segundos)");

        // Faz algo depois
    }
}

public static class SerilogExtensions
{
    public static void AddSerilog(this WebApplicationBuilder builder)
    {
        builder.Host.UseSerilog();
        //
    }
}

public static class LogTempoMiddlewareExtensions
{
    public static void UseLogTempo(this WebApplication app)
    {
        app.UseMiddleware<LogTempoMiddleware>();
    }
}

