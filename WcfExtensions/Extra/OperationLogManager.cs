using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web.Script.Serialization;
using WcfExtensions.Attr;

namespace WcfExtensions.Extra
{
    public class OperationLogManager
    {
        /// <summary>
        /// 记录日志
        /// </summary>
        /// <param name="operationName"></param>
        /// <param name="callTime"></param>
        /// <param name="cost"></param>
        /// <param name="contextID"></param>
        /// <param name="methodInfo"></param>
        /// <param name="inputs"></param>
        /// <param name="outputs"></param>
        /// <param name="retValue"></param>
        public static void Log(string operationName, DateTime callTime, long cost, string contextID, MethodInfo methodInfo, object[] inputs, object[] outputs, object retValue)
        {
            LogObject log = new LogObject()
            {
                OperationName = operationName,
                CallTime = callTime,
                Cost = cost,
                ContextID = contextID,
                RetValue = retValue,
            };
            log.CustomerNo = GetCustomerNo(methodInfo, inputs);
            log.Inputs = GetParamInfo(methodInfo.GetParameters().Where(pi => !pi.IsOut), inputs);
            log.Outputs = GetParamInfo(methodInfo.GetParameters().Where(pi => pi.IsOut), outputs);

            WriteLog(log);
        }

        /// <summary>
        /// 实际写入日志
        /// </summary>
        /// <param name="log"></param>
        public static void WriteLog(LogObject log)
        {
            Console.Out.WriteLine((new JavaScriptSerializer()).Serialize(log));
        }

        private static List<KeyValuePair<string, object>> GetParamInfo(IEnumerable<ParameterInfo> paramInfos, object[] objs)
        {
            List<KeyValuePair<string, object>> ret = new List<KeyValuePair<string, object>>();

            for (int i = 0; i < paramInfos.Count(); i ++)
            {
                if (paramInfos.ElementAt(i).ParameterType == typeof(string) 
                    && paramInfos.ElementAt(i).GetCustomAttributes(true).FirstOrDefault(attr => attr.GetType() == typeof(ParamTypeAttribute) && ((ParamTypeAttribute)attr).ParamType == EnumParamType.CustomerNo) != null)
                {
                    ret.Add(new KeyValuePair<string, object>(paramInfos.ElementAt(i).Name, ""));
                }
                else
                {
                    ret.Add(new KeyValuePair<string, object>(paramInfos.ElementAt(i).Name, objs[i]));
                }
            }

            return ret;
        }

        private static string GetCustomerNo(MethodInfo methodInfo, object[] objs)
        {
            var paramInfo = methodInfo.GetParameters().FirstOrDefault(pi => pi.GetCustomAttributes(true).FirstOrDefault(attr => attr.GetType() == typeof(ParamTypeAttribute) && ((ParamTypeAttribute)attr).ParamType == EnumParamType.CustomerNo) != null);

            if (paramInfo == null)
            {
                return "";
            }

            try
            {
                return (string)objs[paramInfo.Position];
            }
            catch
            {
                return "";
            }
        }


    }

    public class LogObject
    {
        public DateTime CallTime { get; set; }
        public long Cost { get; set; }
        public string OperationName { get; set; }
        public string CustomerNo { get; set; }
        public string ContextID { get; set; }
        public List<KeyValuePair<string, object>> Inputs { get; set; }
        public List<KeyValuePair<string, object>> Outputs { get; set; }
        public object RetValue { get; set; }

    }

}
