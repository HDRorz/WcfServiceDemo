using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.ServiceModel;
using System.Text;
using System.Threading;
using WcfExtensions.Extra;

namespace WcfExtensions.Util
{
    public static class AfterMethodInvoke
    {

        public static void OperatingLog(ConcurrentDictionary<string, object> invokeContext, MethodInfo methodInfo, object target, object[] inputs, object[] outputs, object retValue)
        {
            Stopwatch sw = (Stopwatch)invokeContext["Stopwatch"];
            sw.Stop();
            DateTime CallTime = (DateTime)invokeContext["CallTime"];

            string operationName = target.GetType().Name + "." + methodInfo.Name;

            OperationLogManager.Log(operationName, CallTime, sw.ElapsedMilliseconds, OperationContext.Current.SessionId, methodInfo, inputs, outputs, retValue);
        }
    }
}
