using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace PPI.Core.Web.Controllers
{
    using PPI.Core.Web.Controllers;
    using PPI.Core.Domain.Abstract;
    using PPI.Core.Domain.Entities;
    using PPI.Core.Domain.Concrete;
    using PPI.Core.Web.Infrastructure;
    using PPI.Core.Web.Models;
    using System.Data.Entity.Validation;
    using System.Web.Script.Serialization;
    using System.Configuration;
    using System.IO;
    using System.Text.RegularExpressions;
    [Authorize(Roles = "Admin,J3PAdmin")]
    public class OrderFormsController : BaseController
    {

        //Replace in Scafolding file
        //private CoreContainer db = new CoreContainer();

        public OrderFormsController(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {

        }

        // GET: /OrderForms/
        public ActionResult Index()
        {
            // replace in scafoding file
            //var orderforms = db.OrderForms.Include(o => o.EventType).Include(o => o.Program).Include(o => o.OrderStatu);
            var model = UnitOfWork.IOrderFormRepository.AsQueryable();

            return View(model);
        }

        // GET: /OrderForms/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //replace scafolding
            //OrderForm orderForm = db.OrderForms.Find(id);
            var model = UnitOfWork.IOrderFormRepository.First(m => m.Id == id);
            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        // GET: /OrderForms/Create
        public ActionResult Create()
        {

            var model = new OrderFormCreateViewModel();

            ViewBag.EventTypeId = new SelectList(UnitOfWork.IEventTypeRepository.SelectEventTypeList(1), "Value", "Text");

            ViewBag.ProgramId = new SelectList(UnitOfWork.IProgramRepository.AsQueryable(), "Id", "ProgramName");

            ViewBag.OrderStatusId = new SelectList(UnitOfWork.IOrderStatusRepository.AsQueryable(), "Id", "Name");

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
            ViewBag.Reports = Reports;

            return View(model);
        }

        // POST: /OrderForms/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(OrderFormCreateViewModel orderFormCreateViewModel)
        {
            if (ModelState.IsValid)
            {
                //db.OrderForms.Add(orderForm);
                //db.SaveChanges();            
                if (orderFormCreateViewModel.PracticeReportsIds != null)
                {
                    foreach (var item in orderFormCreateViewModel.PracticeReportsIds)
                    {
                        orderFormCreateViewModel.OrderForm.OrderFormPracticeReports.Add(new OrderFormPracticeReport { OrderFormId = orderFormCreateViewModel.OrderForm.Id, PracticeReportId = int.Parse(item.ToString()) });
                    }
                }
                UnitOfWork.IOrderFormRepository.Add(orderFormCreateViewModel.OrderForm);
                UnitOfWork.Commit();
                return RedirectToAction("Index");
            }

            ViewBag.EventTypeId = new SelectList(UnitOfWork.IEventTypeRepository.SelectEventTypeList(1), "Value", "Text", orderFormCreateViewModel.OrderForm.EventTypeId);
            ViewBag.ProgramId = new SelectList(UnitOfWork.IProgramRepository.AsQueryable(), "Id", "ProgramName", orderFormCreateViewModel.OrderForm.ProgramId);
            ViewBag.OrderStatusId = new SelectList(UnitOfWork.IOrderStatusRepository.AsQueryable(), "Id", "Name", orderFormCreateViewModel.OrderForm.OrderStatusId);
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
            ViewBag.Reports = Reports;
            return View(orderFormCreateViewModel);
        }

        // GET: /OrderForms/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //OrderForm orderForm = db.OrderForms.Find(id);
            var model = new OrderFormEditViewModel();
            model.OrderForm = UnitOfWork.IOrderFormRepository.First(m => m.Id == id);
            if (model == null)
            {
                return HttpNotFound();
            }
            var Reports = new List<CheckBoxModel>();
            foreach (var item in UnitOfWork.IPracticeReportRepository.AsQueryable().Where(m => m.Active == true))
            {
                var checkboxItem = new CheckBoxModel();
                checkboxItem.Selected = UnitOfWork.IOrderFormPracticeReportRepository.AsQueryable().ToList().Exists(m => m.OrderFormId == id && m.PracticeReportId == item.Id);
                checkboxItem.Name = item.ReportTitleResx.ResxValues.FirstOrDefault(m => m.CultureId == 1).Value;
                checkboxItem.Value = item.Id.ToString();
                checkboxItem.Disabled = false;
                Reports.Add(checkboxItem);
            }
            ViewBag.Reports = Reports;
            ViewBag.EventTypeId = new SelectList(UnitOfWork.IEventTypeRepository.SelectEventTypeList(1), "Value", "Text", model.OrderForm.EventTypeId);
            ViewBag.ProgramId = new SelectList(UnitOfWork.IProgramRepository.AsQueryable(), "Id", "ProgramName", model.OrderForm.ProgramId);
            ViewBag.OrderStatusId = new SelectList(UnitOfWork.IOrderStatusRepository.AsQueryable(), "Id", "Name", model.OrderForm.OrderStatusId);
            return View(model);
        }

        // POST: /OrderForms/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(OrderFormEditViewModel OrderFormEditViewModel)
        {
            if (ModelState.IsValid)
            {
                // db.Entry(orderForm).State = EntityState.Modified;
                //db.SaveChanges();
                UnitOfWork.IOrderFormRepository.Update(OrderFormEditViewModel.OrderForm);
                var deleteList = UnitOfWork.IOrderFormPracticeReportRepository.AsQueryable().Where(m => m.OrderFormId == OrderFormEditViewModel.OrderForm.Id).ToList();
                UnitOfWork.IOrderFormPracticeReportRepository.Delete(deleteList);
                if (OrderFormEditViewModel.PracticeReportsIds != null)
                {
                    foreach (var item in OrderFormEditViewModel.PracticeReportsIds)
                    {
                        OrderFormEditViewModel.OrderForm.OrderFormPracticeReports.Add(new OrderFormPracticeReport() { OrderFormId = OrderFormEditViewModel.OrderForm.Id, PracticeReportId = int.Parse(item.ToString()) });
                    }
                }
                UnitOfWork.Commit();
                return RedirectToAction("Index");
            }
            ViewBag.EventTypeId = new SelectList(UnitOfWork.IEventTypeRepository.SelectEventTypeList(1), "Value", "Text", OrderFormEditViewModel.OrderForm.EventTypeId);
            ViewBag.ProgramId = new SelectList(UnitOfWork.IProgramRepository.AsQueryable(), "Id", "ProgramName", OrderFormEditViewModel.OrderForm.ProgramId);
            ViewBag.OrderStatusId = new SelectList(UnitOfWork.IOrderStatusRepository.AsQueryable(), "Id", "Name", OrderFormEditViewModel.OrderForm.OrderStatusId);
            return View(OrderFormEditViewModel);
        }

        // GET: /OrderForms/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //OrderForm orderForm = db.OrderForms.Find(id);
            var model = UnitOfWork.IOrderFormRepository.First(m => m.Id == id);
            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        // POST: /OrderForms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            // OrderForm orderForm = db.OrderForms.Find(id);
            // db.OrderForms.Remove(orderForm);
            // db.SaveChanges();
            var model = UnitOfWork.IOrderFormRepository.First(m => m.Id == id);
            UnitOfWork.IOrderFormRepository.Delete(model);
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

        /// <summary>
        /// 
        /// </summary>
        /// <returns>ActionResult</returns>
        [HttpGet]
        public ActionResult OESSetup(int? ID)
        {
            var model = new OESSetupViewModel();
            model.Setup = new OESSetup();

            if (ID > 0)
            {
                model = GetOESSetUp(ID);

                var Reports = new List<CheckBoxModel>();
                foreach (var item in UnitOfWork.IPracticeReportRepository.AsQueryable().Where(m => m.Active == true))
                {
                    var checkboxItem = new CheckBoxModel();
                    checkboxItem.Selected = UnitOfWork.IOESPracticeReport.AsQueryable().ToList().Exists(m => m.OESSetup.Id == ID && m.PracticeReportId == item.Id);
                    checkboxItem.Name = item.ReportTitleResx.ResxValues.FirstOrDefault(m => m.CultureId == 1).Value;
                    checkboxItem.Value = item.Id.ToString();
                    checkboxItem.Disabled = false;
                    Reports.Add(checkboxItem);
                }
                model.AvailibleReports = Reports;
            }
            else
            {
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
            }
            model.UserName = User.Identity.Name;
            ViewBag.OrganizationId = new SelectList(GetOrganizationList(), "Value", "Text");

            ViewBag.SiteId = new SelectList(GetDepartmentList(), "Value", "Text");


            ViewBag.ProgramId = new SelectList(GetSpecialtyList(), "Value", "Text");

            ViewBag.Billable = new SelectList(GetYesNoList(), "Value", "Text");

            ViewBag.NotBillableReasonId = new SelectList(GetEventNotBillableReasonList(), "Value", "Text");

            ViewBag.VGSpecialtyId = new SelectList(GetEventVGSpecialityList(), "Value", "Text");

            ViewBag.EventTypeId = new SelectList(UnitOfWork.IEventTypeRepository.SelectEventTypeList(1), "Value", "Text");

            ViewBag.SurveyTypeId = new SelectList(GetSurveyTypeList(), "Value", "Text");

            ViewBag.CompositeRequired = new SelectList(GetYesNoList(), "Value", "Text");

            ViewBag.JETRequired = new SelectList(GetYesNoList(), "Value", "Text");

            return View(model);
        }

        /// <summary>
        /// Saves the OES setup
        /// </summary>
        /// <param name="model"></param>
        /// <returns>ActionResult</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult OESSetup(OESSetupViewModel model)
        {            
            string NameRequiredMsg = "Specify Event Name.";
            string DropDownRequiredMsg = "Select a value..";            
            string DescriptionRequiredMsg = " Specify Event Description";
            string DefaultEmailRequiredMsg = "Specify Default Email Address.";
            string BDOpportunityRequiredMsg = "Specify Opportunity.";
            string NoOfParticipantsRequiredMsg = "Specify Total Number of Participants.";
            string ReportsRequiredMsg = "Select at least one report for the event.";
            string DepartmentNotMapped = "Specified Speciality does not match the selected Department.";
            string emailInvalid = "Specify a valid Email Address.";

            if (string.IsNullOrEmpty(model.Setup.Name) || model.Setup.Name.Trim().Length <= 0)
            {
                ModelState.AddModelError("EventNameMessage", NameRequiredMsg);
            }
            if (model.Setup.OrganizationId == null)
            {
                ModelState.AddModelError("OrganizationMessage", DropDownRequiredMsg);
            }
            if (model.Setup.SiteId == null)
            {
                ModelState.AddModelError("DepartmentMessage", DropDownRequiredMsg);
            }
            if (model.Setup.ProgramId == null)
            {
                ModelState.AddModelError("SpecialityMessage", DropDownRequiredMsg);
            }
            if (model.Setup.ProgramId != null && model.Setup.SiteId != null)
            {
                //Assign programsites to the new event
                var ProgramSiteID = UnitOfWork.IProgramSiteRepository.AsQueryable().Where(x => x.ProgramId == model.Setup.ProgramId && x.SiteId == model.Setup.SiteId);

                if (ProgramSiteID.FirstOrDefault() == null)
                    ModelState.AddModelError("SpecialityMessage", DepartmentNotMapped);
            }
            if (string.IsNullOrEmpty(model.Setup.Description) || model.Setup.Description.Trim().Length <= 0)
            {
                ModelState.AddModelError("EventDescriptionMessage", DescriptionRequiredMsg);
            }
            if (string.IsNullOrEmpty(model.Setup.DefaultEmail) || model.Setup.DefaultEmail.Trim().Length <= 0)
            {
                ModelState.AddModelError("EmailAddressMessage", DefaultEmailRequiredMsg);
            }
            else
            {
                string emailRegex = @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z";
                Regex re = new Regex(emailRegex);
                if (!re.IsMatch(model.Setup.DefaultEmail))
                {
                    ModelState.AddModelError("EmailAddressMessage", emailInvalid);
                }
            }
            if (model.Setup.Billable == null)
            {
                ModelState.AddModelError("BillableEventMessage", DropDownRequiredMsg);
            }
            else
            {
                if (!model.Setup.Billable.Value)
                {
                    if (model.Setup.NotBillableReasonId == null)
                    {
                        ModelState.AddModelError("ReasonMessage", DropDownRequiredMsg);
                    }
                    else
                    {
                        var EventNotBillableReason = UnitOfWork.IEventNotBillableReason.AsQueryable().Where(x => x.Id == model.Setup.NotBillableReasonId).FirstOrDefault();
                        if (EventNotBillableReason.Name.ToUpper() == "VALIDITY GENERALIZATION")
                        {
                            model.Setup.BDOpportunity = null;
                            if (model.Setup.VGSpecialtyId == null)
                            {
                                ModelState.AddModelError("VGSpecialityMessage", DropDownRequiredMsg);
                            }
                        }
                        else if (EventNotBillableReason.Name.ToUpper() == "BUSINESS DEVELOPMENT")
                        {
                            model.Setup.VGSpecialtyId = null;
                            if (string.IsNullOrEmpty(model.Setup.BDOpportunity) || model.Setup.BDOpportunity.Trim().Length <= 0)
                            {
                                ModelState.AddModelError("BDOpportunityMessage", BDOpportunityRequiredMsg);
                            }
                        }
                    }
                }
            }
            if (model.Setup.EventTypeId == 0)
            {
                ModelState.AddModelError("EventTypeMessage", DropDownRequiredMsg);
            }
            if (model.Setup.SurveyTypeId == null)
            {
                ModelState.AddModelError("SurveyTypeMessage", DropDownRequiredMsg);
            }
            else
            {
                var SurveyType = UnitOfWork.ISurveyType.AsQueryable().Where(x => x.Id == model.Setup.SurveyTypeId).FirstOrDefault();
                if (SurveyType.Name != "J3P")
                {
                    if (model.PracticeReportsIds == null)
                    {
                        ModelState.AddModelError("ReportsMessage", ReportsRequiredMsg);
                    }
                }
                else
                {
                    model.PracticeReportsIds = null;
                }
            }
            if (model.Setup.CompositeRequired == null)
            {
                ModelState.AddModelError("CompositeRequiredMessage", DropDownRequiredMsg);
            }
            if (model.Setup.JETRequired == null)
            {
                ModelState.AddModelError("JETREquiredMessage", DropDownRequiredMsg);
            }
            if (model.Setup.NoOfParticipants == null)
            {
                ModelState.AddModelError("NoofParticipantsMessage", NoOfParticipantsRequiredMsg);
            }

            if (ModelState.IsValid)
            {
                //Insert / Update OES Setup.
                SaveOESSetup(model);
                // Go to next Tab.
                return RedirectToAction("OESSchedule", new { ID = model.Setup.Id });
            }

            ViewBag.OrganizationId = new SelectList(GetOrganizationList(), "Value", "Text");

            ViewBag.SiteId = new SelectList(GetDepartmentList(), "Value", "Text");

            ViewBag.ProgramId = new SelectList(GetSpecialtyList(), "Value", "Text");

            ViewBag.Billable = new SelectList(GetYesNoList(), "Value", "Text");

            ViewBag.NotBillableReasonId = new SelectList(GetEventNotBillableReasonList(), "Value", "Text");

            ViewBag.VGSpecialtyId = new SelectList(GetEventVGSpecialityList(), "Value", "Text");

            ViewBag.EventTypeId = new SelectList(UnitOfWork.IEventTypeRepository.SelectEventTypeList(1), "Value", "Text");

            ViewBag.SurveyTypeId = new SelectList(GetSurveyTypeList(), "Value", "Text");

            ViewBag.CompositeRequired = new SelectList(GetYesNoList(), "Value", "Text");

            ViewBag.JETRequired = new SelectList(GetYesNoList(), "Value", "Text");

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

            return View("OESSetup", model);
        }

        /// <summary>
        /// Get OES Schedule.
        /// </summary>
        /// <param name="ID"></param>
        /// <returns>ActionResult</returns>
        [HttpGet]
        public ActionResult OESSchedule(int? ID)
        {
            var model = new OESScheduleViewModel();

            var result = UnitOfWork.IOESSetup.AsQueryable().Where(x => x.Id == ID).FirstOrDefault();

            if (ID > 0)
                model = GetOESSchedule(ID);

            return View(model);
        }

        /// <summary>
        /// Save OES Schudule.
        /// </summary>
        /// <param name="model"></param>
        /// <returns>ActionResult</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult OESSchedule(OESScheduleViewModel model)
        {            
            string ReviewDateRequiredMsg = "Specify Event/Review Date.";
            string EndDateRequiredMsg = "Specify End Date.";
            string StartDateRequiredMsg = "Specify Start Date.";
            string JETNeedByDateRequiredMsg = "Specify JET Need By Date.";
            string CompositeNeedByDateRequiredMsg = "Specify Composite Need By Date.";
            string ReviewDateValidationsMsg = "Event / Review Date must be later than End Date.";
            string StartDateValidationMsg = "Event Start Date should be before the Event/Review Date and the End Date.";
            string EndDateValidationMsg = "Event End Date should not be before the Start Date and after the Event Date.";
            string JETNeedByDateValidationMsg = "JET Need By Date should be later than the End Date.";
            string CompositeNeedByDateValidationMsg = "COMPOSITE Need By Date should be later than the End Date.";

            if (model.ReviewDate == null)
            {
                ModelState.AddModelError("ReviewDateMessage", ReviewDateRequiredMsg);
            }
            else
            {
                if (model.EndDate != null)
                {
                    if (DateTime.ParseExact(model.ReviewDate, "MM-dd-yyyy", null) <= DateTime.ParseExact(model.EndDate, "MM-dd-yyyy", null))
                    {
                        ModelState.AddModelError("ReviewDateMessage", ReviewDateValidationsMsg);
                    }
                }
            }

            if (model.StartDate == null)
            {
                ModelState.AddModelError("StartDateMessage", StartDateRequiredMsg);
            }
            else
            {
                if (model.ReviewDate != null && model.EndDate != null)
                {
                    if (DateTime.ParseExact(model.StartDate, "MM-dd-yyyy", null) >= DateTime.ParseExact(model.ReviewDate, "MM-dd-yyyy", null))
                    {
                        ModelState.AddModelError("StartDateMessage", StartDateValidationMsg);
                    }
                    if (DateTime.ParseExact(model.StartDate, "MM-dd-yyyy", null) >= DateTime.ParseExact(model.EndDate, "MM-dd-yyyy", null))
                    {
                        ModelState.AddModelError("StartDateMessage", StartDateValidationMsg);
                    }
                }
            }

            if (model.EndDate == null)
            {
                ModelState.AddModelError("EndDateMessage", EndDateRequiredMsg);
            }
            else
            {
                if (model.StartDate != null && model.ReviewDate != null)
                {
                    if ((DateTime.ParseExact(model.EndDate, "MM-dd-yyyy", null) <= DateTime.ParseExact(model.StartDate, "MM-dd-yyyy", null)) ||
                        (DateTime.ParseExact(model.EndDate, "MM-dd-yyyy", null) >= DateTime.ParseExact(model.ReviewDate, "MM-dd-yyyy", null)))
                    {
                        ModelState.AddModelError("EndDateMessage", EndDateValidationMsg);
                    }
                }
            }

            if (model.JetRequired)
            {
                if (model.JETNeedByDate == null)
                {
                    ModelState.AddModelError("JETNeedByDateMessage", JETNeedByDateRequiredMsg);
                }
                else
                {
                    if (model.EndDate != null)
                    {
                        if (DateTime.ParseExact(model.JETNeedByDate, "MM-dd-yyyy", null) <= DateTime.ParseExact(model.EndDate, "MM-dd-yyyy", null))
                        {
                            ModelState.AddModelError("JETNeedByDateMessage", JETNeedByDateValidationMsg);
                        }
                    }
                }
            }

            if (model.CompositeRequired)
            {
                if (model.CompositeNeedByDate == null)
                {
                    ModelState.AddModelError("CompositeNeedByDateMessage", CompositeNeedByDateRequiredMsg);
                }
                else
                {
                    if (model.EndDate != null)
                    {
                        if (DateTime.ParseExact(model.CompositeNeedByDate, "MM-dd-yyyy", null) <= DateTime.ParseExact(model.EndDate, "MM-dd-yyyy", null))
                        {
                            ModelState.AddModelError("CompositeNeedByDateMessage", CompositeNeedByDateValidationMsg);
                        }
                    }
                }
            }

            if (ModelState.IsValid)
            {
                SaveOESSchedule(model);

                var id = model.OESSetupId;
                return RedirectToAction("OESReview", new { ID = id });
            }

            return View("OESSchedule", model);
        }

        /// <summary>
        /// GET OES Review.
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult OESReview(int? ID)
        {
            OESReviewViewModel model = null;
            model = GetOESReview(ID);

            return View(model);
        }

        /// <summary>
        /// Complete the OES event request.
        /// </summary>
        /// <param name="model"></param>
        /// <returns>ActionResult</returns>      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult OESReview(OESReviewViewModel model)
        {
            var setup = UnitOfWork.IOESSetup.AsQueryable().Where(x => x.Id == model.OESSetupID).FirstOrDefault();
            var schedule = UnitOfWork.IOESSchedule.AsQueryable().Where(x => x.OESSetupId == model.OESSetupID).FirstOrDefault();
            bool isSucess = false;
            if (User.IsInRole("Admin"))
            {
                SaveEvent(setup, schedule);
                isSucess = EventApprovalEmail(model);
            }
            else
            {
                if (setup.Billable.Value == true)
                {
                    SaveEvent(setup, schedule);
                    isSucess = EventApprovalEmail(model);
                }
                else if (setup.Billable.Value == false)
                {
                    if (setup.EventNotBillableReason != null && setup.EventNotBillableReason.Name.ToUpper() == "VALIDITY GENERALIZATION")
                    {
                        SaveEvent(setup, schedule);
                        isSucess = EventApprovalEmail(model);
                    }
                }
            }


            return View("OESComplete", setup);
        }

        public bool EventApprovalEmail(OESReviewViewModel model)
        {

            var GOEmail = new PPI.Core.Web.Infrastructure.EmailService();

            var Emails = new List<EmailModel>();
            var personEmail = new PersonEmail();


            var Email = new EmailModel();
            var EmailTemplate = new EmailTemplateModel();
            EmailTemplate.subject = string.Format(ConfigurationManager.AppSettings["OESEventMailSubject"], model.Setup.Name);
            //EmailTemplate.closing = mailinfo.Closing;
            //EmailTemplate.introduction = mailinfo.Introduction;
            //EmailTemplate.UserName = person.FirstName;

            Email.to = model.Setup.DefaultEmail;
            // Email.from = mailinfo.DefaultFrom;
            Email.subject = string.Format(ConfigurationManager.AppSettings["OESEventMailSubject"] + "{0}", model.Setup.Name);

            Email.body = RenderPartialToString("_PartialEmailEventCompleteFormat", EmailTemplate);

            Emails.Add(Email);


            bool isSuccess = GOEmail.SendEmailAsync(Emails, false);
            return isSuccess;
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



        /// <summary>
        /// Save OES setup requests to event, If Billable is true or billable is false and EventNotBillableReason is 'Validity Generalization'
        /// </summary>
        /// <param name="setup"></param>
        /// <param name="schedule"></param>
        protected void SaveEvent(OESSetup setup, OESSchedule schedule)
        {
            try
            {
                Event newEvent = new Event();

                var invite = new Email();
                var reminder = new Email();
                var Reports = new Email();
                var assessmentsComplete = new Email();

                newEvent.Name = setup.Name;
                newEvent.Billable = setup.Billable;
                newEvent.CreateDate = setup.CreateDate;
                newEvent.Description = setup.Description;
                newEvent.StartDate = schedule.StartDate;
                newEvent.EndDate = schedule.EndDate;
                newEvent.ReviewDate = schedule.ReviewDate;
                newEvent.EventTypeId = setup.EventTypeId;
                newEvent.SurveyTypeId = setup.SurveyTypeId;
                newEvent.EventStatusId = 3;

                //Assign programsites to the new event
                newEvent.ProgramSitesId = setup.Program.ProgramSites.Where(x => x.ProgramId == setup.ProgramId && x.SiteId == setup.SiteId).FirstOrDefault().Id;

                //Assign selected reports to new event
                foreach (var item in setup.OESPracticeReport)
                {
                    newEvent.EventPracticeReports.Add(new EventPracticeReport() { EventId = newEvent.Id, PracticeReportId = item.PracticeReportId });
                }

                Reports.EventId = newEvent.Id;
                Reports.EmailTypeId = 6; //Reports
                Reports.DefaultFrom = setup.DefaultEmail;

                reminder.EventId = newEvent.Id;
                reminder.EmailTypeId = 4; //Reminder
                reminder.DefaultFrom = setup.DefaultEmail;

                invite.DefaultFrom = setup.DefaultEmail;
                invite.EmailTypeId = 3; //Invite
                invite.EventId = newEvent.Id;

                assessmentsComplete.EventId = newEvent.Id;
                assessmentsComplete.EmailTypeId = 7; //Assessments Complete
                assessmentsComplete.DefaultFrom = setup.DefaultEmail;

                newEvent.Emails.Add(invite);
                newEvent.Emails.Add(reminder);
                newEvent.Emails.Add(Reports);
                newEvent.Emails.Add(assessmentsComplete);

                UnitOfWork.IEventRepository.Add(newEvent);

                //Update the OESSetup order to complete , once the evenbt is created.
                setup.OrderStatusId = 2;
                UnitOfWork.IOESSetup.Update(setup);
                UnitOfWork.IEventRepository.Add(newEvent);
                UnitOfWork.Commit();
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        string error = validationError.PropertyName + validationError.ErrorMessage;
                    }
                }
            }
        }

        /// <summary>
        /// Organization Dropdown List.
        /// </summary>
        /// <returns>List<SelectListItem></returns>
        protected List<SelectListItem> GetOrganizationList()
        {
            var SelectOrganizationId = new List<SelectListItem>();
            foreach (var item in UnitOfWork.IOrganizationRepository.AsQueryable())
            {
                var selectItem = new SelectListItem();
                selectItem.Text = item.OrganizationName;
                selectItem.Value = item.Id.ToString();
                SelectOrganizationId.Add(selectItem);
            }
            return SelectOrganizationId;
        }

        /// <summary>
        /// Department Dropdown List
        /// </summary>
        /// <returns>List<SelectListItem></returns>
        protected List<SelectListItem> GetDepartmentList()
        {
            var SelecDepartmentId = new List<SelectListItem>();
            //foreach (var item in UnitOfWork.ISiteRepository.AsQueryable())
            //{
                //var selectItem = new SelectListItem();
                //selectItem.Text = item.SiteName;
                //selectItem.Value = item.Id.ToString();
                //SelecDepartmentId.Add(selectItem);
            //}
            return SelecDepartmentId;
        }

        /// <summary>
        /// Specialty Dropdown List
        /// </summary>
        /// <returns>List<SelectListItem></returns>
        protected List<SelectListItem> GetSpecialtyList()
        {
            var SelectSpecialtyId = new List<SelectListItem>();
            foreach (var item in UnitOfWork.IProgramRepository.AsQueryable())
            {
                var selectItem = new SelectListItem();
                selectItem.Text = item.ProgramName;
                selectItem.Value = item.Id.ToString();
                SelectSpecialtyId.Add(selectItem);
            }
            return SelectSpecialtyId;
        }

        /// <summary>
        /// YesNo Dropdown List
        /// </summary>
        /// <returns>List<SelectListItem></returns>
        protected List<SelectListItem> GetYesNoList()
        {
            List<SelectListItem> yesnoList = new List<SelectListItem>();

            yesnoList.Add(new SelectListItem() { Text = "Yes", Value = "true", Selected = false });
            yesnoList.Add(new SelectListItem() { Text = "No", Value = "false", Selected = false });

            return yesnoList;
        }

        /// <summary>
        /// EventNotBillableReason Dropdown List
        /// </summary>
        /// <returns>List<SelectListItem></returns>
        protected List<SelectListItem> GetEventNotBillableReasonList()
        {
            var SelectEventNotBillableReasonId = new List<SelectListItem>();
            foreach (var item in UnitOfWork.IEventNotBillableReason.AsQueryable())
            {
                var selectItem = new SelectListItem();
                selectItem.Text = item.Name;
                selectItem.Value = item.Id.ToString();
                SelectEventNotBillableReasonId.Add(selectItem);
            }
            return SelectEventNotBillableReasonId;
        }

        /// <summary>
        /// VGSpeciality Dropdown List
        /// </summary>
        /// <returns>List<SelectListItem></returns>
        protected List<SelectListItem> GetEventVGSpecialityList()
        {
            var SelectEventVGSpecialityId = new List<SelectListItem>();
            foreach (var item in UnitOfWork.IEventVGSpecialty.AsQueryable())
            {
                var selectItem = new SelectListItem();
                selectItem.Text = item.Name;
                selectItem.Value = item.Id.ToString();
                SelectEventVGSpecialityId.Add(selectItem);
            }
            return SelectEventVGSpecialityId;
        }

        /// <summary>
        /// SurveyType drop down List
        /// </summary>
        /// <returns>List</returns>
        protected List<SelectListItem> GetSurveyTypeList()
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

        /// <summary>
        /// Get OES SetUp
        /// </summary>
        /// <param name="OESSetupID"></param>
        /// <returns>OESSetupViewModel</returns>
        protected OESSetupViewModel GetOESSetUp(int? OESSetupID)
        {
            OESSetupViewModel setupModel = new OESSetupViewModel();
            setupModel.Setup = new Domain.Entities.OESSetup();

            setupModel.Setup = UnitOfWork.IOESSetup.AsQueryable().Where(x => x.Id == OESSetupID).FirstOrDefault();

            return setupModel;
        }

        /// <summary>
        /// Insert / Updates the OES Setup details
        /// </summary>
        /// <param name="Model"></param>
        protected void SaveOESSetup(OESSetupViewModel Model)
        {
            if (Model.Setup.Id > 0)
            {
                var Result = UnitOfWork.IOESSetup.AsQueryable().Where(x => x.Id == Model.Setup.Id).FirstOrDefault();
                if (Result != null)
                {
                    Result.Name = Model.Setup.Name;
                    Result.OrganizationId = Model.Setup.OrganizationId;
                    Result.SiteId = Model.Setup.SiteId;
                    Result.ProgramId = Model.Setup.ProgramId;
                    Result.Description = Model.Setup.Description;
                    Result.AspNetUserId = User.Identity.GetUserId();
                    Result.DefaultEmail = Model.Setup.DefaultEmail;
                    Result.Billable = Model.Setup.Billable;
                    Result.NotBillableReasonId = Model.Setup.NotBillableReasonId;
                    Result.VGSpecialtyId = Model.Setup.VGSpecialtyId;
                    Result.BDOpportunity = Model.Setup.BDOpportunity;
                    Result.EventTypeId = Model.Setup.EventTypeId;
                    Result.CompositeRequired = Model.Setup.CompositeRequired;
                    Result.JETRequired = Model.Setup.JETRequired;
                    Result.NoOfParticipants = Model.Setup.NoOfParticipants;
                    Result.SurveyTypeId = Model.Setup.SurveyTypeId;

                    var OESPracticeReport = UnitOfWork.IOESPracticeReport.AsQueryable().Where(x => x.OESSetupId == Model.Setup.Id);
                    UnitOfWork.IOESPracticeReport.Delete(OESPracticeReport);

                    if (Model.PracticeReportsIds != null)
                    {
                        foreach (var item in Model.PracticeReportsIds)
                        {
                            Result.OESPracticeReport.Add(new OESPracticeReport { OESSetupId = Model.Setup.Id, PracticeReportId = int.Parse(item.ToString()) });
                        }
                    }

                    UnitOfWork.IOESSetup.Update(Result);
                    UnitOfWork.Commit();
                }
            }
            else
            {
                if (Model.PracticeReportsIds != null)
                {
                    foreach (var item in Model.PracticeReportsIds)
                    {
                        Model.Setup.OESPracticeReport.Add(new OESPracticeReport { OESSetupId = Model.Setup.Id, PracticeReportId = int.Parse(item.ToString()) });
                    }
                }
                Model.Setup.AspNetUserId = User.Identity.GetUserId();
                Model.Setup.OrderStatusId = 1;
                Model.Setup.CreateDate = DateTime.Now;
                UnitOfWork.IOESSetup.Add(Model.Setup);
                UnitOfWork.Commit();
            }
        }

        /// <summary>
        /// Get OES Schedule
        /// </summary>
        /// <param name="ID"></param>
        /// <returns>OESScheduleViewModel</returns>
        protected OESScheduleViewModel GetOESSchedule(int? ID)
        {
            OESScheduleViewModel scheduleModel = new OESScheduleViewModel();
            var result = UnitOfWork.IOESSchedule.AsQueryable().Where(x => x.OESSetupId == ID).FirstOrDefault();

            if (result != null)
            {
                scheduleModel.ReviewDate = result.ReviewDate.HasValue ? result.ReviewDate.Value.ToString("MM-dd-yyyy") : string.Empty;
                scheduleModel.StartDate = result.StartDate.HasValue ? result.StartDate.Value.ToString("MM-dd-yyyy") : string.Empty;
                scheduleModel.EndDate = result.EndDate.HasValue ? result.EndDate.Value.ToString("MM-dd-yyyy") : string.Empty;
                scheduleModel.JETNeedByDate = result.JETNeedByDate.HasValue ? result.JETNeedByDate.Value.ToString("MM-dd-yyyy") : string.Empty;
                scheduleModel.CompositeNeedByDate = result.CompositeNeedByDate.HasValue ? result.CompositeNeedByDate.Value.ToString("MM-dd-yyyy") : string.Empty;
                scheduleModel.Id = result.Id;
                scheduleModel.OESSetupId = result.OESSetupId;
            }

            var oesSetupResult = UnitOfWork.IOESSetup.AsQueryable().Where(x => x.Id == ID).FirstOrDefault();
            if (oesSetupResult != null)
            {
                scheduleModel.JetRequired = oesSetupResult.JETRequired.HasValue ? oesSetupResult.JETRequired.Value : false;
                scheduleModel.CompositeRequired = oesSetupResult.CompositeRequired.HasValue ? oesSetupResult.CompositeRequired.Value : false;
                scheduleModel.OESSetupId = oesSetupResult.Id;
            }

            return scheduleModel;
        }

        /// <summary>
        /// Insert / Update OES Schedule
        /// </summary>
        /// <param name="model"></param>
        protected void SaveOESSchedule(OESScheduleViewModel model)
        {
            var result = UnitOfWork.IOESSchedule.AsQueryable().Where(x => x.OESSetupId == model.OESSetupId).FirstOrDefault();

            if (result != null)
            {
                result.ReviewDate = DateTime.ParseExact(model.ReviewDate, "MM-dd-yyyy", null);
                result.StartDate = DateTime.ParseExact(model.StartDate, "MM-dd-yyyy", null);
                result.EndDate = DateTime.ParseExact(model.EndDate, "MM-dd-yyyy", null);

                if (model.JETNeedByDate != null)
                    result.JETNeedByDate = DateTime.ParseExact(model.JETNeedByDate, "MM-dd-yyyy", null);

                if (model.CompositeNeedByDate != null)
                    result.CompositeNeedByDate = DateTime.ParseExact(model.CompositeNeedByDate, "MM-dd-yyyy", null);

                result.OESSetupId = model.OESSetupId;

                UnitOfWork.IOESSchedule.Update(result);
                UnitOfWork.Commit();
            }
            else
            {
                OESSchedule schedule = new Domain.Entities.OESSchedule();
                schedule.ReviewDate = DateTime.ParseExact(model.ReviewDate, "MM-dd-yyyy", null);
                schedule.StartDate = DateTime.ParseExact(model.StartDate, "MM-dd-yyyy", null);
                schedule.EndDate = DateTime.ParseExact(model.EndDate, "MM-dd-yyyy", null);

                if (model.JETNeedByDate != null)
                    schedule.JETNeedByDate = DateTime.ParseExact(model.JETNeedByDate, "MM-dd-yyyy", null);

                if (model.CompositeNeedByDate != null)
                    schedule.CompositeNeedByDate = DateTime.ParseExact(model.CompositeNeedByDate, "MM-dd-yyyy", null);

                schedule.OESSetupId = model.OESSetupId;

                UnitOfWork.IOESSchedule.Add(schedule);
                UnitOfWork.Commit();
            }
        }

        /// <summary>
        /// Get OES Review
        /// </summary>
        /// <param name="ID"></param>
        /// <returns>OESReviewViewModel</returns>
        protected OESReviewViewModel GetOESReview(int? ID)
        {
            var model = new OESReviewViewModel();
            model.Setup = new OESSetup { };
            model.OESSetupID = ID.Value;

            model.Setup = UnitOfWork.IOESSetup.AsQueryable().Where(x => x.Id == ID).FirstOrDefault();

            foreach (var item in UnitOfWork.IPracticeReportRepository.AsQueryable().Where(m => m.Active == true))
            {
                var selected = UnitOfWork.IOESPracticeReport.AsQueryable().ToList().Exists(m => m.OESSetup.Id == ID && m.PracticeReportId == item.Id);
                if (selected)
                    model.ReportsSelected += item.ReportTitleResx.ResxValues.FirstOrDefault(m => m.CultureId == 1 && m.ResxId == item.ReportTitleResxId).Value + ",";
            }
            if (model.ReportsSelected != null)
                model.ReportsSelected = model.ReportsSelected.TrimEnd(',');

            var result = UnitOfWork.IOESSchedule.AsQueryable().Where(x => x.OESSetupId == ID).FirstOrDefault();
            var departmentdetails = UnitOfWork.ISiteRepository.First(m => m.Id == model.Setup.SiteId);
            if (result != null)
            {
                model.ReviewDate = result.ReviewDate.HasValue ? result.ReviewDate.Value.ToString("MM-dd-yyyy") : string.Empty;
                model.StartDate = result.StartDate.HasValue ? result.StartDate.Value.ToString("MM-dd-yyyy") : string.Empty;
                model.EndDate = result.EndDate.HasValue ? result.EndDate.Value.ToString("MM-dd-yyyy") : string.Empty;
                model.JETNeedByDate = result.JETNeedByDate.HasValue ? result.JETNeedByDate.Value.ToString("MM-dd-yyyy") : string.Empty;
                model.CompositeNeedByDate = result.CompositeNeedByDate.HasValue ? result.CompositeNeedByDate.Value.ToString("MM-dd-yyyy") : string.Empty;
                model.Id = result.Id;
            }
            if (departmentdetails != null)
            {
                model.Setup.Site.BrandingLogo = departmentdetails.BrandingLogo;
                model.Setup.Site.BrandingColor = departmentdetails.BrandingColor;
                model.Setup.Site.BrandingBackground = departmentdetails.BrandingBackground;
            }

            return model;
        }

        [Authorize(Roles = "Admin")]
        public ActionResult ListEventRequests()
        {
            //Only show events for there SITE
            IEnumerable<OESSetup> model;
            //var EventStatusResxID = UnitOfWork.IResxValueRepository.AsQueryable().Where(m => m.Value.ToUpper() == "INCOMPLETE").FirstOrDefault();
            //var EventStatus = UnitOfWork.IEventStatusRepository.AsQueryable().Where(e => e.NameResxId == EventStatusResxID.ResxId).FirstOrDefault();
            //model = UnitOfWork.IEventRepository.AsQueryable().Where(x => x.EventStatus.Id == EventStatus.Id);

            var OrderStatusID = UnitOfWork.IOrderStatusRepository.AsQueryable().Where(m => m.Name.ToUpper() == "INPROCESS").FirstOrDefault();
            model = UnitOfWork.IOESSetup.AsQueryable().Where(x => x.OrderStatusId == OrderStatusID.Id);

            return View("OrderRequests", model);

        }


        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult AddOrganization(string OrganizationName)
        {
            Organization organization = new Organization();
            organization.OrganizationName = OrganizationName;
            var org = UnitOfWork.IOrganizationRepository.First(x => x.OrganizationName == organization.OrganizationName);
            if (org == null) // update
            {

                UnitOfWork.IOrganizationRepository.Add(organization);
                UnitOfWork.Commit();
                var organizationObject = UnitOfWork.IOrganizationRepository.First(x => x.OrganizationName == organization.OrganizationName);
                return Json(new { success = true, organizationId = organizationObject.Id, organizationName = organizationObject.OrganizationName, successFlag = "true" });
            }
            else
            {
                return Json(new { success = true, organizationId = 0, organizationName = OrganizationName, successFlag = "false" });
            }
        }

        /// <summary>
        /// To Get Departments of a Organization.
        /// </summary>
        /// <param name="organizationId"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetDepartments(long organizationId)
        {
            return Json(new SelectList(UnitOfWork.ISiteRepository.AsQueryable().Where(m => m.OrganizationId == organizationId), "Id", "FriendlyName"));
        }


        [Log]
        [HttpGet]
        [ActionName("LoadAddDepartmentView")]
        public ActionResult LoadAddDepartmentView()
        {
            Site site = new Site();

            return PartialView("_PratialOesAddDepartment", site);
        }


        // POST: /Events/EventAddDepartment/
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult EventAddDepartment()
        {
            if (ModelState.IsValid)
            {
                Site site = new Site();
                site.Id = 0;
                site.OrganizationId = System.Convert.ToInt32(Request.Form["DepartmentOrganizationId"]);
                site.SiteName = Request.Form["SiteName"];
                site.FriendlyName = Request.Form["FriendlyName"];
                site.BrandingColor = Request.Form["BrandingColor"];


                var i = 0;
                foreach (string key in Request.Files)
                {

                    HttpPostedFileBase hpf = Request.Files[i] as HttpPostedFileBase;
                    if (hpf.ContentLength != 0)
                    {
                        if (key == "BrandingLogo")
                        {
                            using (var memoryArray = new System.IO.MemoryStream())
                            {
                                hpf.InputStream.CopyTo(memoryArray);
                                site.BrandingLogo = memoryArray.ToArray();
                                site.BrandingLogoMimeType = hpf.ContentType;
                            }
                        }
                        else if (key == "BrandingBackground")
                        {
                            using (var memoryArray = new System.IO.MemoryStream())
                            {
                                hpf.InputStream.CopyTo(memoryArray);
                                site.BrandingBackground = memoryArray.ToArray();
                                site.BrandingBackgroundMimeType = hpf.ContentType;
                            }
                        }
                    }
                    i++;
                }


                var department = UnitOfWork.ISiteRepository.First(x => x.SiteName == site.SiteName && x.OrganizationId == site.OrganizationId);
                if (department == null) // update
                {
                    UnitOfWork.ISiteRepository.Add(site);
                    UnitOfWork.Commit();
                    var departmentObject = UnitOfWork.ISiteRepository.First(x => x.SiteName == site.SiteName && x.OrganizationId == site.OrganizationId);
                    return Json(new { success = true, departmentId = departmentObject.Id, departmentName = departmentObject.FriendlyName, successFlag = "true" });
                }
                else
                {
                    return Json(new { success = true, departmentId = 0, departmentName = site.FriendlyName, successFlag = "false" });
                }
            }
            return Json(new { success = false, departmentId = 0, departmentName = "", successFlag = "false" });
        }

        //ADD Organization Popup and Department Popup - END

        /// <summary>
        /// 
        /// </summary>
        /// <param name="programId"></param>
        /// <param name="OrganizationId"></param>
        /// <param name="SiteId"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddSpeciality(int? programId, int? OrganizationId, int? SiteId, int? evntOrganizationId, int? evntDepartmentId)
        {
            ViewData["Organizations"] = new SelectList(UnitOfWork.IOrganizationRepository.AsQueryable(), "Id", "OrganizationName", (OrganizationId == null ? 0 : OrganizationId.Value));
            if (OrganizationId != null)
            {
                ViewData["SiteIds"] = new SelectList(UnitOfWork.ISiteRepository.AsQueryable().Where(m => m.OrganizationId == OrganizationId.Value), "Id", "FriendlyName");
            }
            else ViewData["SiteIds"] = new SelectList(UnitOfWork.ISiteRepository.AsQueryable().Where(m => m.OrganizationId == 0), "Id", "FriendlyName");
            var model = new ProgramSiteViewModel();
            //To do : - add speciality
            if (OrganizationId != null)
            {
                int programID = programId.GetValueOrDefault(0);
                var Program = UnitOfWork.IProgramRepository.SingleOrDefault(m => m.Id == programID);
                var tempProgramSite = Program.ProgramSites.Where(m => m.SiteId == SiteId).FirstOrDefault();
                if (tempProgramSite == null)
                {
                    var ProgramSite = new ProgramSite();
                    ProgramSite.ProgramId = programID;
                    ProgramSite.SiteId = (int)SiteId;
                    Program.ProgramSites.Add(ProgramSite);
                    UnitOfWork.IProgramRepository.Update(Program);
                    UnitOfWork.Commit();

                    OrganizationId = null;
                    SiteId = null;
                }
                else
                {
                    TempData["alertMessage"] = "Department already exists. Please select other department.";
                    OrganizationId = null;
                    SiteId = null;
                    //return RedirectToAction("IndexSite", new { programId = programId });
                }
            }
            // get depts
            if (programId != null)
            {
                if (evntOrganizationId != null)
                {
                     ViewData["Organizations"] = new SelectList(UnitOfWork.IOrganizationRepository.AsQueryable(), "Id", "OrganizationName", (evntOrganizationId == null ? 0 : evntOrganizationId.Value));
                    // ViewData["Organizations"] = new SelectList(OrganizationsLists(evntOrganizationId), "Value", "Text");


                  //  ViewBag.SiteOrganizations = new SelectList(OrganizationsLists(null), "Value", "Text");



                    ViewData["SiteIds"] = new SelectList(UnitOfWork.ISiteRepository.AsQueryable().Where(m => m.OrganizationId == evntOrganizationId.Value), "Id", "FriendlyName", (evntDepartmentId == null ? 0 : evntDepartmentId.Value));

                    var Programlist = UnitOfWork.IProgramRepository.SingleOrDefault(m => m.Id == programId);

                    if (Programlist != null)
                    {
                        model.ProgramSites = Programlist.ProgramSites.AsQueryable();
                        model.ProgramID = programId.GetValueOrDefault(0);
                        model.ProgramName = Programlist.ProgramName;
                    }
                    else
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }
                }
                else
                {
                    ViewData["Departments"] = new SelectList(new List<SelectListItem>(), "Value", "Text");
                  //  ViewBag.SiteOrganizations = new SelectList(OrganizationsLists(null), "Value", "Text");
                    ViewData["Organizations"] = new SelectList(UnitOfWork.IOrganizationRepository.AsQueryable(), "Id", "OrganizationName");
                }
            }
          
            return PartialView("_PartialAddSpeciality", model);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ActionName("DeleteSite")]
        public JsonResult DeleteSite(int id)
        {
            ResponseBase response = new ResponseBase();
            response.Code = 0;

            if (id > 0)
            {
                try
                {
                    var mvpiCheck = UnitOfWork.IProgramSiteHoganMVPIRepository.AsQueryable().FirstOrDefault(m => m.ProgramSiteId == id);
                    if (mvpiCheck != null)
                        UnitOfWork.IProgramSiteHoganMVPIRepository.Delete(mvpiCheck);

                    var model = UnitOfWork.IProgramSiteRepository.First(m => m.Id == id);
                    UnitOfWork.IProgramSiteRepository.Delete(model);
                    UnitOfWork.Commit();

                }
                catch (Exception ex)
                {
                    response.Code = 1001;
                }

            }
            return Json(response);
        }
    }
}

