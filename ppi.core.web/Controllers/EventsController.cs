using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using PPI.Core.Domain.Entities;
using PPI.Core.Domain.Concrete;
using PPI.Core.Domain.Abstract;
using PPI.Core.Web.Models;

namespace PPI.Core.Web.Controllers
{
    
    public class EventsController : BaseController
    {
        
		//Replace in Scafolding file
		//private CoreContainer db = new CoreContainer();

		public EventsController(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
            
        }

        // GET: /Events/
        [Authorize(Roles = "Admin,SiteCordinator,J3PAdmin")]
        public ActionResult Index(int? NumberSent)
        {
            //Only show events for there SITE
            IEnumerable<Event> model;
            if (User.IsInRole("SiteCordinator"))
            {
                int SiteId = CurrentSite;
                model = UnitOfWork.IEventRepository.AsQueryable().Where(m => m.ProgramSite.SiteId == SiteId);
            }
            else
            {
                 model = UnitOfWork.IEventRepository.AsQueryable();
            }
            ViewBag.NumberSent = NumberSent.GetValueOrDefault(0);

            return View(model);
        }

        // GET: /Events/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
			//replace scafolding
            //Event @event = db.Events.Find(id);
			var model = UnitOfWork.IEventRepository.First(m => m.Id == id);
            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        protected List<SelectListItem> ProgramSiteList()
        { 
            var SelectProgramSiteId = new List<SelectListItem>();            
            foreach (var item in UnitOfWork.IProgramSiteRepository.AsQueryable())
            {
                var selectItem = new SelectListItem();
                selectItem.Text = item.Site.FriendlyName + " - " + item.Program.ProgramName;
                selectItem.Value = item.Id.ToString();
                SelectProgramSiteId.Add(selectItem);
            }
            return SelectProgramSiteId;
        }

        protected List<SelectListItem> SelectEventStatusList(int cultureId)
        {
            var SelectEventStatusId = new List<SelectListItem>();
            foreach (var item in UnitOfWork.IEventStatusRepository.AsQueryable())
            {
                var selectItem = new SelectListItem();
                selectItem.Text = item.NameResx.ResxValues.FirstOrDefault(m => m.CultureId == cultureId).Value;
                selectItem.Value = item.Id.ToString();
                SelectEventStatusId.Add(selectItem);
            }
            return SelectEventStatusId;
        }

        protected List<SelectListItem> SurveyTypeList()
        {
            var SelectSurveyTypeId = new List<SelectListItem>();
            foreach (var item in UnitOfWork.ISurveyType.AsQueryable())
            {
                var selectItem = new SelectListItem();
                selectItem.Text = item.Name;
                selectItem.Value = item.Id.ToString();
                SelectSurveyTypeId.Add(selectItem);
            }
            return SelectSurveyTypeId;
        }
        
        // GET: /Events/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {

            var model = new EventCreateViewModel();                    
            model.Event = new Event { };

            ViewBag.ProgramSiteId = new SelectList(ProgramSiteList(), "Value", "Text");

            ViewBag.EventStatusId = new SelectList(SelectEventStatusList(1), "Value", "Text");

            ViewBag.EventTypeId = new SelectList(UnitOfWork.IEventTypeRepository.SelectEventTypeList(1), "Value", "Text");

            ViewBag.SurveyTypeId = new SelectList(SurveyTypeList(), "Value", "Text");

            var Reports = new List<CheckBoxModel>();
            foreach (var item in UnitOfWork.IPracticeReportRepository.AsQueryable().Where(m => m.Active == true))
	        {
		        var checkboxItem = new CheckBoxModel();
                checkboxItem.Selected = false;
                checkboxItem.Name = item.ReportTitleResx.ResxValues.FirstOrDefault(m => m.CultureId == 1).Value;
                checkboxItem.Value = item.Id.ToString();
                checkboxItem.Disabled = false;
                Reports.Add(checkboxItem);
	        }
            model.AvailibleReports = Reports;

            return View(model);
        }

