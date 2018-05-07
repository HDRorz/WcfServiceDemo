using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using System.Text;
using System.Threading;

namespace WcfExtensions
{
    /// <summary>
    /// 力天使消息检查器
    /// </summary>
    public class DynamesMessageInspector : IDispatchMessageInspector
    {

        private int calls = 0;

        public object AfterReceiveRequest(ref Message request, IClientChannel channel, InstanceContext instanceContext)
        {
            Interlocked.Add(ref calls, 1);
            var buffer = request.CreateBufferedCopy(Int32.MaxValue);
            string temp = "";
            using (var ms = new MemoryStream())
            {
                buffer.WriteMessage(ms);
                ms.Position = 0;
                using (var sr = new StreamReader(ms))
                {
                    temp = sr.ReadToEnd();
                }
            }

            Console.Out.WriteLine(temp);

            return null;
        }

        public void BeforeSendReply(ref Message reply, object correlationState)
        {
            //var buffer = reply.CreateBufferedCopy(Int32.MaxValue);
            //string temp = "";
            //using (var ms = new MemoryStream())
            //{
            //    buffer.WriteMessage(ms);
            //    ms.Position = 0;
            //    using (var sr = new StreamReader(ms))
            //    {
            //        temp = sr.ReadToEnd();
            //    }
            //}

            //Console.Out.WriteLine(temp);
        }
    }
}
