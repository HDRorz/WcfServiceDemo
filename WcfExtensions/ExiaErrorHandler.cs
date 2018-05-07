using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using System.Text;

namespace WcfExtensions
{
    /// <summary>
    /// 能天使异常处理器
    /// </summary>
    public class ExiaErrorHandler : IErrorHandler
    {
        public bool HandleError(Exception error)
        {

            return true;
        }

        public void ProvideFault(Exception error, MessageVersion version, ref Message fault)
        {

        }
    }
}
