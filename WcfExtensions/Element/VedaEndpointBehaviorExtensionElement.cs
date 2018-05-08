using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Configuration;
using System.Text;

namespace WcfExtensions.Element
{
    public class VedaEndpointBehaviorExtensionElement : BehaviorExtensionElement
    {
        public override Type BehaviorType
        {
            get { return typeof(VedaEndpointBehavior); }
        }

        protected override object CreateBehavior()
        {
            return new VedaEndpointBehavior();
        }
    }
}
