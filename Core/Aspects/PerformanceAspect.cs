using System.Diagnostics;
using Castle.DynamicProxy;
using Core.Interceptors;
using Microsoft.Extensions.Logging;

namespace Core.Aspects;

public class PerformanceAspect : MethodInterceptorAttribute
{
    private readonly int _interval;
    private readonly Stopwatch _stopwatch;
    private readonly ILogger<PerformanceAspect> _logger;

    public PerformanceAspect(int interval = 100)
    {
        _interval = interval;
        _stopwatch = new Stopwatch();

        var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
        _logger = loggerFactory.CreateLogger<PerformanceAspect>();
    }

    protected override void OnBefore(IInvocation invocation)
    {
        _stopwatch.Start();
    }

    protected override void OnAfter(IInvocation invocation)
    {
        if (_stopwatch.ElapsedMilliseconds > _interval)
        {
            _logger.LogInformation($"Performance : {invocation.Method.DeclaringType?.FullName}.{invocation.Method.Name}() elapsed {_stopwatch.Elapsed.TotalSeconds} second(s).");
        }
        _stopwatch.Stop();
    }
}