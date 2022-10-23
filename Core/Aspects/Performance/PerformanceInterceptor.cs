using System.Diagnostics;
using Castle.DynamicProxy;
using Core.Dependencies;
using Microsoft.Extensions.Logging;

namespace Core.Aspects.Performance;

public class PerformanceInterceptor : InterceptorBase<PerformanceAttribute>
{
    private readonly ILogger<PerformanceInterceptor> _logger;
    private readonly Stopwatch _stopwatch;

    public PerformanceInterceptor(ILogger<PerformanceInterceptor> logger)
    {
        _logger = logger;
        _stopwatch = new Stopwatch();
    }

    protected override void OnBefore(IInvocation invocation, PerformanceAttribute attribute)
    {
        _stopwatch.Start();
    }

    protected override void OnAfter(IInvocation invocation, PerformanceAttribute attribute)
    {
        if (_stopwatch.Elapsed.TotalMilliseconds > attribute.Interval)
        {
            _logger.LogInformation($"{invocation.Method.DeclaringType?.FullName}.{invocation.Method.Name}() elapsed {_stopwatch.Elapsed.TotalSeconds} second(s).");
        }
        _stopwatch.Stop();
    }
}