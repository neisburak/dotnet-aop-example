using Castle.DynamicProxy;
using Core.Aspects.Logging;
using Core.Aspects.Performance;
using Core.Dependencies;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Utilities.IoC;

public static class Configuration
{
    public static IServiceCollection ConfigureCore(this IServiceCollection services)
    {
        services.AddSingleton<IProxyGenerator, ProxyGenerator>();

        services.AddTransient<InterceptorBase<PerformanceAttribute>, PerformanceInterceptor>();
        services.AddTransient<InterceptorBase<LogAttribute>, LogInterceptor>();

        return services;
    }
}