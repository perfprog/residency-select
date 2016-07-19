using System;
using System.IO;
//using System.Web;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Data.Entity;
using System.Net;
using System.Configuration;
using PPI.Core.Domain.Entities;
using PPI.Core;
using PPI.Core.Web.Models;
//using System.Web.Routing;
using PPI.Core.Web.Controllers;

namespace PPI.ScheduledEmailJob
{
    class EmailJob
    {
        public EmailJob()
        {

        }        

        [Log]
        public void RunSchedule()
        {            
            using (var UnitOfWork = new PPI.Core.Domain.Concrete.EfUnitOfWork())
            {
                Console.WriteLine("Looking for pending scheduled emails...");

                //check if any scheduled batches need to be processed
                //completed date and start must be null                
                IEnumerable<ScheduledEmail> nextSchedules = UnitOfWork.IScheduledEmailRepository.AsQueryable()
                    .Where(d => d.ScheduleDate <= DateTime.Now
                        && d.StartDate == null
                        && d.CompletedDate == null).OrderBy(d => d.ScheduleDate).ToList(); 
                
                if (nextSchedules.Count() < 1)
                {                    
                    return;     //nothing to do
                }                

                var GOEmail = new PPI.Core.Web.Infrastructure.EmailService();
                
                foreach (ScheduledEmail currentSchedule in nextSchedules)
                {                    
                    Console.WriteLine("Starting Schedule ID #{0}...", currentSchedule.Id);

                    //check again whether scheduled job has been started - perhaps by another instance of this program
                    ScheduledEmail currentScheduleVerify = 
                        UnitOfWork.IScheduledEmailRepository.SingleOrDefault(m => m.Id == currentSchedule.Id);   //.ToList();

                    if (currentScheduleVerify == null)
                    {
                        string errMsg = string.Format("Could not load Schedule ID #{0}...", currentSchedule.Id);                        
                        Console.WriteLine(errMsg);
                        ExceptionSvcHelper.HandleFatalException(new ApplicationException(errMsg));
                        return; 
                    }

                    if (currentScheduleVerify.StartDate != null)
                    {
                        //skip this scheduled job
                        Console.WriteLine("Skipping Schedule ID #{0} already in progress...", currentSchedule.Id);
                        return;
                    }                                        
                        
                    //use RS web site to process the scheduled batch
                    Console.WriteLine("Processing Schedule ID #{0}...", currentSchedule.Id);

                    string scheduleUri = ConfigurationManager.AppSettings["emailScheduleUri"];

                    using (var webClient = new WebClient())
                    {
                        NameValueCollection scheduleParams = new NameValueCollection();
                        scheduleParams.Add("scheduledEmailId", currentSchedule.Id.ToString());
                        scheduleParams.Add("userName", Properties.Settings.Default.EmailScheduleUser);
                        scheduleParams.Add("password", Properties.Settings.Default.EmailSchedulePwd);

                        byte[] responseBytes = webClient.UploadValues(scheduleUri, "POST", scheduleParams);                        

                        string responseText = Encoding.UTF8.GetString(responseBytes);
                        Console.WriteLine("Response: {0}", responseText);

                        if (responseText != string.Empty)
                        {
                            ExceptionSvcHelper.HandleNonFatalException(new ApplicationException(responseText));
                        }                        
                    }
                }                
            }
        }
    }
}
