using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Messaging;
using System.ServiceModel;
using System.Text;
using System.Threading;
using WcfExtensions.Attr;
using WcfExtensions.Extra;

namespace WcfExtensions.Util
{
    public static class BeforeMethodInvoke
    {

        public static object SetStopWatch(ConcurrentDictionary<string, object> invokeContext, MethodInfo methodInfo, object target, object[] inputs, out bool ifBreakInvoke)
        {
            ifBreakInvoke = false;

            var sw = new Stopwatch();
            sw.Start();
            invokeContext["Stopwatch"] = sw;
            invokeContext["CallTime"] = DateTime.Now;

            return null;
        }

        /// <summary>
        /// rpc调用前验证token是否合法，不合法返回给定错误信息。
        /// </summary>
        /// <param name="methodInfo"></param>
        /// <param name="target"></param>
        /// <param name="inputs"></param>
        /// <param name="ifBreakInvoke"></param>
        /// <returns></returns>
        public static object TokenValid(ConcurrentDictionary<string, object> invokeContext, MethodInfo methodInfo, object target, object[] inputs, out bool ifBreakInvoke)
        {
            ifBreakInvoke = false;
            object ret = null;

            TokenValidAttribute tokenValidAttr = TokenValidAttribute.GetTokenValidAttribute(methodInfo);
            if (tokenValidAttr == null)
            {
                return ret;
            }

            int tokenIndex = tokenValidAttr.GetTokenIndex(methodInfo);
            if (tokenIndex < 0)
            {
                return ret;
            }

            string token = (string)inputs[tokenIndex];

            bool isValid = TokenValidManager.IsTokenValid(token);
            if (!isValid)
            {
                ret = tokenValidAttr.GetFailedRetObj(methodInfo);
                ifBreakInvoke = true;
            }

            return null;
        }
    }
}
