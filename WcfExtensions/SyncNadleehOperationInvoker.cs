using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.ServiceModel.Diagnostics;
using System.ServiceModel.Dispatcher;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WcfExtensions.Util;

namespace WcfExtensions
{
    /// <summary>
    /// 娜德雷同步方法调用者
    /// 扩展软爹内部的SyncMethodInvoker
    /// 在方法调用前后插入自定义的操作
    /// </summary>
    public class SyncNadleehOperationInvoker : IOperationInvoker
    {

        private IOperationInvoker innerInvoker = null;

        //private InvokeDelegate invokeDelegate;
        private MethodInfo method;
        private string methodName;
        private int inputParameterCount;
        private int outputParameterCount;
        private Type type;

        public SyncNadleehOperationInvoker(MethodInfo method)
        {
            this.method = method;
            GetSyncMethodInvoker(method);

            inputParameterCount = method.GetParameters().Count(e => e.IsIn);
            outputParameterCount = method.GetParameters().Count(e => e.IsOut);

            BeforerInvokerHandles.Add(BeforeMethodInvoke.SetStopWatch);
            BeforerInvokerHandles.Add(BeforeMethodInvoke.TokenValid);

            AfterInvokerHandles.Add(AfterMethodInvoke.OperatingLog);
        }

        public SyncNadleehOperationInvoker(Type type, string methodName)
        {
            this.type = type;
            this.methodName = methodName;
            this.method = this.type.GetMethod(this.methodName);
            GetSyncMethodInvoker(type, methodName);

            inputParameterCount = method.GetParameters().Count(e => e.IsIn);
            outputParameterCount = method.GetParameters().Count(e => e.IsOut);

            BeforerInvokerHandles.Add(BeforeMethodInvoke.SetStopWatch);
            BeforerInvokerHandles.Add(BeforeMethodInvoke.TokenValid);

            AfterInvokerHandles.Add(AfterMethodInvoke.OperatingLog);
        }

        public bool IsSynchronous
        {
            get
            {
                return true;
            }
        }

        public MethodInfo Method
        {
            get
            {
                return this.method;
            }
        }
        
        public string MethodName
        {
            get
            {
                if (this.methodName == null)
                {
                    this.methodName = this.method.Name;
                }
                return this.methodName;
            }
        }

        /// <summary>
        /// 方法调用前的自定义操作列表
        /// </summary>
        private List<BeforeMethodInvokeDelegate> BeforerInvokerHandles = new List<BeforeMethodInvokeDelegate>();

        /// <summary>
        /// 方法调用后的自定义操作列表
        /// </summary>
        private List<AfterMethodInvokeDelegate> AfterInvokerHandles = new List<AfterMethodInvokeDelegate>();

        /// <summary>
        /// 获取软爹内部的SyncMethodInvoker
        /// </summary>
        /// <param name="method"></param>
        private void GetSyncMethodInvoker(MethodInfo method)
        {
            Type innerInvokerType = typeof(DispatchRuntime).Assembly.GetType("System.ServiceModel.Dispatcher.SyncMethodInvoker");
            var constructorMethod = innerInvokerType.GetConstructor(new Type[1] { typeof(MethodInfo) });
            innerInvoker = (IOperationInvoker)constructorMethod.Invoke(new object[1] { method });
        }

        /// <summary>
        /// 获取软爹内部的SyncMethodInvoker
        /// </summary>
        /// <param name="type"></param>
        /// <param name="methodName"></param>
        private void GetSyncMethodInvoker(Type type, string methodName)
        {
            //innerInvoker = (IOperationInvoker)AppDomain.CurrentDomain.CreateInstance("System.ServiceModel, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089", "System.ServiceModel.Dispatcher.SyncMethodInvoker", false, BindingFlags.CreateInstance | BindingFlags.OptionalParamBinding, null, new object[2] { type, methodName }, null, new object[0]);
            Type innerInvokerType = typeof(DispatchRuntime).Assembly.GetType("System.ServiceModel.Dispatcher.SyncMethodInvoker");
            var constructorMethod = innerInvokerType.GetConstructor(new Type[2] { typeof(Type), typeof(String) });
            innerInvoker = (IOperationInvoker)constructorMethod.Invoke(new object[2] { type, methodName });
        }

        public object[] AllocateInputs()
        {
            return innerInvoker.AllocateInputs();
        }

        /// <summary>
        /// 在方法调用前后插入自定义的操作
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="inputs"></param>
        /// <param name="outputs"></param>
        /// <returns></returns>
        public object Invoke(object instance, object[] inputs, out object[] outputs)
        {
            bool ifBreak = false;
            object ret = null;
            ConcurrentDictionary<string, object> invokeContext = new ConcurrentDictionary<string, object>();
            foreach (var beforeInvoke in BeforerInvokerHandles)
            {
                var temp = beforeInvoke.Invoke(invokeContext, method, instance, inputs, out ifBreak);
                if (ifBreak)
                {
                    ret = temp;
                    break;
                }
            }

            if (!ifBreak)
            {
                ret = innerInvoker.Invoke(instance, inputs, out outputs); ;
            }
            else
            {
                outputs = new object[outputParameterCount];
            }

            foreach (var afterInvoke in AfterInvokerHandles)
            {
                afterInvoke.Invoke(invokeContext, method, instance, inputs, outputs, ret);
            }

            return ret;
        }

        public IAsyncResult InvokeBegin(object instance, object[] inputs, AsyncCallback callback, object state)
        {
            throw new NotImplementedException();
        }

        public object InvokeEnd(object instance, out object[] outputs, IAsyncResult result)
        {
            throw new NotImplementedException();
        }
    }
}
