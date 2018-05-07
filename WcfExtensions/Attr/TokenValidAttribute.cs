using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.ServiceModel.Dispatcher;
using System.Text;
using System.Web.Script.Serialization;

namespace WcfExtensions.Attr
{
    [AttributeUsage(AttributeTargets.Method)]
    public class TokenValidAttribute : Attribute
    {
        private string parameterName = "token";

        private string failedRetJson = "\"Token验证失败，无权访问。\"";

        private object failedRetObj = null;

        /// <summary>
        /// 初始化验证token特性
        /// </summary>
        /// <param name="parameterName">参数名</param>
        /// <param name="failedRetJson">验证失败时返回信息</param>
        public TokenValidAttribute(string parameterName = "", string failedRetJson = "")
        {
            if (!string.IsNullOrWhiteSpace(parameterName))
            {
                this.parameterName = parameterName;
            }
            if (!string.IsNullOrWhiteSpace(failedRetJson))
            {
                this.failedRetJson = failedRetJson;
            }
        }

        public string ParameterName
        {
            get
            {
                return parameterName;
            }
        }

        /// <summary>
        /// 获取验证失败返回实体
        /// </summary>
        /// <param name="methodInfo"></param>
        /// <returns></returns>
        public object GetFailedRetObj(MethodInfo methodInfo)
        {
            if (string.IsNullOrWhiteSpace(failedRetJson))
            {
                return null;
            }

            if (failedRetObj != null)
            {
                return failedRetObj;
            }

            failedRetObj = (new JavaScriptSerializer()).Deserialize(failedRetJson, methodInfo.ReturnType);

            return failedRetObj;
        }

        /// <summary>
        /// 获取token字段在方法参数的下标
        /// </summary>
        /// <param name="methodInfo"></param>
        /// <returns></returns>
        public int GetTokenIndex(MethodInfo methodInfo)
        {
            var pi = methodInfo.GetParameters().FirstOrDefault(e => e.Name.Equals(parameterName, StringComparison.OrdinalIgnoreCase));
            if (pi == null)
            {
                return -1;
            }
            return pi.Position;
        }

        /// <summary>
        /// 从MethodInfo中获取验证token特性
        /// </summary>
        /// <param name="methodInfo"></param>
        /// <returns></returns>
        public static TokenValidAttribute GetTokenValidAttribute(MethodInfo methodInfo)
        {
            return (TokenValidAttribute)methodInfo.GetCustomAttributes(false).Where(e => e.GetType() == typeof(TokenValidAttribute)).FirstOrDefault();
        }

    }
}
