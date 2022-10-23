using Castle.DynamicProxy;
using Core.Dependencies;
using Microsoft.Extensions.Logging;

namespace Core.Aspects.Logging;

public class LogInterceptor : InterceptorBase<LogAttribute>
{
    private readonly ILogger<LogInterceptor> _logger;

    public LogInterceptor(ILogger<LogInterceptor> logger)
    {
        _logger = logger;
    }

    protected override void OnBefore(IInvocation invocation, LogAttribute attribute)
    {
        _logger.LogInformation($"{invocation.Method.Name}_OnBefore");
    }

    protected override void OnAfter(IInvocation invocation, LogAttribute attribute)
    {
        _logger.LogInformation($"{invocation.Method.Name}_OnAfter");
    }
}