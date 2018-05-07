using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Topshelf;

namespace WcfServiceHost
{
    class Program
    {
        static void Main(string[] args)
        {
            var rc = HostFactory.Run(x =>
            {
                x.Service<WinService>(s =>
                {
                    s.ConstructUsing(name => new WinService());
                    s.WhenStarted(tc => tc.Start());
                    s.WhenStopped(tc => tc.Stop());
                });
                x.RunAsLocalSystem();

                x.SetDescription("A Wcf WinService Host");
                x.SetDisplayName("A Wcf Topshelf Host");
                x.SetServiceName("WcfServiceHost");
            });

            var exitCode = (int)Convert.ChangeType(rc, rc.GetTypeCode());
            Environment.ExitCode = exitCode;
        }
    }
}
