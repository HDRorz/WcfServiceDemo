using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WcfService1
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的接口名“IService1”。
    [ServiceContract]
    public interface IService1
    {

        [OperationContract]
        //[WebGet(UriTemplate = "/Data", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [WebGet(RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        FooObj GetData();

        [OperationContract]
        [WebGet(UriTemplate = "/NullData", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        FooObj GetNullData();

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/Data?foo={foo}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        bool SetData(FooObj foo);
    }


    // 使用下面示例中说明的数据约定将复合类型添加到服务操作。
    [DataContract]
    public class FooObj
    {
        [DataMember]
        public string FooStr { get; set; }

        [DataMember]
        public DateTime FooTime { get; set; }
    }
}
