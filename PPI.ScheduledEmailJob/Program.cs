using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPI.ScheduledEmailJob
{
    class Program
    {
        static void Main(string[] args)
        {
            EmailJob emailJob = new EmailJob();

            emailJob.RunSchedule();            
        }
    }
}