        // POST: /Events/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EventCreateViewModel newEvent)
        {
            if (ModelState.IsValid)
            {
                //Also add records to the Email table for the default Email Types invite and Reminider
                var invite = new Email();
                var reminder = new Email();
                var Reports = new Email();
                var assessmentsComplete = new Email();

                Reports.EventId = newEvent.Event.Id;
                Reports.EmailTypeId = 6; //Reports
                Reports.DefaultFrom = newEvent.DefaultEmail;
                
                reminder.EventId = newEvent.Event.Id;
                reminder.EmailTypeId = 4; //Reminder
                reminder.DefaultFrom = newEvent.DefaultEmail;

                invite.DefaultFrom = newEvent.DefaultEmail;
                invite.EmailTypeId = 3; //Invite
                invite.EventId = newEvent.Event.Id;

                assessmentsComplete.EventId = newEvent.Event.Id;
                assessmentsComplete.EmailTypeId = 7; //Assessments Complete
                assessmentsComplete.DefaultFrom = newEvent.DefaultEmail;

                newEvent.Event.Emails.Add(invite);
                newEvent.Event.Emails.Add(reminder);
                newEvent.Event.Emails.Add(Reports);
                newEvent.Event.Emails.Add(assessmentsComplete);
                newEvent.Event.ProgramSitesId = newEvent.ProgramSiteId;
                newEvent.Event.CreateDate = DateTime.Now;
                UnitOfWork.IEventRepository.Add(newEvent.Event);
				//add the siteevent record
                //newEvent.Event.ProgramSiteEvents.Add(new ProgramSiteEvent { ProgramSitesId = newEvent.ProgramSiteId });
                //1.2.2015 moving away from PSE table

                //loop thru selected reports
                if (newEvent.PracticeReportsIds != null)
                { 
                    foreach (var item in newEvent.PracticeReportsIds)
                    {
                        newEvent.Event.EventPracticeReports.Add(new EventPracticeReport { EventId = newEvent.Event.Id, PracticeReportId = int.Parse(item.ToString()) });
                    }
                }      
                
                
                UnitOfWork.Commit();		
                return RedirectToAction("Index");
            }

            ViewBag.ProgramSiteId = new SelectList(ProgramSiteList(), "Value", "Text");

            ViewBag.EventStatusId = new SelectList(SelectEventStatusList(1), "Value", "Text");

            ViewBag.SurveyTypeId = new SelectList(SurveyTypeList(), "Value", "Text");

            ViewBag.EventTypeId = new SelectList(UnitOfWork.IEventTypeRepository.SelectEventTypeList(1), "Value", "Text");
            return View(newEvent);
        }

        // GET: /Events/Edit/5
        //CordinatorEdit
        public ActionResult CordinatorEdit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Event @event = db.Events.Find(id);
            var model = new EventCordinatorEditViewModel();
            model.Event = UnitOfWork.IEventRepository.First(m => m.Id == id);
            model.DefaultEmail = model.Event.Emails.FirstOrDefault().DefaultFrom;
            model.datepickerDateRange = model.Event.StartDate.Value.ToShortDateString() + " - " + model.Event.EndDate.Value.ToShortDateString();
            if (model == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProgramSiteId = new SelectList(ProgramSiteList(), "Value", "Text");

            ViewBag.EventStatusId = new SelectList(SelectEventStatusList(1), "Value", "Text",model.Event.EventStatusId);

            ViewBag.EventTypeId = new SelectList(UnitOfWork.IEventTypeRepository.SelectEventTypeList(1), "Value", "Text", model.Event.EventTypeId);
            return View(model);
        }

        // POST: /Events/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CordinatorEdit(EventCordinatorEditViewModel editEvent)
        {
            if (ModelState.IsValid)
            {
                //Fix up the Start End Date
                var startDate = DateTime.Parse(editEvent.datepickerDateRange.Substring(0, editEvent.datepickerDateRange.IndexOf("-") - 1));
                var endDate = DateTime.Parse(editEvent.datepickerDateRange.Substring(editEvent.datepickerDateRange.IndexOf("-") + 1));

                var Event = UnitOfWork.IEventRepository.First(m => m.Id == editEvent.Event.Id);
                Event.StartDate = startDate;
                Event.EndDate = endDate;
                Event.ReviewDate = editEvent.Event.ReviewDate;
                Event.Description = editEvent.Event.Description;
                foreach (var item in Event.Emails)
                {
                    item.DefaultFrom = editEvent.DefaultEmail;
                    UnitOfWork.IEmailRepository.Update(item);
                }
                
                UnitOfWork.IEventRepository.Update(Event);

                UnitOfWork.Commit();
                return RedirectToAction("Index");
            }
           // ViewBag.EventStatusId = new SelectList(UnitOfWork.IEventStatusRepository.AsQueryable(), "Id", "Id", @event.EventStatusId);
           // ViewBag.EventTypeId = new SelectList(UnitOfWork.IEventTypeRepository.AsQueryable(), "Id", "Id", @event.EventTypeId);
            return View(editEvent);
        }
        
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Event @event = db.Events.Find(id);

