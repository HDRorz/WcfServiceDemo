using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using WcfServiceLibrary1;

namespace WcfServiceHost
{
    public class WinService
    {

        ServiceHost service = null;

        public void Start()
        {
            service = new ServiceHost(typeof(Service1));
            service.Open();
        }

        public void Stop()
        {
            service.Close();
            service = null;
        }
    }
}
