using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using WcfExtensions.Attr;

namespace WcfServiceLibrary1
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的接口名“IService1”。
    [ServiceContract]
    public interface IService1
    {

        [OperationContract]
        [WebGet(UriTemplate = "Data", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        FooObj GetData();

        [OperationContract]
        [WebGet(UriTemplate = "NullData", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        FooObj GetNullData();

        [OperationContract]
        [WebGet(UriTemplate = "Int", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        int GetInt();

        [OperationContract]
        [WebGet(UriTemplate = "String?cno={customerNo}&pwd={password}&token={token}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [TokenValid("token", "\"token校验失败\"")]
        string GetString(
            [ParamType(EnumParamType.CustomerNo)] string customerNo,
            [ParamType(EnumParamType.Password)] string password,
            [ParamType(EnumParamType.Token)] string token);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "Data", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        bool SetData(FooObj foo);

        [OperationContract]
        [WebGet(UriTemplate = "Add?a={a}&b={b}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        decimal Add(decimal a, decimal b);
    }


    // 使用下面示例中说明的数据约定将复合类型添加到服务操作。
    [DataContract]
    public class FooObj
    {
        [DataMember]
        public string FooStr { get; set; }

        [DataMember]
        public DateTime FooTime { get; set; }

        [OnSerializing]
        internal void OnSerializingMethod(StreamingContext context)
        {
            var propertys = this.GetType().GetProperties();

            foreach (var pi in propertys.Where(e => e.PropertyType == typeof(DateTime)))
            {
                DateTime obj = (DateTime)pi.GetValue(this, null);
                pi.SetValue(this, obj.ToUniversalTime(), null);
            }
        }
    }

}
