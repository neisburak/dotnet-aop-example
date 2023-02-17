using Core.Aspects;
using Castle.DynamicProxy;
using Core.Dependencies;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Utilities.IoC;

public static class DependencyExtensions
{
    public static IServiceCollection AddInterceptedTransient<TInterface, TImplementation>(this IServiceCollection serviceCollection) where TInterface : class where TImplementation : class, TInterface
    {
        serviceCollection.AddTransient<TImplementation>();
        serviceCollection.AddTransient(typeof(TInterface), serviceProvider =>
        {
            var proxyGenerator = serviceProvider.GetRequiredService<IProxyGenerator>();
            var implementation = serviceProvider.GetRequiredService<TImplementation>();

            var interceptors = serviceProvider.GetInterceptors<TImplementation>();

            var options = new ProxyGenerationOptions() { Selector = new InterceptorSelector<TImplementation>() };

            return proxyGenerator.CreateInterfaceProxyWithTarget<TInterface>(implementation, options, interceptors);
        });
        return serviceCollection;
    }

    public static IServiceCollection AddInterceptedScoped<TInterface, TImplementation>(this IServiceCollection serviceCollection) where TInterface : class where TImplementation : class, TInterface
    {
        serviceCollection.AddScoped<TImplementation>();
        serviceCollection.AddScoped(typeof(TInterface), serviceProvider =>
        {
            var proxyGenerator = serviceProvider.GetRequiredService<IProxyGenerator>();
            var implementation = serviceProvider.GetRequiredService<TImplementation>();

            var interceptors = serviceProvider.GetInterceptors<TImplementation>();

            var options = new ProxyGenerationOptions() { Selector = new InterceptorSelector<TImplementation>() };

            return proxyGenerator.CreateInterfaceProxyWithTarget<TInterface>(implementation, options, interceptors);
        });
        return serviceCollection;
    }

    public static IServiceCollection AddInterceptedSingleton<TInterface, TImplementation>(this IServiceCollection serviceCollection) where TInterface : class where TImplementation : class, TInterface
    {
        serviceCollection.AddSingleton<TImplementation>();
        serviceCollection.AddSingleton(typeof(TInterface), serviceProvider =>
        {
            var proxyGenerator = serviceProvider.GetRequiredService<IProxyGenerator>();
            var implementation = serviceProvider.GetRequiredService<TImplementation>();

            var interceptors = serviceProvider.GetInterceptors<TImplementation>();

            var options = new ProxyGenerationOptions() { Selector = new InterceptorSelector<TImplementation>() };

            return proxyGenerator.CreateInterfaceProxyWithTarget<TInterface>(implementation, options, interceptors);
        });
        return serviceCollection;
    }

    private static IInterceptor[]? GetInterceptors<TImplementation>(this IServiceProvider serviceProvider) where TImplementation : class
    {
        var classAttributes = typeof(TImplementation).GetCustomAttributes(typeof(AttributeBase), true).Cast<AttributeBase>();
        var methodAttributes = typeof(TImplementation).GetMethods().SelectMany(s => s.GetCustomAttributes(typeof(AttributeBase), true).Cast<AttributeBase>());

        var attributes = classAttributes.Union(methodAttributes).OrderBy(o => o.Priority);

        var interceptors = attributes.Select(f => serviceProvider.GetRequiredService(typeof(InterceptorBase<>).MakeGenericType(f.GetType()))).Cast<IInterceptor>();

        return interceptors.ToArray();
    }
}