            var model = new EventEditViewModel();
            model.Event = UnitOfWork.IEventRepository.First(m => m.Id == id);
            var Reports = new List<CheckBoxModel>();
            foreach (var item in UnitOfWork.IPracticeReportRepository.AsQueryable().Where(m => m.Active == true))
            {
                var checkboxItem = new CheckBoxModel();
                checkboxItem.Selected = UnitOfWork.IEventPracticeReportRepository.AsQueryable().ToList().Exists(m => m.EventId == id && m.PracticeReportId == item.Id);
                checkboxItem.Name = item.ReportTitleResx.ResxValues.FirstOrDefault(m => m.CultureId == 1).Value;
                checkboxItem.Value = item.Id.ToString();
                checkboxItem.Disabled = false;
                Reports.Add(checkboxItem);
            }
            model.DefaultEmail = model.Event.Emails.FirstOrDefault().DefaultFrom;
            model.AvailibleReports = Reports;
                       
            if (model == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProgramSiteId = new SelectList(ProgramSiteList(), "Value", "Text");

            ViewBag.EventStatusId = new SelectList(SelectEventStatusList(1), "Value", "Text");

            ViewBag.SurveyTypeId = new SelectList(SurveyTypeList(), "Value", "Text");

            ViewBag.EventTypeId = new SelectList(UnitOfWork.IEventTypeRepository.SelectEventTypeList(1), "Value", "Text");
            return View(model);
        }

        // POST: /Events/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EventEditViewModel EventEdit)
        {
            if (ModelState.IsValid)
            {
               // db.Entry(@event).State = EntityState.Modified;
                //db.SaveChanges();
                UnitOfWork.IEventRepository.Update(EventEdit.Event);
                var deleteList = UnitOfWork.IEventPracticeReportRepository.AsQueryable().Where(m => m.EventId == EventEdit.Event.Id).ToList();
                UnitOfWork.IEventPracticeReportRepository.Delete(deleteList);
                if (EventEdit.PracticeReportsIds != null)
                { 
                    foreach (var item in EventEdit.PracticeReportsIds)
                    {
                        EventEdit.Event.EventPracticeReports.Add(new EventPracticeReport() { EventId = EventEdit.Event.Id, PracticeReportId = int.Parse(item.ToString()) });
                    }
                }

                foreach (var item in UnitOfWork.IEmailRepository.AsQueryable().Where(m => m.EventId == EventEdit.Event.Id))
                {
                    item.DefaultFrom = EventEdit.DefaultEmail;
                    UnitOfWork.IEmailRepository.Update(item);
                }
				UnitOfWork.Commit();	
				return RedirectToAction("Index");
            }
            ViewBag.ProgramSiteId = new SelectList(ProgramSiteList(), "Value", "Text");

            ViewBag.EventStatusId = new SelectList(SelectEventStatusList(1), "Value", "Text");

            ViewBag.SurveyTypeId = new SelectList(SurveyTypeList(), "Value", "Text");

            ViewBag.EventTypeId = new SelectList(UnitOfWork.IEventTypeRepository.SelectEventTypeList(1), "Value", "Text");
            return View(EventEdit);
        }

        // GET: /Events/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Event @event = db.Events.Find(id);
			var model = UnitOfWork.IEventRepository.First(m => m.Id == id);
            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        // POST: /Events/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
           // Event @event = db.Events.Find(id);
           // db.Events.Remove(@event);
           // db.SaveChanges();
		   var model = UnitOfWork.IEventRepository.First(m => m.Id == id);
		   UnitOfWork.IEventRepository.Delete(model);	
		   UnitOfWork.Commit();	

