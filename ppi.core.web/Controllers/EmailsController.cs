using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Text;
using PPI.Core.Web.Models;
using PPI.Core.Domain.Entities;
using PPI.Core.Domain.Abstract;
using System.IO;
using System.Web.UI;
using System.Web.Routing;
using System.Configuration;
using Microsoft.AspNet.Identity;                    //for usermanager
using Microsoft.AspNet.Identity.EntityFramework;    //for usermanager
using PPI.Core.Web.Infrastructure;

namespace PPI.Core.Web.Controllers
{
    [Authorize(Roles = "Admin,SiteCordinator,J3PAdmin")]
    public class EmailsController : BaseController
    {
        [Log]
        [AllowAnonymous]        
        [HttpPost]
        //[Authorize(Roles = "Admin")]     
        public string SendCompletedNotification(string hoganId, string userName, string password)
        {
            if (string.IsNullOrWhiteSpace(hoganId))
            {
                return (string.Format("Hogan ID# {0} is invalid", hoganId));
            }

            //authentication
            AccountController accountController = new AccountController(UnitOfWork);
            var user = accountController.UserManager.Find(userName, password);

            if (user == null)
            {
                return (string.Format("HoganId ID# {0}: Username or Password is incorrect", hoganId));
            }

            //now make sure there is still work to do.
            //recheck assessment completeion, in case controller being called independently

            var hoganPerson = UnitOfWork.IManual_Hogan_ImportRepository.AsQueryable()
                .Where(h => h.Hogan_User_ID == hoganId)
                .FirstOrDefault();

            if (hoganPerson == null)
            {
                return (string.Format("Hogan ID# {0} couldn't be located", hoganId));
            }

            if (string.IsNullOrWhiteSpace(hoganPerson.HPIDate) || string.IsNullOrWhiteSpace(hoganPerson.MVPIDate) || string.IsNullOrWhiteSpace(hoganPerson.HDSDate))
            {
                return (string.Format("Hogan ID# {0}: At least one assessment has not been completed", hoganId));
            }
                            
            var recipPerson = UnitOfWork.IPersonRepository.AsQueryable()
                .Where(p => p.Hogan_Id == hoganId)
                .Where(p => p.PersonEmails.Count(pe => pe.Email.EmailTypeId == 7) < 1)
                .Where(p => p.PersonEmails.Count(pe => pe.Email.EmailTypeId == 3) > 0)  //just in case
                .FirstOrDefault();

            if (recipPerson == null)
            {
                return (string.Format("Hogan ID# {0}: Nothing to do", hoganId));
            }

            var recipEmail = recipPerson.PersonEmails.Where(r => r.Email.EmailTypeId == 3)
                .FirstOrDefault()
                .Email;
                //.FirstOrDefault()
                //.EventId;
//            var recipEvent = recipEmail.Email.EventId;

            var GOEmail = new PPI.Core.Web.Infrastructure.EmailService();
            var mailinfo = UnitOfWork.IEmailRepository.First(m => m.EmailTypeId == 7 && m.EventId == recipEmail.EventId);
            var Emails = new List<EmailModel>();
            var personEmail = new PersonEmail();                        
            var Email = new EmailModel();
            var EmailTemplate = new EmailTemplateModel();

            EmailTemplate.subject = mailinfo.Subject;
            EmailTemplate.closing = mailinfo.Closing;
            EmailTemplate.introduction = mailinfo.Introduction;
            EmailTemplate.hogan_Id = recipPerson.Hogan_Id;
            EmailTemplate.hogan_password = recipPerson.Hogan_Password;
            Email.to = recipPerson.PrimaryEmail;
            Email.from = mailinfo.DefaultFrom;
            Email.subject = mailinfo.Subject;
            Email.body = RenderPartialToString("_PartialEmailFormat", EmailTemplate);
            Emails.Add(Email);
            personEmail.PersonId = recipPerson.Id;
            personEmail.EmailStatusId = 3; // inprocess
            personEmail.EmailId = mailinfo.Id;
            personEmail.EmailBody = Email.body;
            personEmail.SentDate = DateTime.Now;
            personEmail.Origin = "Hogan";
            UnitOfWork.IPersonEmailRepository.Add(personEmail);
            UnitOfWork.Commit();
            Email.personEmailId = personEmail.Id;
            
            GOEmail.SendEmailAsync(Emails, false, UnitOfWork);            

            if (Emails[0].emailStatusId == 2)
            {
                return(string.Format("Email failed for PersonEmailId {0}", personEmail.Id));
            }

            return (string.Empty);  //success
        }        

