using Business.Abstract;
using Business.Concrete;
using Core.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;

namespace Business.Utilities.IoC;

public static class Configuration
{
    public static IServiceCollection ConfigureBusiness(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddInterceptedScoped<IPostService, PostManager>();
        serviceCollection.AddScoped<IInterceptedPostService, InterceptedPostManager>();

        return serviceCollection;
    }
}