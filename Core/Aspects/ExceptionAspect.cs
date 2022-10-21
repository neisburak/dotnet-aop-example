using Castle.DynamicProxy;
using Core.Interceptors;
using Microsoft.Extensions.Logging;

namespace Core.Aspects;

public class ExceptionAspect : MethodInterceptorAttribute
{
    private readonly ILogger<ExceptionAspect> _logger;

    public ExceptionAspect()
    {
        var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
        _logger = loggerFactory.CreateLogger<ExceptionAspect>();
    }

    protected override void OnException(IInvocation invocation, Exception ex)
    {
        _logger.LogError(ex.Message);
    }
}