        [Log]
        [AllowAnonymous]        
        [HttpPost]
        //[Authorize(Roles = "Admin")]     
        public string ProcessScheduledBatch(int? scheduledEmailId, string userName, string password)
        {
            if ((scheduledEmailId ?? 0) < 1)
            {
                return ("scheduledEmailId is invalid");
            }

            //authentication
            AccountController accountController = new AccountController(UnitOfWork);
            var user = accountController.UserManager.Find(userName, password);
            
            if (user == null)
            {
                return (string.Format("Schedule ID# {0}: Username or Password is incorrect", scheduledEmailId));
            }

            ScheduledEmail currentSchedule =
                UnitOfWork.IScheduledEmailRepository.AsQueryable().SingleOrDefault(m => m.Id == scheduledEmailId);  //.ToList();

            if (currentSchedule == null)
            {
                //throw new HttpException((int)HttpStatusCode.BadRequest, "scheduled cannot be located");
                return (string.Format("Schedule ID#{0} cannot be located", scheduledEmailId));
            }            

            if (currentSchedule.CompletedDate != null)
            {
                //skip this scheduled job, already done
                return (string.Format("Schedule ID#{0}: Already complete", scheduledEmailId));
            }

            if (currentSchedule.StartDate != null)
            {
                //skip this scheduled job, already in progress
                return (string.Format("Schedule ID#{0}: Already in progress", scheduledEmailId));
            }

            currentSchedule.StartDate = DateTime.Now;
            UnitOfWork.IScheduledEmailRepository.Update(currentSchedule);
            UnitOfWork.Commit();

            //proceed with processing scheduled batch                        
            IEnumerable<ScheduledEmailPerson> scheduleItems = 
                UnitOfWork.IScheduledEmailPersonRepository.AsQueryable()
                    .Where(m => (m.Enabled ?? false) == true
                        && m.ScheduledEmailId == scheduledEmailId
                        //&& m.StartDate == null 
                        && m.CompletedDate == null).ToList();

            if (scheduleItems.Count() < 1)
            {
                //mark the schedule as complete - this would either happen due to an error or if a new schedule caused
                //existing schedule items to be disabled
                currentSchedule.CompletedDate = DateTime.Now;
                UnitOfWork.IScheduledEmailRepository.Update(currentSchedule);
                UnitOfWork.Commit();
            }
            else
            {
                var GOEmail = new PPI.Core.Web.Infrastructure.EmailService();                
                var emails = new List<EmailModel>();
                var personEmail = new PersonEmail();
                var mailInfo = UnitOfWork.IEmailRepository.AsQueryable().FirstOrDefault(m => m.EmailTypeId == currentSchedule.Email.EmailTypeId && m.EventId == currentSchedule.Email.EventId);

                if (mailInfo == null)
                {
                    return (string.Format("Schedule ID# {0}: Mail info is null, cannot process schedule", scheduledEmailId));                    
                }

                foreach (ScheduledEmailPerson scheduleItem in scheduleItems)
                {                    
                    //scheduleItem.StartDate = DateTime.Now;
                    //UnitOfWork.IScheduledEmailPersonRepository.Update(scheduleItem);
                    //UnitOfWork.Commit();

                    var person = UnitOfWork.IPersonRepository.SingleOrDefault(m => m.Id == scheduleItem.PersonId);

                    if (person != null)
                    {
                        var EmailTemplate = new EmailTemplateModel();
                        EmailTemplate.subject = mailInfo.Subject;
                        EmailTemplate.closing = mailInfo.Closing ?? string.Empty;
                        EmailTemplate.introduction = mailInfo.Introduction;
                        EmailTemplate.hogan_Id = person.Hogan_Id;
                        EmailTemplate.hogan_password = person.Hogan_Password;

                        var email = new EmailModel();
                        email.to = person.PrimaryEmail;
                        email.from = mailInfo.DefaultFrom;
                        email.subject = mailInfo.Subject;
                        email.body = RenderPartialToString("_PartialEmailFormat", EmailTemplate);                      
                        email.scheduledEmailPersonId = scheduleItem.Id;
                        emails.Add(email);

                        personEmail.PersonId = person.Id;
                        personEmail.EmailStatusId = 3;      // inprocess
                        personEmail.EmailId = mailInfo.Id;
                        personEmail.EmailBody = email.body;
                        personEmail.SentDate = DateTime.Now;
                        personEmail.Origin = "Job";
                        UnitOfWork.IPersonEmailRepository.Add(personEmail);
                        UnitOfWork.Commit();

                        email.personEmailId = personEmail.Id;
                    }                    
                }
                
                //send the mail
                GOEmail.SendEmailAsync(emails, false, UnitOfWork);

                //check results - don't fail on email issues, but log it
                StringBuilder failedIds = new StringBuilder(string.Empty);
                foreach(EmailModel email in emails)
                {
                    if (email.emailStatusId == 2)
                    {
                        failedIds.AppendFormat("{0},", email.personEmailId);                        
                    }
                }

                if (failedIds.ToString() != string.Empty)
                {
                    ApplicationException ex =
                        new ApplicationException(string.Format("Schedule Id:{0}: One or more emails failed for person email ids: {1}", currentSchedule.Id, failedIds.ToString()));
                    ExceptionSvcHelper.HandleNonFatalException(ex);
                }
                
                //done with batch - mark as complete
                currentSchedule.CompletedDate = DateTime.Now;
                UnitOfWork.IScheduledEmailRepository.Update(currentSchedule);
                UnitOfWork.Commit();
            }

            return (string.Empty);  //success
        }        

