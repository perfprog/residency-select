using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Data.Entity;
using System.IO;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace PPI.Core.Web.Controllers
{
    using PPI.Core.Domain.Entities;
    using PPI.Core.Domain.Abstract;
    using PPI.Core.Web.Models;
    using CsvHelper;
    using CsvHelper.Configuration;
    [Authorize(Roles = "Admin")]
    public class BillingController : BaseController
    {
        
        public BillingController(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        { 
        
        }
        // GET: Billing
        
        public ActionResult Index()
        {
            var model = new BillingViewModel();
            model.Events = new List<Event>();
            model.PracticeReports = new List<PersonPracticeReport>();
            return View(model);
        }
        [HttpPost]
        public ActionResult Review(string datepickerDateRange)
        {
            var model = new BillingViewModel();
            var startDate = DateTime.Parse(datepickerDateRange.Substring(0, datepickerDateRange.IndexOf("-") - 1));
            var endDate = DateTime.Parse(datepickerDateRange.Substring(datepickerDateRange.IndexOf("-") + 1));
            model.Events = UnitOfWork.IEventRepository.AsQueryable().Where(m => DbFunctions.TruncateTime(m.CreateDate) >= startDate && DbFunctions.TruncateTime(m.CreateDate) <= endDate && m.Billable == true).ToList();
            model.PracticeReports = UnitOfWork.IPersonPracticeReportRepository.AsQueryable()
                .Where(m => DbFunctions.TruncateTime(m.RunDate) >= startDate && DbFunctions.TruncateTime(m.RunDate) <= endDate)
                .Where(m => m.Event.EventTypeId != 1) // ignore match events match
                .OrderBy(m => m.PracticeReportId).ToList();
           
            //var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));            
            return View("Index",model);
        }       
        public ActionResult Participants(int eventId)
        {
            var model = UnitOfWork.IPersonEventRepository.AsQueryable().Where(m => m.EventId == eventId).Select(m => m.Person);
            ViewBag.EventId = eventId;
            return PartialView("ParticpantHogan",model);
        }
        public ActionResult Reports(int eventId)
        {
            var model = new BillingReportViewModel();
            ViewBag.EventId = eventId;
            var personreports = UnitOfWork.IPersonPracticeReportRepository.AsQueryable().Where(m => m.EventId == eventId)
                .GroupBy(m => m.PracticeReportId)
                //.Select(n => new {reportId = n.Key, report = n.Select(y => y.PracticeReport), people = n.Select(x => x.Person)});
                .Select(n => new ReportsRun() { Report = n.Select(m => m.PracticeReport).FirstOrDefault(), Participants = n.Select(m => m.Person).Distinct() });
            model.ReportsSelected = personreports;
            return PartialView("Reports", model);
        }
        public ActionResult ExportParticipants(int eventId)
        {
            string exportName = "Participants" + ".csv";
            CsvWriter writer;            
            var streamoutput = new MemoryStream();
            var sw = new StreamWriter(streamoutput);
            var PeopleList = UnitOfWork.IPersonEventRepository.AsQueryable().Where(m => m.EventId == eventId).Select(m => m.Person);
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
            return File(streamoutput, "text/csv", exportName);
        }
        public ActionResult ExportReports(int eventId)
        {
            string exportName = "Reports" + ".csv";
            CsvWriter writer;
            var streamoutput = new MemoryStream();
            var sw = new StreamWriter(streamoutput);
            var personreports = UnitOfWork.IPersonPracticeReportRepository.AsQueryable().Where(m => m.EventId == eventId)
                .GroupBy(m => m.PracticeReportId)
                //.Select(n => new {reportId = n.Key, report = n.Select(y => y.PracticeReport), people = n.Select(x => x.Person)});
                .Select(n => new ReportsRun() { Report = n.Select(m => m.PracticeReport).FirstOrDefault(), Participants = n.Select(m => m.Person).Distinct() });
            if (personreports.Count() > 0)
            {
                var CsvConfig = new CsvConfiguration();                
                writer = new CsvWriter(sw, CsvConfig);                
                writer.WriteRecords(personreports.ToList());
                sw.Flush();
            }
            streamoutput.Seek(0, SeekOrigin.Begin);
            return File(streamoutput, "text/csv", exportName);
        }
    }
}