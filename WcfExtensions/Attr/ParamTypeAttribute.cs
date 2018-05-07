using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WcfExtensions.Attr
{
    [AttributeUsage(AttributeTargets.Parameter)]
    public class ParamTypeAttribute : Attribute
    {
        private EnumParamType paramType = EnumParamType.Default;

        public ParamTypeAttribute(EnumParamType paramType)
        {
            this.paramType = paramType;
        }

        public EnumParamType ParamType
        {
            get
            {
                return paramType;
            }
        }

    }

    public enum EnumParamType
    {
        Default = 0,
        CustomerNo = 1,
        Token = 2,
        Password = 3
    }

}