        /// <summary>
        /// Render a view into a string. It's a hack, it may fail badly.
        /// </summary>
        /// <param name="name">Name of the view, that is, its path.</param>
        /// <param name="data">Data to pass to the view, a model or something like that.</param>
        /// <returns>A string with the (HTML of) view.</returns>
        protected string RenderPartialToString(string viewName, object model)
        {
            if (string.IsNullOrEmpty(viewName))
                viewName = ControllerContext.RouteData.GetRequiredString("action");

            ViewData.Model = model;

            using (StringWriter sw = new StringWriter())
            {
                ViewEngineResult viewResult = ViewEngines.Engines.FindPartialView(ControllerContext, viewName);
                ViewContext viewContext = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, sw);
                viewResult.View.Render(viewContext, sw);

                return sw.GetStringBuilder().ToString();
            }
        }

        //Replace in Scafolding file
        //private CoreContainer db = new CoreContainer();

        public EmailsController(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        // GET: /Emails/
        [Authorize(Roles = "Admin,J3PAdmin")]
        public ActionResult Index()
        {
            // replace in scafoding file
            //var emails = db.Emails.Include(e => e.Event);

            var model = UnitOfWork.IEmailRepository.AsQueryable();
            return View(model);
        }

        [Authorize(Roles = "SiteCordinator,Admin,J3PAdmin")]
        public ActionResult CordinatorSendSwitch(int eventId)
        {
            clearSelectedList();
            return RedirectToAction("Send", new { eventId = eventId });
            //Clear the Session information if you switch events           
        }
        [Authorize(Roles = "SiteCordinator,Admin,J3PAdmin")]
        public ActionResult CordinatorEventSwitch(int eventId)
        {
            return RedirectToAction("Cordinator", new { eventId = eventId });
        }
        //CordinatorIndex
        [Authorize(Roles = "SiteCordinator")]
        [ActionName("Cordinator")]
        public ActionResult CordinatorIndex(int? eventId)
        {
            var model = new PPI.Core.Web.Models.EmailCordinatorViewModel();
            int SiteId = CurrentSite;
            var Events = this.Events;  //UnitOfWork.IEventRepository.AsQueryable().Where(m => m.ProgramSite.SiteId == SiteId);
            int? FirstEventId = this.CurrentEvent;
            if (Events.Count() > 0)
            {
                if (eventId.HasValue)
                {
                    FirstEventId = eventId.Value;
                    this.CurrentEvent = FirstEventId.Value;
                }               
            }

            model.EmailSetup = UnitOfWork.IEmailRepository.AsQueryable().Where(m => m.EventId == FirstEventId).ToList();
            if (model.EmailSetup == null)
            {
                model.EmailSetup = new List<Email>();
            }
            else
            {
                foreach (var item in model.EmailSetup)
                {
                    //setup the preview
                    var EmailTemplate = new EmailTemplateModel();
                    EmailTemplate.introduction = item.Introduction;
                    EmailTemplate.closing = item.Closing;
                    EmailTemplate.subject = item.Subject;
                    EmailTemplate.hogan_Id = "xxxxxxxx";
                    EmailTemplate.hogan_password = "xxxxxxxx";
                    item.Preview = RenderPartialToString("_PartialEmailFormat", EmailTemplate);
                }

            }
            model.EventId = FirstEventId.GetValueOrDefault(0);
            ViewBag.EventId = new SelectList(Events, "Value", "Text", model.EventId);
            return View(model);
        }

        // GET: /Emails/Details/5
        [Authorize(Roles = "Admin,J3PAdmin")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //replace scafolding
            //Email email = db.Emails.Find(id);
            var model = UnitOfWork.IEmailRepository.First(m => m.Id == id);
            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        private void personCheckHelper(int Id, int listItem, int? selectAll)
        {
            var PersonList = GetSelectedList(listItem);
            var SelectPerson = PersonList.SelectedParticipants.FirstOrDefault(m => m.Id == Id);
            if (SelectPerson == null)
            {
                PersonList.SelectedParticipants.Add(new Person { Id = Id, selected = true });
            }
            else if (selectAll == null || selectAll == 0)
            {
                PersonList.SelectedParticipants.Remove(SelectPerson);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public void personCheckAll(int listItem, int selectAll, string idList)
        {
            string[] arrIds = (idList ?? string.Empty).Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string sId in arrIds)
            {
                personCheckHelper(Convert.ToInt32(sId), listItem, selectAll);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public void personCheck(int Id, int listItem)
        {
            personCheckHelper(Id, listItem, null);

        }


        private SelectedPerson GetSelectedList(int listItem)
        {
            SelectedPerson PersonList = (SelectedPerson)Session["SelectedPerson" + listItem];
            if (PersonList == null)
            {
                PersonList = new SelectedPerson();
                var Participants = new List<Person>();
                PersonList.SelectedParticipants = Participants;
                Session["SelectedPerson" + listItem] = PersonList;
            }
            return PersonList;
        }

        private void clearSelectedList()
        {
            Session.Clear();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Schedule(int eventId, int emailTypeIdForSchedule, DateTime scheduleDatePickerVal, int removeFlag)
        {
            var getEmailList = GetSelectedList(emailTypeIdForSchedule);

            if (getEmailList.SelectedParticipants.Count < 1)
            {
                TempData["showNothingConf"] = true;
            }
            else
            {
                var mailinfo = UnitOfWork.IEmailRepository.AsQueryable()
                    .FirstOrDefault(m => m.EmailTypeId == emailTypeIdForSchedule && m.EventId == eventId);

                if (mailinfo == null)
                {
                    throw new ApplicationException("Mail info is null when scheduling an email");
                }

                if (removeFlag == 1)    //remove any matches from schedule
                {
                    foreach (var item in getEmailList.SelectedParticipants)
                    {
                        var person = UnitOfWork.IPersonRepository.SingleOrDefault(m => m.Id == item.Id);

                        if (person == null)
                        {
                            throw new ApplicationException("Person is null when unscheduling an email");
                        }

                        IEnumerable<ScheduledEmailPerson> existingScheduleEntries =
                            UnitOfWork.IScheduledEmailPersonRepository.AsQueryable()
                            .Where(m => m.PersonId == person.Id                                
                                && m.ScheduledEmail.EmailId == mailinfo.Id
                                //&& m.StartDate == null
                                && m.CompletedDate == null
                                && (m.Enabled ?? false) == true
                                );

                        foreach (var eitem in existingScheduleEntries)
                        {
                            eitem.Enabled = false;
                            UnitOfWork.IScheduledEmailPersonRepository.Update(eitem);
                        }
                    }

                    UnitOfWork.Commit();
                    TempData["showRemoveConf"] = true;
                }
                else    //add to schedule
                {
                    var scheduledEmail = new ScheduledEmail();
                    var scheduledEmailPerson = new ScheduledEmailPerson();                    

                    scheduledEmail.EmailId = mailinfo.Id;                    
                    scheduledEmail.ScheduleDate = scheduleDatePickerVal;
                    
                    UnitOfWork.IScheduledEmailRepository.Add(scheduledEmail);
                    //UnitOfWork.Commit();

                    foreach (var item in getEmailList.SelectedParticipants)
                    {
                        //var person = UnitOfWork.IPersonRepository.First(m => m.Id == item.Id);
                        var person = UnitOfWork.IPersonRepository.SingleOrDefault(m => m.Id == item.Id);

                        if (person == null)
                        {
                            throw new ApplicationException("Person is null when scheduling an email");
                        }

                        //first, update existing unprocessed schedule entries for these people/event/emailtype so they are disabled
                        IEnumerable<ScheduledEmailPerson> existingScheduleEntries =
                            UnitOfWork.IScheduledEmailPersonRepository.AsQueryable()
                            .Where(m => m.PersonId == person.Id                                
                                //&& m.StartDate == null
                                && m.CompletedDate == null
                                && m.ScheduledEmail.EmailId == mailinfo.Id
                                && (m.Enabled ?? false) == true
                                );

                        foreach (var eitem in existingScheduleEntries)
                        {
                            eitem.Enabled = false;  //disable existing scheduled email
                            UnitOfWork.IScheduledEmailPersonRepository.Update(eitem);
                        }

                        //proceed with adding person/event/emailtype to the schedule
                        scheduledEmailPerson.ScheduledEmailId = scheduledEmail.Id;
                        scheduledEmailPerson.PersonId = person.Id;
                        scheduledEmailPerson.Enabled = true;

                        UnitOfWork.IScheduledEmailPersonRepository.Add(scheduledEmailPerson);
                        UnitOfWork.Commit();
                    }

                    TempData["showScheduleConf"] = true;
                }
            }

            clearSelectedList();    //clear the list regardless of schedule or unschedule

            return RedirectToAction("Send", new { @eventId = eventId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Send(int EventId, int EmailTypeId)
        {
            var getEmailList = GetSelectedList(EmailTypeId);

            if (getEmailList.SelectedParticipants.Count < 1)
            {
                TempData["showNothingConf"] = true; 
            }
            else
            {
                var GOEmail = new PPI.Core.Web.Infrastructure.EmailService();
                var mailinfo = UnitOfWork.IEmailRepository.First(m => m.EmailTypeId == EmailTypeId && m.EventId == EventId);
                var eventType = UnitOfWork.IEventRepository.Single(x => x.Id == EventId); 
                var Emails = new List<EmailModel>();
                var personEmail = new PersonEmail();

                foreach (var item in getEmailList.SelectedParticipants)
                {
                    var person = UnitOfWork.IPersonRepository.First(m => m.Id == item.Id);
                    var Email = new EmailModel();
                    var EmailTemplate = new EmailTemplateModel();
                    EmailTemplate.subject = mailinfo.Subject;
                    EmailTemplate.closing = mailinfo.Closing;
                    EmailTemplate.introduction = mailinfo.Introduction;
                    EmailTemplate.UserName = person.FirstName;

                    Email.to = person.PrimaryEmail;
                    Email.from = mailinfo.DefaultFrom;
                    Email.subject = mailinfo.Subject;
                    if (eventType.SurveyType != null && eventType.SurveyType.Name.ToUpper() == "J3P")
                    {
                        EncryptedQueryString args = new EncryptedQueryString();                        
                        args["arg"] = person.Id.ToString();
                        args["arg1"] = person.J3P_Id;  
                        
                        EmailTemplate.j3p_URL = string.Format(ConfigurationManager.AppSettings["J3P_URL"] + "{0}", args);
                        Email.body = RenderPartialToString("_PartialEmailFormatJ3P", EmailTemplate);
                    }
                    else
                    {
                        EmailTemplate.hogan_Id = person.Hogan_Id;
                        EmailTemplate.hogan_password = person.Hogan_Password;                    
                        Email.body = RenderPartialToString("_PartialEmailFormat", EmailTemplate);
                    }
                    Emails.Add(Email);
                    personEmail.PersonId = person.Id;
                    personEmail.EmailStatusId = 3; // inprocess
                    personEmail.EmailId = mailinfo.Id;
                    personEmail.EmailBody = Email.body;
                    personEmail.SentDate = DateTime.Now;
                        personEmail.Origin = "UI";
                    UnitOfWork.IPersonEmailRepository.Add(personEmail);
                    UnitOfWork.Commit();
                    Email.personEmailId = personEmail.Id;
                }
                GOEmail.SendEmailAsync(Emails, false, UnitOfWork);

                //generate email stats and persist them
                int numFailed = 0;
                int numSent = 0;
                StringBuilder failedIds = new StringBuilder(string.Empty);

                foreach (EmailModel objEmail in Emails)
                {
                    if (objEmail.emailStatusId == 1)
                    {
                        numSent++;
                    }
                    else if (objEmail.emailStatusId == 2)
                    {
                        numFailed++;
                        failedIds.AppendFormat("{0},", objEmail.personEmailId);
                    }
                }

                if (failedIds.ToString() != string.Empty)
                {
                    ApplicationException ex =
                        new ApplicationException(string.Format("One or more emails failed for person email ids: {0}", failedIds.ToString()));
                    ExceptionSvcHelper.HandleNonFatalException(ex);
                }

                TempData["showEmailStats"] = true;  //non null is what's important
                TempData["numEmailsFailed"] = numFailed;
                TempData["numEmailsSent"] = numSent;
            }

            clearSelectedList();

            return RedirectToAction("Send", new { @eventId = EventId });
        }

        [Authorize(Roles = "SiteCordinator,Admin,J3PAdmin")]
        public ActionResult Send(int? eventId, int? page, int? emailTypeId, string search)
        {
            var model = new EmailSendViewModel();
            var TabList = new List<Mailing>();            
            SelectList Events = null;            
            Events = this.Events;

            int? FirstEventId = this.CurrentEvent;
            if (Events.Count() > 0)
            {
                if (eventId.HasValue)
                {
                    FirstEventId = eventId.Value;
                    this.CurrentEvent = FirstEventId.Value;
                }               
            }
                //FirstEventId = (eventId != null) ? eventId : int.Parse(Events.FirstOrDefault().Value);
            model.EventId = FirstEventId;
            ViewBag.EventId = new SelectList(Events, "Value", "Text", model.EventId);
            if (model.EventId != -1)
            {
                //var Emails = UnitOfWork.IEventRepository.AsQueryable().FirstOrDefault(m => m.Id == FirstEventId).Emails;
                var Results = UnitOfWork.IEventRepository.AsQueryable().FirstOrDefault(m => m.Id == FirstEventId);                
                ViewData["SurveyType"] = Results.SurveyType.Name;
                var Emails = Results.Emails;
                    //Events.First(m => m.Id == FirstEventId).Emails;
                //Invites first then Reminders
                //If emailypeid is not null, ONLY do that Email group because you are paging        
                foreach (var item in Emails.Where(m => m.EmailTypeId != 6 && m.EmailTypeId != 7)) //Invites first order by ID  ; exclude reports
                {
                    var Mailer = new PPI.Core.Web.Models.Mailing();
                    Mailer.active = (item.EmailTypeId == emailTypeId) ? true : false;
                    Mailer.Email = item;
                    if (item.EmailTypeId == 3)
                    {
                        //if emailtypeId is null, default to the invit (3) as active
                        Mailer.active = (emailTypeId == null) ? true : Mailer.active;
                        var totalRecords = UnitOfWork.IPersonEventRepository.AsQueryable()
                            .Where(m => m.EventId == FirstEventId)
                            .Where(m => m.Person.PersonEmails.Count(pe => pe.Email.EmailTypeId == 3) == 0)
                            .Select(m => m.Person).Count();
                        PagingInfo pagingInfo;
                        pagingInfo = new PagingInfo { CurrentPage = page.GetValueOrDefault(1), PageCount = 5, TotalRecords = totalRecords };
                        pagingInfo.PageSize = int.Parse(PPI.Core.Web.Infrastructure.Utility.GetCookie("pageSize"));
                        pagingInfo.LastPage = totalRecords / pagingInfo.PageSize;
                        Mailer.PagingInfo = pagingInfo;
                        var PersonList = new List<Person>();
                        if (search != null && search != "")
                            PersonList = UnitOfWork.IPersonEventRepository.AsQueryable()
                                .Where(m => m.EventId == FirstEventId).Select(m => m.Person)
                                .Where(p => p.PersonEmails.Count(pe => pe.Email.EmailTypeId == 3) == 0)
                                .Where(p => p.Hogan_Id == search || p.LastName.ToUpper() == search.ToUpper() || p.PrimaryEmail.ToUpper() == search.ToUpper() || p.AAMCNumber == search)
                                .ToList().OrderBy(m => m.LastName)
                                .Skip((pagingInfo.CurrentPage - 1) * pagingInfo.PageSize)
                                .Take(pagingInfo.PageSize).ToList();
                        else
                            PersonList = UnitOfWork.IPersonEventRepository.AsQueryable()
                                .Where(m => m.EventId == FirstEventId).Select(m => m.Person)
                                .Where(p => p.PersonEmails.Count(pe => pe.Email.EmailTypeId == 3) == 0)
                                .ToList().OrderBy(m => m.LastName).Skip((pagingInfo.CurrentPage - 1) * pagingInfo.PageSize)
                                .Take(pagingInfo.PageSize).ToList();
                        Mailer.Participants = PersonList;
                        //Session Select list
                        var selectList = GetSelectedList(item.EmailTypeId.GetValueOrDefault(3)); //invite

                        foreach (var Listitem in Mailer.Participants)
                        {
                            if (selectList.SelectedParticipants.Exists(m => m.Id == Listitem.Id))
                            {
                                Listitem.selected = true;
                            }

                            //Cliff: populating schedule date                            
                            if (Listitem.ScheduledEmailPersons.Count(m => m.ScheduledEmail.Email.Id == Mailer.Email.Id
                                && m.ScheduledEmail.CompletedDate == null
                                && (m.Enabled ?? false)) > 0)
                            {
                                Listitem.ScheduleDate = Listitem.ScheduledEmailPersons
                                    .Where(m => m.ScheduledEmail.Email.Id == Mailer.Email.Id && m.ScheduledEmail.CompletedDate == null && (m.Enabled ?? false))
                                    .Max(m => m.ScheduledEmail.ScheduleDate);
                            }
                            else
                            {
                                Listitem.ScheduleDate = null;
                            }
                        }
                    }
                    else if (item.EmailTypeId == 4) //Reminders
                    {
                        var manualHogan = UnitOfWork.IManual_Hogan_ImportRepository.AsQueryable();
                                                
                        var totalRecords = UnitOfWork.IPersonEventRepository.AsQueryable()
                            .Where(m => m.EventId == FirstEventId)
                            .Where(m => m.Person.PersonEmails.Count(pe => pe.Email.EmailTypeId == 3) > 0)
                            .Where(m => manualHogan.Any(h => (h.Hogan_User_ID == m.Person.Hogan_Id && (h.HPIDate == null || h.HPIDate.Trim() == string.Empty || h.MVPIDate == null || h.MVPIDate.Trim() == string.Empty || h.HDSDate == null || h.HDSDate.Trim() == string.Empty))
                                || manualHogan.Where(t => t.Hogan_User_ID == m.Person.Hogan_Id).Count() < 1))
                            .Select(m => m.Person).Count();

                        PagingInfo pagingInfo;
                        pagingInfo = new PagingInfo { CurrentPage = page.GetValueOrDefault(1), PageCount = 5, TotalRecords = totalRecords };
                        pagingInfo.PageSize = int.Parse(PPI.Core.Web.Infrastructure.Utility.GetCookie("pageSize"));
                        pagingInfo.LastPage = totalRecords / pagingInfo.PageSize;
                        Mailer.PagingInfo = pagingInfo;
                        var PersonList = new List<Person>();
                        if (search != null && search != "")
                            PersonList = UnitOfWork.IPersonEventRepository.AsQueryable()
                                .Where(m => m.EventId == FirstEventId).Select(m => m.Person)
                                .Where(p => p.Hogan_Id == search || p.LastName.ToUpper() == search.ToUpper() || p.PrimaryEmail.ToUpper() == search.ToUpper() || p.AAMCNumber == search)
                                .Where(p => p.PersonEmails.Count(pe => pe.Email.EmailTypeId == 3) > 0)
                                .Where(m => manualHogan.Any(h => (h.Hogan_User_ID == m.Hogan_Id && (h.HPIDate == null || h.HPIDate.Trim() == string.Empty || h.MVPIDate == null || h.MVPIDate.Trim() == string.Empty || h.HDSDate == null || h.HDSDate.Trim() == string.Empty))
                                    || manualHogan.Where(t => t.Hogan_User_ID == m.Hogan_Id).Count() < 1))
                                .ToList().OrderBy(m => m.LastName)
                                .Skip((pagingInfo.CurrentPage - 1) * pagingInfo.PageSize)
                                .Take(pagingInfo.PageSize).ToList();
                        else
                            PersonList = UnitOfWork.IPersonEventRepository.AsQueryable()
                                .Where(m => m.EventId == FirstEventId).Select(m => m.Person)
                                .Where(p => p.PersonEmails.Count(pe => pe.Email.EmailTypeId == 3) > 0)
                                .Where(m => manualHogan.Any(h => (h.Hogan_User_ID == m.Hogan_Id && (h.HPIDate == null || h.HPIDate.Trim() == string.Empty || h.MVPIDate == null || h.MVPIDate.Trim() == string.Empty || h.HDSDate == null || h.HDSDate.Trim() == string.Empty))
                                    || manualHogan.Where(t => t.Hogan_User_ID == m.Hogan_Id).Count() < 1))
                                .ToList().OrderBy(m => m.LastName).Skip((pagingInfo.CurrentPage - 1) * pagingInfo.PageSize)
                                .Take(pagingInfo.PageSize).ToList();
                        Mailer.Participants = PersonList;
                        //Session Select list
                        var selectList = GetSelectedList(item.EmailTypeId.GetValueOrDefault(4)); //reminder

                        foreach (var Listitem in Mailer.Participants)
                        {
                            if (selectList.SelectedParticipants.Exists(m => m.Id == Listitem.Id))
                            {
                                Listitem.selected = true;
                            }
                            //Also Set there LastReminder Date
                            Listitem.LastReminderSent = Listitem.PersonEmails.Where(pe => pe.Email.EmailTypeId == 4).Max(m => m.SentDate).GetValueOrDefault();
                            Listitem.InviteSent = Listitem.PersonEmails.Where(pe => pe.Email.EmailTypeId == 3).Max(m => m.SentDate).GetValueOrDefault();

                            //Cliff: populating schedule date                            
                            if (Listitem.ScheduledEmailPersons.Count(m => m.ScheduledEmail.Email.Id == Mailer.Email.Id
                                && m.ScheduledEmail.CompletedDate == null
                                && (m.Enabled ?? false)) > 0)
                            {
                                Listitem.ScheduleDate = Listitem.ScheduledEmailPersons
                                    .Where(m => m.ScheduledEmail.Email.Id == Mailer.Email.Id && m.ScheduledEmail.CompletedDate == null && (m.Enabled ?? false))
                                    .Max(m => m.ScheduledEmail.ScheduleDate);
                            }
                            else
                            {
                                Listitem.ScheduleDate = null;
                            }
                        }
                    }

                    TabList.Add(Mailer);
                }
            }
            model.Mailer = TabList;


            //populate remaining flags for email operations such as Send, Schedule, Remove
            model.Ops = new EmailOps();
            model.Ops.NothingFlag = (TempData["showNothingConf"] != null ? true : false);
            model.Ops.RemoveFlag = (TempData["showRemoveConf"] != null ? true : false);
            model.Ops.ScheduleFlag = (TempData["showScheduleConf"] != null ? true : false);
            model.Ops.EmailStatsFlag = (TempData["showEmailStats"] != null ? true : false);

            if (model.Ops.EmailStatsFlag)
            {
                model.Ops.EmailsFailed = (int)TempData["numEmailsFailed"];
                model.Ops.EmailsSent = (int)TempData["numEmailsSent"];
            }                        

            return View(model);
        }

        // GET: /Emails/Create

        [Authorize(Roles = "Admin,J3PAdmin")]
        public ActionResult Create()
        {

            ViewBag.EventId = new SelectList(UnitOfWork.IEventRepository.AsQueryable(), "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,SiteCordinator,J3PAdmin")]
        public ActionResult CreateCordinator()
        {
            //TODO relook at this why auto mapper not working
            var Id = int.Parse(this.Request.Params["Emailitem.Id"].ToString());
            var defaultFrom = this.Request.Params["Emailitem.DefaultFrom"].ToString();
            var EventId = int.Parse(this.Request.Params["Emailitem.EventId"].ToString());
            var EmailTypeId = int.Parse(this.Request.Params["Emailitem.EmailTypeId"].ToString());
            var Subject = this.Request.Params["Emailitem.Subject"].ToString();
            var Intro = this.Request.Params["Emailitem.Introduction"].ToString();
            var Closing = this.Request.Params["Emailitem.Closing"].ToString();

            var email = UnitOfWork.IEmailRepository.AsQueryable().FirstOrDefault(m => m.Id == Id);
            if (email != null)
            {
                //update
                email.Closing = Closing;
                email.DefaultFrom = defaultFrom;
                email.Introduction = Intro;
                email.Subject = Subject;
                UnitOfWork.IEmailRepository.Update(email);

            }
            else
            {
                email.EventId = EventId;
                email.EmailTypeId = EmailTypeId;
                email.Closing = Closing;
                email.DefaultFrom = defaultFrom;
                email.Introduction = Intro;
                email.Subject = Subject;
                UnitOfWork.IEmailRepository.Add(email);
            }
            UnitOfWork.Commit();
            return RedirectToAction("Cordinator", new { @eventId = email.EventId });
        }


        // POST: /Emails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,SiteCordinator,J3PAdmin")]
        public ActionResult Create([Bind(Include = "Id,EventId,DefaultFrom,Subject,Introduction,Closing")] Email email)
        {
            if (ModelState.IsValid)
            {
                //db.Emails.Add(email);
                //db.SaveChanges();         
                UnitOfWork.IEmailRepository.Add(email);
                UnitOfWork.Commit();
                return RedirectToAction("Index");
            }

            ViewBag.EventId = new SelectList(UnitOfWork.IEventRepository.AsQueryable(), "Id", "Name", email.EventId);
            return View(email);
        }

        // GET: /Emails/Edit/5
        [Authorize(Roles = "Admin,J3PAdmin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Email email = db.Emails.Find(id);
            var model = UnitOfWork.IEmailRepository.First(m => m.Id == id);
            if (model == null)
            {
                return HttpNotFound();
            }
            ViewBag.EventId = new SelectList(UnitOfWork.IEventRepository.AsQueryable(), "Id", "Name", model.EventId);
            return View(model);
        }

        // POST: /Emails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,J3PAdmin")]
        public ActionResult Edit([Bind(Include = "Id,EventId,EmailTypeId,DefaultFrom,Subject,Introduction,Closing")] Email email)
        {
            if (ModelState.IsValid)
            {
                // db.Entry(email).State = EntityState.Modified;
                //db.SaveChanges();
                UnitOfWork.IEmailRepository.Update(email);
                UnitOfWork.Commit();
                return RedirectToAction("Index");
            }
            ViewBag.EventId = new SelectList(UnitOfWork.IEventRepository.AsQueryable(), "Id", "Name", email.EventId);
            return View(email);
        }

        // GET: /Emails/Delete/5
        [Authorize(Roles = "Admin,J3PAdmin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Email email = db.Emails.Find(id);
            var model = UnitOfWork.IEmailRepository.First(m => m.Id == id);
            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        // POST: /Emails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,J3PAdmin")]
        public ActionResult DeleteConfirmed(int id)
        {
            // Email email = db.Emails.Find(id);
            // db.Emails.Remove(email);
            // db.SaveChanges();
            var model = UnitOfWork.IEmailRepository.First(m => m.Id == id);
            UnitOfWork.IEmailRepository.Delete(model);
            UnitOfWork.Commit();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            //if (disposing)
            //{
            //    db.Dispose();
            //}
            // base.Dispose(disposing);
        }

        [Authorize(Roles = "Admin,J3PAdmin,SiteCordinator")]
        public ActionResult Preview(int? Id)
        {
            if (Id == null || Id < 1)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            var model = UnitOfWork.IPersonEmailRepository.First(m => m.Id == Id);
            if (model == null)
            {
                return HttpNotFound();
            }

            return View(model);
        }
    }
}
