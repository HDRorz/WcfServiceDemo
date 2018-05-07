using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Diagnostics;
using System.ServiceModel.Dispatcher;
using System.Text;
using System.Threading.Tasks;
using WcfExtensions.Util;

namespace WcfExtensions
{
    /// <summary>
    /// 德天使方法扩展
    /// </summary>
    public class VirtueOperationBehavior : IOperationBehavior
    {
        public void AddBindingParameters(OperationDescription operationDescription, BindingParameterCollection bindingParameters)
        {

        }

        public void ApplyClientBehavior(OperationDescription operationDescription, ClientOperation clientOperation)
        {

        }

        /// <summary>
        /// 增加方法扩展
        /// </summary>
        /// <param name="operationDescription"></param>
        /// <param name="dispatchOperation"></param>
        public void ApplyDispatchBehavior(OperationDescription operationDescription, DispatchOperation dispatchOperation)
        {
            dispatchOperation.Invoker = new SyncNadleehOperationInvoker(operationDescription.SyncMethod);
        }

        public void Validate(OperationDescription operationDescription)
        {

        }
    }
}
