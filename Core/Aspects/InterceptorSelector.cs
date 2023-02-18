using System.Reflection;
using Castle.DynamicProxy;
using Core.Dependencies;

namespace Core.Aspects;

public class InterceptorSelector<TImplementation> : IInterceptorSelector where TImplementation : class
{
    public IInterceptor[] SelectInterceptors(Type type, MethodInfo methodInfo, IInterceptor[] interceptors)
    {
        var classAttributes = type.GetTypeInfo().GetCustomAttributes(typeof(AttributeBase), true).Cast<AttributeBase>().ToList();

        var methodParameterTypes = methodInfo.GetParameters().Select(s => s.ParameterType).ToArray();
        var concreteMethod = typeof(TImplementation).GetMethod(methodInfo.Name, methodParameterTypes);
        if (concreteMethod is not null)
        {
            var methodAttributes = concreteMethod.GetCustomAttributes<AttributeBase>(true).Cast<AttributeBase>();
            classAttributes.AddRange(methodAttributes);
        }

        var interceptorList = new List<IInterceptor>();
        foreach (var item in classAttributes.OrderBy(o => o.Priority))
        {
            var baseType = typeof(InterceptorBase<>).MakeGenericType(item.GetType());

            var interceptor = interceptors.FirstOrDefault(a => a.GetType().IsAssignableTo(baseType));
            if (interceptor is not null) interceptorList.Add(interceptor);
        }
        return interceptorList.ToArray();
    }
}