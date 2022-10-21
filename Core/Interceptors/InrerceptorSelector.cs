using System.Reflection;
using Castle.DynamicProxy;

namespace Core.Interceptors;

public class InterceptorSelector : IInterceptorSelector
{
    public IInterceptor[] SelectInterceptors(Type type, MethodInfo method, IInterceptor[] interceptors)
    {
        var classAttributes = type.GetCustomAttributes<MethodInterceptorAttribute>(true).ToList();
        var methodAttributes = type.GetMethod(method.Name, method.GetParameters().Select(s => s.ParameterType).ToArray())?.GetCustomAttributes<MethodInterceptorAttribute>(true).ToList();
        classAttributes.AddRange(methodAttributes!);
        return classAttributes.OrderBy(x => x.Priority).ToArray();
    }
}
