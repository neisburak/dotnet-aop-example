using System.Reflection;
using Castle.DynamicProxy;

namespace Core.Dependencies;

public abstract class InterceptorBase<TAttribute> : IInterceptor where TAttribute : AttributeBase
{
    protected virtual void OnBefore(IInvocation invocation, TAttribute attribute) { }

    protected virtual void OnAfter(IInvocation invocation, TAttribute attribute) { }

    protected virtual void OnException(IInvocation invocation, Exception ex, TAttribute attribute) { }

    protected virtual void OnSuccess(IInvocation invocation, TAttribute attribute) { }

    public void Intercept(IInvocation invocation)
    {
        var attribute = GetAttributes(invocation.MethodInvocationTarget, invocation.TargetType);
        if (attribute is null) invocation.Proceed();
        else
        {
            var isSuccess = true;
            OnBefore(invocation, attribute);
            try
            {
                invocation.Proceed();
            }
            catch (Exception ex)
            {
                isSuccess = false;
                OnException(invocation, ex, attribute);
                throw;
            }
            finally
            {
                if (isSuccess)
                {
                    OnSuccess(invocation, attribute);
                }
            }
            OnAfter(invocation, attribute);
        }
    }

    private TAttribute? GetAttributes(MethodInfo methodInfo, Type type)
    {
        var attribute = methodInfo.GetCustomAttribute<TAttribute>(true);
        if (attribute is not null) return attribute;

        return type.GetTypeInfo().GetCustomAttribute<TAttribute>(true);
    }
}