            return RedirectToAction("Index");
        }

        public ActionResult Convert(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Event @event = db.Events.Find(id);

            var model = new EventCreateViewModel();
            var OrderForm = UnitOfWork.IOrderFormRepository.AsQueryable().FirstOrDefault(m => m.Id == id);
            model.Event = new Event();
            model.Event.Name = OrderForm.EventName;                        
            model.Event.Description = OrderForm.EventDescription;
            model.Event.Billable = OrderForm.Billable;
            model.Event.CreateDate = DateTime.Now;
            model.Event.StartDate = OrderForm.StartDate;
            model.Event.EndDate = OrderForm.EndDate;
            model.Event.ReviewDate = OrderForm.ReviewDate;            
            model.Event.EventTypeId = OrderForm.EventTypeId;
            model.Event.Placement = OrderForm.Placement;
            //model.Event.EventPracticeReports = OrderForm.OrderFormPracticeReports.;
            foreach (var item in OrderForm.OrderFormPracticeReports)
            {
                model.Event.EventPracticeReports.Add(new EventPracticeReport() { EventId = model.Event.Id, PracticeReportId = item.PracticeReportId });
            }

            var Reports = new List<CheckBoxModel>();
            foreach (var item in UnitOfWork.IPracticeReportRepository.AsQueryable().Where(m => m.Active == true))
            {
                var checkboxItem = new CheckBoxModel();
                checkboxItem.Selected = model.Event.EventPracticeReports.AsQueryable().ToList().Exists(m => m.PracticeReportId == item.Id);
                checkboxItem.Name = item.ReportTitleResx.ResxValues.FirstOrDefault(m => m.CultureId == 1).Value;
                checkboxItem.Value = item.Id.ToString();
                checkboxItem.Disabled = false;
                Reports.Add(checkboxItem);
            }

            model.AvailibleReports = Reports;

            if (model == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProgramSiteId = new SelectList(ProgramSiteList(), "Value", "Text");

            ViewBag.EventStatusId = new SelectList(SelectEventStatusList(1), "Value", "Text");

            ViewBag.EventTypeId = new SelectList(UnitOfWork.IEventTypeRepository.SelectEventTypeList(1), "Value", "Text");
            return View("Create", model);
        }
        protected override void Dispose(bool disposing)
        {
            //if (disposing)
            //{
            //    db.Dispose();
            //}
           // base.Dispose(disposing);
        }
        [Authorize(Roles = "Admin,J3PAdmin")]
        public ActionResult Email(int eventId)
        {
            //First find the participants that have not had a Transistion TPR Id #2
            IList<PersonEvent> currentEventParticipants = UnitOfWork.IPersonEventRepository.AsQueryable()
                .Where(m => m.EventId == eventId)
                .ToList();
            int i = 0;
            foreach (var item in currentEventParticipants)
            {
                //create the report and email it
                var ReportEmailed = item.Person.PersonEmails.Where(m => m.PersonId == item.PersonId).Select(m => m.Email).FirstOrDefault(s => s.EmailTypeId == 6);
                var HasData = UnitOfWork.IManual_Hogan_ImportRepository.AsQueryable().FirstOrDefault(m => m.Hogan_User_ID == item.Person.Hogan_Id);
                bool RunReport = false;
                if (HasData != null)
                {
                    if (!String.IsNullOrWhiteSpace(HasData.HPIDate))
                    {
                        RunReport = true;
                    }
                }
                //var ExistingReportSent = item.Person.PersonPracticeReports.AsQueryable().FirstOrDefault(m => m.PracticeReportId == 2);
                if (ReportEmailed == null && RunReport == true)
                {                    
                    var ReportC = new ReportsController(UnitOfWork);
                    ReportC.ControllerContext = this.ControllerContext;
                // Report is 2 and siteId -1 to force branding to be report default
                                        
                    var report = ReportC.DownloadProfessionReport(item.Person.Hogan_Id, CurrentCulture, 2, -1, item.Event.ProgramSite.ProgramId, eventId, "Event" + eventId.ToString());
                    var EmailSend = new PPI.Core.Web.Infrastructure.EmailService();
                    var MailList = new List<EmailModel>();

                    
                    var Mail = new EmailModel(){ AttachmentPath = report, to = item.Person.PrimaryEmail};                                                            

                    var EmailTemplate = UnitOfWork.IEmailRepository.AsQueryable().FirstOrDefault(m => m.EventId == eventId && m.EmailTypeId == 6); // Reports                    
                    Mail.from = EmailTemplate.DefaultFrom;
                    Mail.body = EmailTemplate.Introduction + EmailTemplate.Closing;
                    Mail.subject = EmailTemplate.Subject;                    
                    var personEmail = new PersonEmail();
                    personEmail.PersonId = item.Person.Id;
                    personEmail.EmailStatusId = 3; // inprocess
                    personEmail.EmailId =EmailTemplate.Id;
                    personEmail.EmailBody = Mail.body;                         
                    personEmail.SentDate = DateTime.Now;
                    personEmail.Origin = "UI - Event";
                    UnitOfWork.IPersonEmailRepository.Add(personEmail);
                    UnitOfWork.Commit();
                    Mail.personEmailId = personEmail.Id;  
                    MailList.Add(Mail);
                    EmailSend.SendEmailAsync(MailList,false,UnitOfWork);
                    i++;
                }
            }
            return RedirectToAction("Index", new { NumberSent = i});
        }        
    }
}
