using System;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Proxies;

namespace DotNetBasicConsoleApp
{
    /// <summary>
    /// 远程调用服务提供类
    /// </summary>
    public class InvokeService
    {
        /// <summary>
        /// 获取一个服务的本地调用代理对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T Proxy<T>()
        {
            var proxy = new InvokeProxy<T>();
            return (T)proxy.GetTransparentProxy();
        }
    }
    /// <summary>
    /// 服务本地代理对象实现类
    /// </summary>
    public class InvokeProxy<T> : RealProxy
    {
        private Type type = null;
        public InvokeProxy() : this(typeof(T))
        {
            type = typeof(T);
        }
        protected InvokeProxy(Type classToProxy) : base(classToProxy)
        {
        }
        /// <summary>
        /// 接收本地调用请求，然后转发远程访问
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public override IMessage Invoke(IMessage msg)
        {
            Console.WriteLine("Invoke 远程服务调用！");
            var message = new ReturnMessage("Test DotNet Basic", null, 0, null, (IMethodCallMessage)msg);
            return (IMessage)message;
        }
    }
    public interface IUserService
    {
        string GetCurrentsUserName();
    }
}
