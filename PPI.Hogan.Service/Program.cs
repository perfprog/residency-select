using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace PPI.Hogan.Service
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            Console.WriteLine("Starting Hogan Service");

            //Cliff: DEBUGGING
            Utility.Util u = new Utility.Util();
            u.LoadData(@"B:\CMSDATA\Clients\PPI\HoganTesting\Hogan_Datafeed_CliffTest_05112015.csv");
            //u.LoadData(@"B:\CMSDATA\Clients\PPI\Sites\RSOnline\Main\ppi.core.web\ppi.core.web\Uploadfiles\Hogan_Datafeed_CliffTest_05112015.csv");
            
           
            //ServiceBase[] ServicesToRun;
            //ServicesToRun = new ServiceBase[] 
            //{ 
            //    new Feed() 
            //};
            //ServiceBase.Run(ServicesToRun);
        }
    }
}
