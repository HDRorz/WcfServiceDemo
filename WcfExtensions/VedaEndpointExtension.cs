using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.Text;

namespace WcfExtensions
{

    /// <summary>
    /// 吠陀终结点扩展
    /// </summary>
    public class VedaEndpointExtension : IEndpointBehavior
    {

        public void AddBindingParameters(ServiceEndpoint endpoint, BindingParameterCollection bindingParameters)
        {

        }

        /// <summary>
        /// 只有在客户端时才会被调用
        /// </summary>
        /// <param name="endpoint"></param>
        /// <param name="clientRuntime"></param>
        public void ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime)
        {

        }

        /// <summary>
        /// 增加调度器扩展
        /// </summary>
        /// <param name="endpoint"></param>
        /// <param name="endpointDispatcher"></param>
        public void ApplyDispatchBehavior(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher)
        {
            endpointDispatcher.DispatchRuntime.ChannelDispatcher.ErrorHandlers.Add(new ExiaErrorHandler());

            endpointDispatcher.DispatchRuntime.MessageInspectors.Add(new DynamesMessageInspector());

            foreach (var operation in endpointDispatcher.DispatchRuntime.Operations)
            {
                //原为null。这里设置没有效果，因为实际调用时并没有使用这里的设置
                operation.Invoker = new SyncNadleehOperationInvoker(endpoint.Contract.ContractType, operation.Name);
                operation.ParameterInspectors.Add(new AstraeaParameterInspector());
            }

            //内部调度器
            var type = typeof(DispatchRuntime);
            var mi = type.GetMethod("GetRuntime", BindingFlags.NonPublic | BindingFlags.Instance);
            var innerRuntime = mi.Invoke(endpointDispatcher.DispatchRuntime, null);

            foreach (var operation in endpoint.Contract.Operations)
            {
                operation.Behaviors.Add(new VirtueOperationBehavior());
            }
        }

        public void Validate(ServiceEndpoint endpoint)
        {

        }
    }
}
