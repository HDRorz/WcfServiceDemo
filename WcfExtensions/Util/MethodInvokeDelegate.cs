using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading;

namespace WcfExtensions.Util
{
    public delegate object BeforeMethodInvokeDelegate(ConcurrentDictionary<string, object> invokeContext, MethodInfo methodInfo, object target, object[] inputs, out bool ifBreakInvoke);

    public delegate void AfterMethodInvokeDelegate(ConcurrentDictionary<string, object> invokeContext, MethodInfo methodInfo, object target, object[] inputs, object[] outputs, object retValue);

}
