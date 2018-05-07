using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Dispatcher;
using System.Text;

namespace WcfExtensions
{
    /// <summary>
    /// 正义天使参数检查器
    /// </summary>
    public class AstraeaParameterInspector : IParameterInspector
    {
        public void AfterCall(string operationName, object[] outputs, object returnValue, object correlationState)
        {
            return;
        }

        public object BeforeCall(string operationName, object[] inputs)
        {
            return null;
        }
    }
}
