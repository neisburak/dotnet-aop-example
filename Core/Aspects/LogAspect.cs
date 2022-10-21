using Castle.DynamicProxy;
using Core.Interceptors;
using Microsoft.Extensions.Logging;

namespace Core.Aspects;

public class LogAspect : MethodInterceptorAttribute
{
    private readonly ILogger<LogAspect> _logger;

    public LogAspect()
    {
        var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
        _logger = loggerFactory.CreateLogger<LogAspect>();
    }


    protected override void OnBefore(IInvocation invocation)
    {
        _logger.LogInformation("OnBefore");
    }

    protected override void OnAfter(IInvocation invocation)
    {
        _logger.LogInformation("OnAfter");
    }
}