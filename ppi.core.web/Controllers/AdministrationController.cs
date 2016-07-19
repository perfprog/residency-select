using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.IO;

namespace PPI.Core.Web.Controllers
{
    using CsvHelper.Configuration;
    using PPI.Core.Domain.Abstract;
    using PPI.Core.Domain.Entities;
    using PPI.Core.Web.Models;
    using CsvHelper;

    [Authorize(Roles = "SiteCordinator,Admin,J3PAdmin")]
    public class AdministrationController : BaseController
    {
        public AdministrationController(IUnitOfWork unitOfWork)
            : base(unitOfWork) { }
        
        public ActionResult DashboardSendSwitch(int? SelectedProgramSiteEventId)
        {
            return RedirectToAction("Index", new { eventId = SelectedProgramSiteEventId });
            //Clear the Session information if you switch events           
        }
        //
        // GET: /Administration/
        [Authorize()]
        [Log]
        public ActionResult Index(int? eventId)
        {            
            var model = new DashboardViewModel();
            var Events = this.Events;            
            if (Events.Count() > 0)
            {

                var currentEvent = eventId.GetValueOrDefault(-1);
                if (this.CurrentEvent != -1 && !eventId.HasValue)
                {
                    currentEvent = this.CurrentEvent;
                }
                else
                    this.CurrentEvent = currentEvent;//set the cookie
                    
                
                model.EventName = Events.First(m => m.Value == currentEvent.ToString()).Text;
                ViewData["EventList"] = new SelectList(this.Events,"Value","Text", currentEvent);
                //Calculations
                var Dashboard = UnitOfWork.IvDashboardRepository.AsQueryable().Where(m => m.EventId == currentEvent);
                
                int TotalPeople = 0;
                int HPIComplete = 0;
                int HDSComplete = 0;
                int MVPIComplete = 0;
                int UsersCompleted = 0;
                int TodayCompleted = 0;
                int TotalInvitations = 0;
                int TotalReminders = 0;
                int TotalAssess = 0;
                DateTime ThisRun = DateTime.Now;
                DateTime LastUpdated = DateTime.Now;
                if (currentEvent != -1) //This is "Select Event"
                {
                    LastUpdated = UnitOfWork.IvDashboardRepository.AsQueryable().Where(m => m.EventId == currentEvent).Max(m => m.LastUpdated).HasValue ? UnitOfWork.IvDashboardRepository.AsQueryable().Where(m => m.EventId == currentEvent).Max(m => m.LastUpdated).Value : UnitOfWork.IEventRepository.AsQueryable().FirstOrDefault(m => m.Id == currentEvent).CreateDate;
                }                                
                
                if (Dashboard != null)
                {
                    TotalPeople = Dashboard.Count();                
                    TotalAssess = TotalPeople ;
                    foreach (var item in Dashboard)
                    {                                                
                            //have hogan data check dates
                        if (item.HPIDate != null && item.HPIDate != "")
                            {
                                HPIComplete++;                            
                                if (DateTime.Parse(item.HPIDate).ToShortDateString() == ThisRun.ToShortDateString())
                                    TodayCompleted++;
                            }
                        if (item.HDSDate != null && item.HDSDate != "")
                            {
                                HDSComplete++;
                                if (DateTime.Parse(item.HDSDate).ToShortDateString() == ThisRun.ToShortDateString())
                                    TodayCompleted++;
                            }
                        if (item.MVPIDate != null && item.MVPIDate != "")
                            {
                                MVPIComplete++;
                                if (DateTime.Parse(item.MVPIDate).ToShortDateString() == ThisRun.ToShortDateString())
                                    TodayCompleted++;
                            }
                        if (item.HPIDate != null && item.HPIDate != "" && item.HDSDate != null && item.HDSDate != "" && item.MVPIDate != null && item.MVPIDate != "")
                        { 
                            // this users has completed all three
                            UsersCompleted++;
                        }
                            //Mail
                            
                        
                        TotalInvitations = TotalInvitations + item.Person.PersonEmails.Count(m =>  (m.Email.EmailTypeId == 3 && m.Email.EventId == currentEvent));//TODO FIX THIS HARDCODE when fix Email pages
                        TotalReminders = TotalReminders + item.Person.PersonEmails.Count(m => m.Email.EmailTypeId == 4 && m.Email.EventId == currentEvent);//TODO FIX THIS HARDCODE when fix Email pages
                    }
                }
                // fill model
                int subtotal = UsersCompleted;
                int subnontotal = TotalAssess - (subtotal);
                double assessperc = 0;
                double nonassessperc = 0;
                if (TotalAssess > 0)
                {
                    assessperc = (double)subtotal / TotalAssess * 100;
                    nonassessperc = (double)subnontotal / TotalAssess * 100;
                }
                model.AssessmentComplete = new ValueRatio { TotalNumber = TotalAssess, NumberCompleted = subtotal, AsOfDate = LastUpdated, PercentComplete = assessperc };
                model.AssessmentNotComplete = new ValueRatio { TotalNumber = TotalAssess, NumberCompleted = TotalAssess - subtotal, AsOfDate = LastUpdated, PercentComplete = nonassessperc };
                model.AssessmentsToday = TodayCompleted;
                model.HPI = new ValueRatio { TotalNumber = TotalPeople, NumberCompleted = HPIComplete, AsOfDate = LastUpdated };
                model.HDS = new ValueRatio { TotalNumber = TotalPeople, NumberCompleted = HDSComplete, AsOfDate = LastUpdated };
                model.MVPI = new ValueRatio { TotalNumber = TotalPeople, NumberCompleted = MVPIComplete, AsOfDate = LastUpdated };
                model.InvitationsTotal = TotalInvitations;
                model.RemindersTotal = TotalReminders;
                model.eventId = currentEvent;
                model.AsOfDate = LastUpdated;
            }
            return View(model);
        }
        [Log]
        protected IEnumerable<vDashboard> GetAssessmentList(int eventId, string assessmentStatus)
        {
            IEnumerable<vDashboard> PeopleDashboard = new List<vDashboard>();
            switch(assessmentStatus)
            {
                case "Completed":
                    {
                        //var PeopleEvent = UnitOfWork.IPersonProgramSiteEventRepository.AsQueryable().Where(pse => pse.ProgramSiteEvent.EventId == eventId).Select(p => p.Person);

                        PeopleDashboard = UnitOfWork.IvDashboardRepository.AsQueryable()
                                .Where(d => d.EventId == eventId)
                                .Where(d => d.HDSDate != null && d.HDSDate != "")
                                .Where(d => d.HPIDate != null && d.HPIDate != "")
                                .Where(d => d.MVPIDate != null && d.MVPIDate != "");                                
                        break;
                    }

                case "UnCompleted":
                    {
                        var PeopleEvent = UnitOfWork.IPersonEventRepository.AsQueryable().Where(m => m.EventId == eventId).Select(p => p.Person);
                        var HoganData = UnitOfWork.IManual_Hogan_ImportRepository.AsQueryable();

                        PeopleDashboard = UnitOfWork.IvDashboardRepository.AsQueryable()
                            .Where(d => d.EventId == eventId)
                            .Where(d => (d.HPIDate == null || d.HPIDate == "") || (d.HDSDate == null || d.HDSDate == "") || (d.MVPIDate == null || d.MVPIDate == ""));                         
                        break;
                    
                    }
            
            }
            return PeopleDashboard;
        }
        [Log]
        public ActionResult AssessmentsCompleted(int eventId)
        {
            IEnumerable<Person> model = GetAssessmentList(eventId, "Completed").Select(m => m.Person);
            ViewData["EventId"] = eventId;
            return PartialView("AssessmentsCompleted",model);
        }
        [Log]
        public ActionResult AssessmentsUncompleted(int eventId)
        {
            IEnumerable<vDashboard> model = GetAssessmentList(eventId, "UnCompleted");
            ViewData["EventId"] = eventId;
            return PartialView("AssessmentsUncompleted", model);
        }
        public ActionResult Export(int eventId, string assessmentStatus)
        {
            string exportName = "people" + assessmentStatus + ".csv";
            CsvWriter writer;
            var streamoutput = new MemoryStream();
            var sw = new StreamWriter(streamoutput);
            switch (assessmentStatus)
            {
                case "Completed":
                    {
                        var PeopleList = GetAssessmentList(eventId, assessmentStatus).Select(m => m.Person);
                        if (PeopleList.Count() > 0)
                        {
                            var CsvConfig = new CsvConfiguration();
                            CsvConfig.RegisterClassMap<PPI.Core.Web.Infrastructure.ImportMaps.People_Map>();
                            writer = new CsvWriter(sw, CsvConfig);
                            IEnumerable<Person> records = PeopleList.ToList();
                            writer.WriteRecords(records);
                            sw.Flush();
                        }
                        streamoutput.Seek(0, SeekOrigin.Begin);
                        break;
                    }
                case "UnCompleted":
                    {
                        var PeopleList = GetAssessmentList(eventId, assessmentStatus);
                        if (PeopleList.Count() > 0)
                        {
                            var CsvConfig = new CsvConfiguration();
                            //CsvConfig.RegisterClassMap<PPI.Core.Web.Infrastructure.ImportMaps.People_Map>();
                            writer = new CsvWriter(sw, CsvConfig);
                            var records = PeopleList.Select(m => new { Title = m.Person.Title, FirstName = m.Person.FirstName,LastName = m.Person.LastName,AAMCNumber = m.Person.AAMCNumber,PrimaryEmail = m.Person.PrimaryEmail,Hogan_Id = m.Person.Hogan_Id,Gender = m.Person.Gender, HPIDate = m.HPIDate, HDSDate = m.HDSDate, MVPIDate = m.MVPIDate });
                            writer.WriteRecords(records);
                            sw.Flush();
                        }
                        streamoutput.Seek(0, SeekOrigin.Begin);
                        break;
                    }
            }
            return File(streamoutput, "text/csv", exportName);
        }
    }
}