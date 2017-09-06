using System;
using System.Reflection;

namespace DotNetCoreConsoleApp
{
    public class InvokeService
    {
        public static T Proxy<T>()
        {
            return DispatchProxy.Create<T, InvokeProxy<T>>();
        }
    }
    public class InvokeProxy<T> : DispatchProxy
    {
        private Type type = null;
        public InvokeProxy()
        {
            type = typeof(T);
        }
        protected override object Invoke(MethodInfo targetMethod, object[] args)
        {
            Console.WriteLine("Invoke 远程服务调用！");
            return "Test DotNet Core";
        }
    }
    public interface IUserService
    {
        string GetCurrentsUserName();
    }
}
