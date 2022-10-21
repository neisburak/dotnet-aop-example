using Castle.DynamicProxy;

namespace Core.Interceptors;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
public class MethodInterceptorAttribute : Attribute, IInterceptor
{
    public int Priority { get; set; }

    protected virtual void OnBefore(IInvocation invocation) { }

    protected virtual void OnAfter(IInvocation invocation) { }

    protected virtual void OnException(IInvocation invocation, Exception ex) { }

    protected virtual void OnSuccess(IInvocation invocation) { }

    public virtual void Intercept(IInvocation invocation)
    {
        var succeeded = true;
        OnBefore(invocation);
        try
        {
            invocation.Proceed();
        }
        catch (Exception ex)
        {
            succeeded = false;
            OnException(invocation, ex);
        }
        finally
        {
            if (succeeded)
            {
                OnSuccess(invocation);
            }
        }
        OnAfter(invocation);
    }
}