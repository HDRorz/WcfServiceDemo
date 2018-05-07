using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WcfServiceLibrary1
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的类名“Service1”。
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.Single)]
    public class Service1 : IService1
    {
        public decimal Add(decimal a, decimal b)
        {
            return a + b;
        }

        public FooObj GetData()
        {
            var ret = new FooObj() { FooStr = "", FooTime = default(DateTime) };
            return ret;
        }

        public int GetInt()
        {
            return 2;
        }

        public FooObj GetNullData()
        {
            return null;
        }

        public string GetString(string customerNo, string password, string token)
        {
            string ret = "123";
            return ret;
        }

        public bool SetData(FooObj foo)
        {
            return true;
        }
    }
}
