using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using Microsoft.AspNet.Identity;
using Microsoft.Reporting.WebForms;
using System.Diagnostics;
using System.Text.RegularExpressions;
using Ionic.Zip;

namespace PPI.Core.Web.Controllers
{
    using PPI.Core.Web.Models;
    using PPI.Core.Web.Infrastructure;
    using PPI.Core.Domain.Abstract;
    using PPI.Core.Domain.Concrete;
    using PPI.Core.Domain.Entities;

    public class ReportsController : BaseController
    {               
        [Log]
        public ReportsController(IUnitOfWork unitOfWork)
            : base(unitOfWork) { }        
        //
        // GET: /Reports/
        [Log]
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            return View();
        }        
        [Log]
        [Authorize(Roles = "Admin")]
        public ActionResult ManageText()
        {
            var model = new ManageTextViewModel();
            model.Programs = UnitOfWork.IProgramRepository.AsQueryable();
            model.Cultures = UnitOfWork.ICultureRepository.AsQueryable();
            model.CultureId = 1;
                        
            return View(model);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult SelectProgram(int? programId)
        {
            var reportslist = programId.HasValue ? UnitOfWork.IProgramPracticeReportRepository.AsQueryable().Where(m => m.ProgramId == programId) : null;
            List<PracticeReport> reports = new List<PracticeReport>();
            foreach (var item in reportslist)
            {
                reports.Add(item.PracticeReport);
            }

            var model = new ManageTextViewModel();
            //model.Programs = UnitOfWork.IProgramRepository.AsQueryable();
            model.CultureId = 1;
            model.ProgramId = programId.GetValueOrDefault(0);
            model.Reports = reports;
            return PartialView("_PartialPracticeReports", model);
            
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult SelectReport(int? reportId,int? programId,int? cultureId)
        {
            var scaleslist = reportId.HasValue ? UnitOfWork.IPracticeScaleReportRepository.AsQueryable().Where(m => m.PracticeReportId == reportId) : null;
            List<HoganField> scales = new List<HoganField>();
            foreach (var item in scaleslist)
            {
                if (!scales.Exists(m => m.Id == item.PracticeScale.HoganFieldId))
                    scales.Add(item.PracticeScale.HoganField);
            }
            
            var model = new ManageTextViewModel();
            model.ReportId = reportId.GetValueOrDefault(0);
            model.ProgramId = programId.GetValueOrDefault(0);            
            model.CultureId = cultureId.GetValueOrDefault(1); 
            model.Scales = scales;

            
                return PartialView("_PartialPracticeScales", model);
            
        }
        [HttpPost, ValidateInput(false)]        
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult UpdateText(ResxValue textUpdate)
        {
            UnitOfWork.IResxValueRepository.Update(textUpdate);
            UnitOfWork.Commit();
            return PartialView("_PartialManageResxValue", textUpdate);
        }
        [Authorize(Roles = "Admin")]
        public ActionResult ModifyText(int programId,int reportId,int scaleId, int cultureId)
        { 
            var model = new ManageTextViewModel();
            model.ProgramId = programId;
            model.ReportId = reportId;
            model.ScaleId = scaleId;
            model.CultureId = cultureId;
            var ReportTextList = new List<ReportTextModel>();
            //Get all the Scales for the Report
            var ReportData = UnitOfWork.IPracticeScaleReportRepository.Find(m => m.PracticeReportId == model.ReportId && m.PracticeScale.HoganFieldId == model.ScaleId);
            foreach (var item in ReportData)
            {
                ReportTextModel TextModel = new ReportTextModel();
                TextModel.Category = item.PracticeScale.PracticeCategory;
                TextModel.ReportScale = item;
                string egtext = item.PracticeText.TextResx.ResxValues.FirstOrDefault(m => m.Culture.Id == 1).Value; //Hard coded English
                TranslationText TranText = new Models.TranslationText();
                TranText.EnglishText = egtext;
                TranText.Text = item.PracticeText;                
                //Get intro statement pieces
                List<TranslationText> ChildText = new List<TranslationText>();
                if (item.PracticeText.IsIntroduction.GetValueOrDefault(false))
                {
                    foreach (var introitem in item.PracticeText.PracticeTextChildren)
                    {
                        TranslationText ChildTranText = new Models.TranslationText();
                        ChildTranText.Text = introitem;
                        ChildTranText.EnglishText = introitem.TextResx.ResxValues.FirstOrDefault(m => m.Culture.Id == 1).Value; //Hard coded English;
                        ChildText.Add(ChildTranText);
                    }
                }
                TranText.ChildText = ChildText;                
                TextModel.Text = TranText;
                List<TranslationText> AlternativeText = new List<TranslationText>();
                // Get the Alteratives
                foreach (var Altitem in item.PracticeText.PracticeTextOptions)
                {
                    TranslationText AltTranText = new Models.TranslationText();
                    if (Altitem.PracticeTextAlternative != null)
                    {
                        AltTranText.Text = Altitem.PracticeTextAlternative;
                        AltTranText.EnglishText = Altitem.PracticeTextAlternative.TextResx.ResxValues.FirstOrDefault(m => m.Culture.Id == 1).Value; //Hard coded English;                        

                        //Get intro statement pieces
                        List<TranslationText> ChildText2 = new List<TranslationText>();
                        if (Altitem.PracticeText.IsIntroduction.GetValueOrDefault(false))
                        {
                            foreach (var introitem2 in Altitem.PracticeTextAlternative.PracticeTextChildren)
                            {
                                TranslationText ChildTranText2 = new Models.TranslationText();
                                ChildTranText2.Text = introitem2;
                                ChildTranText2.EnglishText = introitem2.TextResx.ResxValues.FirstOrDefault(m => m.Culture.Id == 1).Value; //Hard coded English;
                                ChildText2.Add(ChildTranText2);
                            }
                        }
                        AltTranText.ChildText = ChildText2;
                        AlternativeText.Add(AltTranText);
                    }
                }
                
                TextModel.AlternativeText = AlternativeText;
                ReportTextList.Add(TextModel);
            }
            model.ReportText = ReportTextList;
            

           //var AvailibleCategories = 

            return View(model);
        }
        #region Reports        
        [Log]
        [Authorize(Roles = "Admin")]
        public ActionResult Reports()
        {
            return View();
        }
        [Log]
        [Authorize(Roles = "Admin,SiteCordinator,J3PAdmin")]
        public ActionResult PreviewProfessionReport(string hoganId, int language, int report, int siteId, int programId,int eventId)
        {
            var model = new PracticeReportsViewModel(UnitOfWork.IUserPracticeTextRepository, hoganId, language, report);
            var PracticeReport = new PracticeReportModel();                      
            var Candidate = UnitOfWork.IPersonRepository.First(m => m.Hogan_Id == hoganId);            
            //var Report = UnitOfWork.IPracticeReportRepository.GetById(1, "en-US");
            var Report = UnitOfWork.IPracticeReportRepository.First(m => m.Id == report);
            var ReportSet = UnitOfWork.IUserPracticeTextRepository.GetUserPracticeText(hoganId, language, report);
            // TODO: refactor this mess ..
            // Need to do the replacements
            var Replacements = UnitOfWork.IReplacementExpressionRepository.AsQueryable();
            foreach (var item in ReportSet)
            {

                foreach (var replitem in Replacements)
                {
                    string pattern = replitem.FindValue;
                    
                        switch (pattern)
                        {
                            case "<Dr.>":
                                item.Text = Regex.Replace(item.Text, pattern, "Dr.");
                                break;
                            case "<His>":
                                if (Candidate.Gender == "Male")
                                    item.Text = Regex.Replace(item.Text, pattern, "His");
                                else if (Candidate.Gender == "Female")
                                    item.Text = Regex.Replace(item.Text, pattern, "Her");
                                break;
                            case "<his>":
                                if (Candidate.Gender == "Male")
                                    item.Text = Regex.Replace(item.Text, pattern, "his");
                                else if (Candidate.Gender == "Female")
                                    item.Text = Regex.Replace(item.Text, pattern, "her");
                                break;
                            case "<He>":
                                if (Candidate.Gender == "Male")
                                    item.Text = Regex.Replace(item.Text, pattern, "He");
                                else if (Candidate.Gender == "Female")
                                    item.Text = Regex.Replace(item.Text, pattern, "She");
                                break;
                            case "<he>":
                                if (Candidate.Gender == "Male")
                                    item.Text = Regex.Replace(item.Text, pattern, "he");
                                else if (Candidate.Gender == "Female")
                                    item.Text = Regex.Replace(item.Text, pattern, "she");
                                break;
                            case "<Himself>":
                                if (Candidate.Gender == "Male")
                                    item.Text = Regex.Replace(item.Text, pattern, "Himself");
                                else if (Candidate.Gender == "Female")
                                    item.Text = Regex.Replace(item.Text, pattern, "Herself");
                                break;
                            case "<himself>":
                                if (Candidate.Gender == "Male")
                                    item.Text = Regex.Replace(item.Text, pattern, "himself");
                                else if (Candidate.Gender == "Female")
                                    item.Text = Regex.Replace(item.Text, pattern, "herself");
                                break;
                            case "<him>":
                                if (Candidate.Gender == "Male")
                                    item.Text = Regex.Replace(item.Text, pattern, "him");
                                else if (Candidate.Gender == "Female")
                                    item.Text = Regex.Replace(item.Text, pattern, "her");
                                break;
                            case "<Him>":
                                if (Candidate.Gender == "Male")
                                    item.Text = Regex.Replace(item.Text, pattern, "Him");
                                else if (Candidate.Gender == "Female")
                                    item.Text = Regex.Replace(item.Text, pattern, "Her");
                                break;
                            case "<Name>":
                                item.Text = Regex.Replace(item.Text, pattern, Candidate.FirstName);
                                break;
                            case "<Title>":
                                if (string.IsNullOrEmpty(Candidate.Title))
                                {
                                    item.Text = Regex.Replace(item.Text, pattern, "Dr.");
                                }
                                else
                                {
                                    item.Text = Regex.Replace(item.Text, pattern, Candidate.Title);
                                }
                                
                                break;
                            case "<Lastname>":
                                item.Text = Regex.Replace(item.Text, pattern, Candidate.LastName);
                                break;
                            
                            
                            
                    }
                    
                }
                
                //string pattern = "(<Dr.>)";
                //var currentText = item.Text;
                //currentText = Regex.Replace(currentText, pattern, Replacements.FirstOrDefault().Expression);
                //item.Text = currentText;
                
            }


            var Site = UnitOfWork.ISiteRepository.First(h => h.Id == siteId);
                
            var ReportFor = Candidate.FirstName + " " + Candidate.LastName;

           

            PracticeReport.HoganId = hoganId;
            PracticeReport.ReportFor = ReportFor;                                   

            var jPersonicaLogo = "";

            if (Site.BrandingLogo != null)
            {
                PracticeReport.Color = Site.BrandingColor;
                PracticeReport.BackgroundMimeType = Site.BrandingBackgroundMimeType;
                PracticeReport.Background = Site.BrandingBackground;
                PracticeReport.Logo = Site.BrandingLogo;
                PracticeReport.LogoMimeType = Site.BrandingLogoMimeType;
                //RS LOGO DOTS'
                jPersonicaLogo = Server.MapPath("~\\Reports\\images\\j3p_Logo_Distributed_dots.png");               
            }
            else
            {
                PracticeReport.Color = Report.DefaultColor;
                PracticeReport.BackgroundMimeType = Report.DefaultBackgroundMimeType;
                PracticeReport.Background = Report.DefaultBackground;
                PracticeReport.Logo = Report.DefaultLogo;
                PracticeReport.LogoMimeType = Report.DefaultLogoMimeType;
                jPersonicaLogo = Server.MapPath("~\\Reports\\images\\j3p_Logo_Distributed.png");
            }

            var noTranslation = "No Translation Available";
            PracticeReport.Title = noTranslation;
            PracticeReport.Introduction = noTranslation;
            PracticeReport.IntroductionTwo = noTranslation;
            PracticeReport.IntroductionThree = noTranslation;

            if (Report.ReportTitleResx.ResxValues.FirstOrDefault(rx => rx.CultureId == language) != null)
                PracticeReport.Title = Report.ReportTitleResx.ResxValues.First(rx => rx.CultureId == language).Value;
            if (Report.IntroductionResx.ResxValues.FirstOrDefault(rx => rx.CultureId == language) != null)
                PracticeReport.Introduction = Report.IntroductionResx.ResxValues.First(rx => rx.CultureId == language).Value;
            if (Report.IntroductionTwoResx.ResxValues.FirstOrDefault(rx => rx.CultureId == language) != null)
                PracticeReport.IntroductionTwo = Report.IntroductionTwoResx.ResxValues.First(rx => rx.CultureId == language).Value;
            if (Report.IntroductionThreeResx.ResxValues.FirstOrDefault(rx => rx.CultureId == language) != null)
                PracticeReport.IntroductionThree = Report.IntroductionThreeResx.ResxValues.First(rx => rx.CultureId == language).Value;

            

            List<PracticeReportModel> ReportBarandingData = new List<PracticeReportModel>();
            ReportBarandingData.Add(PracticeReport);
                
                
            
            
            model.localReport.EnableExternalImages = true;
            switch (report)
            {
                case 1:
                    model.FileName = "~/Reports/ProfessionPractice.rdlc";                    
                    break;
                case 2:
                    model.FileName = "~/Reports/TransistionPractice.rdlc";                    
                    break;
                case 3:
                    model.FileName = "~/Reports/ManagePractice.rdlc";                    
                    break;
                case 11:
                    model.FileName = "~/Reports/GraphPractice.rdlc";
                    break;
            }
            model.ReportTitle = Candidate.LastName.Replace(' ', '_') + "_" + Candidate.FirstName.Replace(' ', '_') + "_" + PracticeReport.Title.Replace(' ', '_') + "_" + hoganId;           
            model.localReport.DataSources.Add(new ReportDataSource("PracticeReports", ReportSet));
            model.localReport.DataSources.Add(new ReportDataSource("PracticeReportBranding", ReportBarandingData));
            model.Format = Models.Base.ReportViewModel.ReportFormat.PDF;
            model.localReport.ReportPath = System.Web.HttpContext.Current.Server.MapPath(model.FileName);            
            model.ViewAsAttachment = true;
            model.localReport.SetParameters(new ReportParameter("jPersonicaLogo", jPersonicaLogo));
            
                       
            
            var renderbytes = model.RenderReport();
            
            //Record the Report Request
            PersonPracticeReport ReportRequest = new PersonPracticeReport();
            ReportRequest.AspNetUsersId = User.Identity.GetUserId();
            ReportRequest.PersonId = Candidate.Id;
            ReportRequest.PracticeReportId = report;
            ReportRequest.RunDate = DateTime.Now;
            ReportRequest.EventId = eventId;
            //ReportRequest.EventId = 
            UnitOfWork.IPersonPracticeReportRepository.Add(ReportRequest);
            UnitOfWork.Commit();
            // ------------------            
                return File(renderbytes, model.LastmimeType, model.ReportExportFileName);                            
        }
        [Log]
        [Authorize(Roles = "Admin,SiteCordinator,J3PAdmin")]
        public ActionResult PreviewMatchReport(string hoganId, int language, int report, int siteId, int programId, int eventId)
        {

            var PracticeReport = new PracticeReportModel();
            var Candidate = UnitOfWork.IPersonRepository.First(m => m.Hogan_Id == hoganId);
            var Report = UnitOfWork.IPracticeReportRepository.First(m => m.Id == report);
            var Site = UnitOfWork.ISiteRepository.First(h => h.Id == siteId);
            var programID = programId;
            var model = new PracticeMatchReportsViewModel(UnitOfWork.IUserPracticeCategoryTextRepository, hoganId, language, report, programID);
            var ReportSet = UnitOfWork.IUserPracticeCategoryTextRepository.GetUserPracticeCategoryText(hoganId, language, report, programID);
            var ReportFor = Candidate.FirstName + " " + Candidate.LastName;
            var ValidRating = UnitOfWork.IManual_Hogan_ImportRepository.AsQueryable().FirstOrDefault(m => m.Hogan_User_ID == hoganId).Valid;



            PracticeReport.HoganId = hoganId;
            PracticeReport.ReportFor = ReportFor;

            var jPersonicaLogo = "";

            if (Site.BrandingLogo != null)
            {
                PracticeReport.Color = Site.BrandingColor;
                PracticeReport.BackgroundMimeType = Site.BrandingBackgroundMimeType;
                PracticeReport.Background = Site.BrandingBackground;
                PracticeReport.Logo = Site.BrandingLogo;
                PracticeReport.LogoMimeType = Site.BrandingLogoMimeType;
                //RS LOGO DOTS'
                jPersonicaLogo = Server.MapPath("~\\Reports\\images\\j3p_Logo_Distributed_dots.png");

            }
            else
            {
                PracticeReport.Color = Report.DefaultColor;
                PracticeReport.BackgroundMimeType = Report.DefaultBackgroundMimeType;
                PracticeReport.Background = Report.DefaultBackground;
                PracticeReport.Logo = Report.DefaultLogo;
                PracticeReport.LogoMimeType = Report.DefaultLogoMimeType;
                jPersonicaLogo = Server.MapPath("~\\Reports\\images\\j3p_Logo_Distributed.png");
            }

            var noTranslation = "No Translation Available";
            PracticeReport.Title = noTranslation;
            PracticeReport.Introduction = noTranslation;
            PracticeReport.IntroductionTwo = noTranslation;
            PracticeReport.IntroductionThree = noTranslation;


            //TODO: FIX THIS DUPLICATION
            if (Report.ReportTitleResx.ResxValues.FirstOrDefault(rx => rx.CultureId == language) != null)
                PracticeReport.Title = Report.ReportTitleResx.ResxValues.First(rx => rx.CultureId == language).Value;
            if (Report.IntroductionResx.ResxValues.FirstOrDefault(rx => rx.CultureId == language) != null)
                PracticeReport.Introduction = Report.IntroductionResx.ResxValues.First(rx => rx.CultureId == language).Value;
            if (Report.IntroductionTwoResx.ResxValues.FirstOrDefault(rx => rx.CultureId == language) != null)
                PracticeReport.IntroductionTwo = Report.IntroductionTwoResx.ResxValues.First(rx => rx.CultureId == language).Value;
            if (Report.IntroductionThreeResx.ResxValues.FirstOrDefault(rx => rx.CultureId == language) != null)
                PracticeReport.IntroductionThree = Report.IntroductionThreeResx.ResxValues.First(rx => rx.CultureId == language).Value;



            List<PracticeReportModel> ReportBarandingData = new List<PracticeReportModel>();
            ReportBarandingData.Add(PracticeReport);




            model.localReport.EnableExternalImages = true;

            //TODO need to fix these two hard codes some how?  Look from db first i guess
            if (report == 8)
            {
                model.FileName = "~/Reports/RangeReport.rdlc";
            }
            else if (report == 7)
            {
                model.FileName = "~/Reports/MatchReport.rdlc";
            }
            else if (report == 9)
            {
                model.FileName = "~/Reports/CoachReport.rdlc";
            }
            else if (report == 10)
            {
                model.FileName = "~/Reports/CoachReport.rdlc";
            }
            else if (report == 12)
            {
                model.FileName = "~/Reports/ScalesReport.rdlc";
            }
            else if (report == 13)
            {
                model.FileName = "~/Reports/ScalesReport.rdlc";
            }
            else if (report == 14)
            {
                model.FileName = "~/Reports/ScalesReport.rdlc";
            }
            //model.ReportTitle = hoganId + "_" + Candidate.FirstName.Replace(' ', '_') + "_" + Candidate.LastName.Replace(' ', '_') + "_" + PracticeReport.Title.Replace(' ', '_');
            model.ReportTitle = Candidate.LastName.Replace(' ', '_') + "_" + Candidate.FirstName.Replace(' ', '_') + "_" + PracticeReport.Title.Replace(' ', '_') + "_" + hoganId;
            model.localReport.DataSources.Add(new ReportDataSource("MatchPracticeReports", ReportSet));
            model.localReport.DataSources.Add(new ReportDataSource("PracticeReportBranding", ReportBarandingData));
            model.Format = Models.Base.ReportViewModel.ReportFormat.PDF;
            model.localReport.ReportPath = System.Web.HttpContext.Current.Server.MapPath(model.FileName);
            model.ViewAsAttachment = true;
            //TODO need to fix these two hard codes some how?  Look from db first i guess
            int CountOFIN = ReportSet.Count(m => m.CategoryId == 1002 || m.CategoryId == 1003);
            int TotalCount = ReportSet.Count(m => m != null);
            model.localReport.SetParameters(new ReportParameter("CountOFIN", CountOFIN.ToString()));
            model.localReport.SetParameters(new ReportParameter("TotalCount", TotalCount.ToString()));
            model.localReport.SetParameters(new ReportParameter("jPersonicaLogo", jPersonicaLogo));
            if (report == 7)
            {

                string AssessmentValid = "Assessment Not Valid";
                if (ValidRating.HasValue)
                {
                    if (ValidRating.Value >= 10)
                        AssessmentValid = "Assessment Valid";
                }
                model.localReport.SetParameters(new ReportParameter("Valid", AssessmentValid));
            }

            var renderbytes = model.RenderReport();
            //Record the Report Request

            PersonPracticeReport ReportRequest = new PersonPracticeReport();
            ReportRequest.AspNetUsersId = User.Identity.GetUserId();
            ReportRequest.PersonId = Candidate.Id;
            ReportRequest.PracticeReportId = report;
            ReportRequest.RunDate = DateTime.Now;
            ReportRequest.EventId = eventId;
            UnitOfWork.IPersonPracticeReportRepository.Add(ReportRequest);
            UnitOfWork.Commit();
            // ------------------            
                return File(renderbytes, model.LastmimeType, model.ReportExportFileName);
            
        }
        [Log]
        [Authorize(Roles = "Admin")]
        public ActionResult UploadFiles()
        {
            int i = 0;
            
                var r = new List<UploadFileModel>();
                string savedFileName = "";
                int ErrorCount = 0;
                int SuccessCount = 0;
                foreach (string key in Request.Files)
                {
                   ErrorCount = 0;
                   SuccessCount = 0;
                
                    HttpPostedFileBase hpf = Request.Files[i] as HttpPostedFileBase;
                    if (hpf.ContentLength == 0)
                        continue;
                    savedFileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Properties.Settings.Default.UploadFilesFolderName, Path.GetFileName(hpf.FileName));
                    hpf.SaveAs(savedFileName);

                   
                    FileInfo fileInfo = new FileInfo(savedFileName);
                    if (fileInfo.Exists)
                    {
                        using (var reader = new CsvHelper.CsvReader(new StreamReader(savedFileName)))
                        {
                            reader.Configuration.RegisterClassMap<PPI.Core.Web.Infrastructure.ImportMaps.Hogan_File_Map>();
                            reader.Configuration.IgnoreReadingExceptions = true;
                            reader.Configuration.ReadingExceptionCallback = (exception, row) =>
                                {
                                    Trace.TraceInformation("Row Exception {0}: Row Data {1}", exception.Message, row.Parser.RawRecord);
                                    ErrorCount++;
                                };
                            var records = reader.GetRecords<PPI.Core.Domain.Entities.Manual_Hogan_Import>();
                            
                            //Should be an Add / Update base on Hogin ID
                            foreach (var item in records)
	                        {
                                var Hogan = UnitOfWork.IManual_Hogan_ImportRepository.AsQueryable().FirstOrDefault(m => m.Hogan_User_ID == item.Hogan_User_ID);
                                if (Hogan == null)
                                {
                                    item.LastUpdated = DateTime.Now;
                                    UnitOfWork.IManual_Hogan_ImportRepository.Add(item);
                                }
                                else
                                {
                                    item.LastUpdated = DateTime.Now;                                                                        
                                    UnitOfWork.IManual_Hogan_ImportRepository.Update(Utility.Update(Hogan, item));
                                }
                                SuccessCount++;
                                UnitOfWork.Commit();
                            }
                            
                            
                            
                        }
                    }
                     r.Add(new UploadFileModel()
                    {
                        Name = savedFileName,
                        Length = hpf.ContentLength,
                        FailedCount = ErrorCount,
                        SuccessfullCount = SuccessCount
                    });
                    i++;
                }
                
                return View("UploadFiles", r);
          
        }
        [Log]
        [Authorize(Roles = "Admin")]
        [ActionName("DeleteAll")]
        public ActionResult DeleteReports()
        {
            var concreate = UnitOfWork.IManual_Hogan_ImportRepository.AsQueryable();
            UnitOfWork.IManual_Hogan_ImportRepository.Delete(concreate);
            UnitOfWork.Commit();
            return View("Delete");
        }
        #endregion        
        [Authorize(Roles = "Admin,SiteCordinator,J3PAdmin")]
        public ActionResult GetReports(int? eventId, int? page)
        {
            var model = new GetReportViewModel();
            SelectList Events = null;
            Events = this.Events;            
            int totalRecords = 0;
            int? FirstEventId = this.CurrentEvent;
            if (Events.Count() > 0)
            {
                if (eventId.HasValue)
                {
                    FirstEventId = eventId.Value;
                    this.CurrentEvent = FirstEventId.Value;
                }
            }
            model.EventId = FirstEventId;            
            model.Reports = UnitOfWork.IEventPracticeReportRepository.AsQueryable().Where(m => m.EventId == FirstEventId);
            
            totalRecords = UnitOfWork.IPersonEventRepository.AsQueryable().Where(m => m.EventId == FirstEventId).Select(t => t.Person).Count();
            PagingInfo pagingInfo = new PagingInfo { CurrentPage = page.GetValueOrDefault(1), PageCount = 5, TotalRecords = totalRecords };
            pagingInfo.PageSize = int.Parse(PPI.Core.Web.Infrastructure.Utility.GetCookie("pageSize"));
            pagingInfo.LastPage = totalRecords / pagingInfo.PageSize;
            model.PagingInfo = pagingInfo;                                

            
            model.Participants = UnitOfWork.IPersonEventRepository.AsQueryable().Where(m => m.EventId == FirstEventId)
                .Select(t => t.Person)
                .ToList().OrderBy(s => s.LastName)
                .Skip((pagingInfo.CurrentPage - 1) * pagingInfo.PageSize)
                .Take(pagingInfo.PageSize);

            model.PeopleAvailibleReports = new List<PeopleAvailibleReports>();
            foreach (var item in model.Participants)
            {
                var newList = new PeopleAvailibleReports();
                newList.PersonId = item.Id;
                newList.ReportDataAvailible = PPI.Core.Web.Infrastructure.Utility.ReportDataAvailible(item.Hogan_Id, UnitOfWork);
                model.PeopleAvailibleReports.Add(newList);
            }

            ViewBag.EventId = new SelectList(Events, "Value", "Text", model.EventId);

            return View(model);
        }
        [Authorize(Roles = "Admin,SiteCordinator,J3PAdmin")]
        [HttpPost]
        public ActionResult GetReports(GetReportsPostViewModel model)
        {            
            var CurrentEvent = UnitOfWork.IEventRepository.AsQueryable().FirstOrDefault(m => m.Id == model.EventId);
            if (CurrentEvent == null)
                return new HttpNotFoundResult();
            var uniqueFolder = Guid.NewGuid().ToString();
            var FilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Properties.Settings.Default.UploadFilesFolderName,uniqueFolder);
            System.IO.DirectoryInfo di = new DirectoryInfo(FilePath);
            di.Create();
            var zipFIleName = FilePath + "\\" + uniqueFolder + ".zip";
            var zip = new ZipFile(zipFIleName);
            zip.TempFileFolder = AppDomain.CurrentDomain.BaseDirectory;
                foreach (var item in model.SelectedReports)
                {
                
                    if (!item.Equals("on"))
                    {
                        var userId = int.Parse(item.Split('_')[1].ToString());
                        var reportId = int.Parse(item.Split('_')[0].ToString());
                        var CurrentUser = UnitOfWork.IPersonRepository.AsQueryable().FirstOrDefault(m => m.Id == userId);
                        var CurrentReport = UnitOfWork.IPracticeReportRepository.AsQueryable().FirstOrDefault(m => m.Id == reportId);
                        if (CurrentReport.PracticeGroup == "Match")
                        {
                            DownloadMatchReport(CurrentUser.Hogan_Id, CurrentCulture, CurrentReport.Id, CurrentEvent.ProgramSite.SiteId, CurrentEvent.ProgramSite.ProgramId, CurrentEvent.Id, uniqueFolder);
                        }
                        else
                        {
                            DownloadProfessionReport(CurrentUser.Hogan_Id, CurrentCulture, CurrentReport.Id, CurrentEvent.ProgramSite.SiteId, CurrentEvent.ProgramSite.ProgramId, CurrentEvent.Id, uniqueFolder);
                        }              
                    }
                    
                }
                zip.AddSelectedFiles("*.*", FilePath,"",false);
                zip.Save();                
                return File(zipFIleName, "application/zip", uniqueFolder + ".zip");
        }
        public ActionResult ReportEventSwitch(int eventId)
        {
            return RedirectToAction("GetReports", new { eventId = eventId });
        }
        public string DownloadProfessionReport(string hoganId, int language, int report, int siteId, int programId, int eventId, string uniqueFolder)
        {
            var model = new PracticeReportsViewModel(UnitOfWork.IUserPracticeTextRepository, hoganId, language, report);
            var PracticeReport = new PracticeReportModel();
            var Candidate = UnitOfWork.IPersonRepository.First(m => m.Hogan_Id == hoganId);
            //var Report = UnitOfWork.IPracticeReportRepository.GetById(1, "en-US");
            var Report = UnitOfWork.IPracticeReportRepository.First(m => m.Id == report);
            var ReportSet = UnitOfWork.IUserPracticeTextRepository.GetUserPracticeText(hoganId, language, report);
            // TODO: refactor this mess ..
            // Need to do the replacements
            var Replacements = UnitOfWork.IReplacementExpressionRepository.AsQueryable();
            foreach (var item in ReportSet)
            {

                foreach (var replitem in Replacements)
                {
                    string pattern = replitem.FindValue;

                    switch (pattern)
                    {
                        case "<Dr.>":
                            item.Text = Regex.Replace(item.Text, pattern, "Dr.");
                            break;
                        case "<His>":
                            if (Candidate.Gender == "Male")
                                item.Text = Regex.Replace(item.Text, pattern, "His");
                            else if (Candidate.Gender == "Female")
                                item.Text = Regex.Replace(item.Text, pattern, "Her");
                            break;
                        case "<his>":
                            if (Candidate.Gender == "Male")
                                item.Text = Regex.Replace(item.Text, pattern, "his");
                            else if (Candidate.Gender == "Female")
                                item.Text = Regex.Replace(item.Text, pattern, "her");
                            break;
                        case "<He>":
                            if (Candidate.Gender == "Male")
                                item.Text = Regex.Replace(item.Text, pattern, "He");
                            else if (Candidate.Gender == "Female")
                                item.Text = Regex.Replace(item.Text, pattern, "She");
                            break;
                        case "<he>":
                            if (Candidate.Gender == "Male")
                                item.Text = Regex.Replace(item.Text, pattern, "he");
                            else if (Candidate.Gender == "Female")
                                item.Text = Regex.Replace(item.Text, pattern, "she");
                            break;
                        case "<Himself>":
                            if (Candidate.Gender == "Male")
                                item.Text = Regex.Replace(item.Text, pattern, "Himself");
                            else if (Candidate.Gender == "Female")
                                item.Text = Regex.Replace(item.Text, pattern, "Herself");
                            break;
                        case "<himself>":
                            if (Candidate.Gender == "Male")
                                item.Text = Regex.Replace(item.Text, pattern, "himself");
                            else if (Candidate.Gender == "Female")
                                item.Text = Regex.Replace(item.Text, pattern, "herself");
                            break;
                        case "<him>":
                            if (Candidate.Gender == "Male")
                                item.Text = Regex.Replace(item.Text, pattern, "him");
                            else if (Candidate.Gender == "Female")
                                item.Text = Regex.Replace(item.Text, pattern, "her");
                            break;
                        case "<Him>":
                            if (Candidate.Gender == "Male")
                                item.Text = Regex.Replace(item.Text, pattern, "Him");
                            else if (Candidate.Gender == "Female")
                                item.Text = Regex.Replace(item.Text, pattern, "Her");
                            break;
                        case "<Name>":
                            item.Text = Regex.Replace(item.Text, pattern, Candidate.FirstName);
                            break;
                        case "<Title>":
                            if (string.IsNullOrEmpty(Candidate.Title))
                            {
                                item.Text = Regex.Replace(item.Text, pattern, "Dr.");
                            }
                            else
                            {
                                item.Text = Regex.Replace(item.Text, pattern, Candidate.Title);
                            }

                            break;
                        case "<Lastname>":
                            item.Text = Regex.Replace(item.Text, pattern, Candidate.LastName);
                            break;



                    }

                }

                //string pattern = "(<Dr.>)";
                //var currentText = item.Text;
                //currentText = Regex.Replace(currentText, pattern, Replacements.FirstOrDefault().Expression);
                //item.Text = currentText;

            }

            //if siteId = -1 were going to use default branding on the report and not Any Site Branding
            Site Site;
            if (siteId == -1)
            {
                Site = new Site();
                Site.BrandingLogo = null;
            }
            else
            {
                Site = UnitOfWork.ISiteRepository.First(h => h.Id == siteId);
            }
            
            var ReportFor = Candidate.FirstName + " " + Candidate.LastName;

            PracticeReport.HoganId = hoganId;
            PracticeReport.ReportFor = ReportFor;

            var jPersonicaLogo = "";

            if (Site.BrandingLogo != null)
            {
                PracticeReport.Color = Site.BrandingColor;
                PracticeReport.BackgroundMimeType = Site.BrandingBackgroundMimeType;
                PracticeReport.Background = Site.BrandingBackground;
                PracticeReport.Logo = Site.BrandingLogo;
                PracticeReport.LogoMimeType = Site.BrandingLogoMimeType;
                //RS LOGO DOTS'
                jPersonicaLogo = Server.MapPath("~\\Reports\\images\\j3p_Logo_Distributed_dots.png");
            }
            else
            {
                PracticeReport.Color = Report.DefaultColor;
                PracticeReport.BackgroundMimeType = Report.DefaultBackgroundMimeType;
                PracticeReport.Background = Report.DefaultBackground;
                PracticeReport.Logo = Report.DefaultLogo;
                PracticeReport.LogoMimeType = Report.DefaultLogoMimeType;
                jPersonicaLogo = Server.MapPath("~\\Reports\\images\\j3p_Logo_Distributed.png");
            }

            var noTranslation = "No Translation Available";
            PracticeReport.Title = noTranslation;
            PracticeReport.Introduction = noTranslation;
            PracticeReport.IntroductionTwo = noTranslation;
            PracticeReport.IntroductionThree = noTranslation;

            if (Report.ReportTitleResx.ResxValues.FirstOrDefault(rx => rx.CultureId == language) != null)
                PracticeReport.Title = Report.ReportTitleResx.ResxValues.First(rx => rx.CultureId == language).Value;
            if (Report.IntroductionResx.ResxValues.FirstOrDefault(rx => rx.CultureId == language) != null)
                PracticeReport.Introduction = Report.IntroductionResx.ResxValues.First(rx => rx.CultureId == language).Value;
            if (Report.IntroductionTwoResx.ResxValues.FirstOrDefault(rx => rx.CultureId == language) != null)
                PracticeReport.IntroductionTwo = Report.IntroductionTwoResx.ResxValues.First(rx => rx.CultureId == language).Value;
            if (Report.IntroductionThreeResx.ResxValues.FirstOrDefault(rx => rx.CultureId == language) != null)
                PracticeReport.IntroductionThree = Report.IntroductionThreeResx.ResxValues.First(rx => rx.CultureId == language).Value;



            List<PracticeReportModel> ReportBarandingData = new List<PracticeReportModel>();
            ReportBarandingData.Add(PracticeReport);




            model.localReport.EnableExternalImages = true;
            switch (report)
            {
                case 1:
                    model.FileName = "~/Reports/ProfessionPractice.rdlc";
                    break;
                case 2:
                    model.FileName = "~/Reports/TransistionPractice.rdlc";
                    break;
                case 3:
                    model.FileName = "~/Reports/ManagePractice.rdlc";
                    break;
                case 11:
                    model.FileName = "~/Reports/GraphPractice.rdlc";
                    break;
            }
            //model.ReportTitle = hoganId + "_" + Candidate.FirstName.Replace(' ', '_') + "_" + Candidate.LastName.Replace(' ', '_') + "_" + PracticeReport.Title.Replace(' ', '_');
            model.ReportTitle = Candidate.LastName.Replace(' ', '_') + "_" + Candidate.FirstName.Replace(' ', '_') + "_" + PracticeReport.Title.Replace(' ', '_') + "_" + hoganId;           
            model.localReport.DataSources.Add(new ReportDataSource("PracticeReports", ReportSet));
            model.localReport.DataSources.Add(new ReportDataSource("PracticeReportBranding", ReportBarandingData));
            model.Format = Models.Base.ReportViewModel.ReportFormat.PDF;
            model.localReport.ReportPath = System.Web.HttpContext.Current.Server.MapPath(model.FileName);
            model.ViewAsAttachment = true;
            model.localReport.SetParameters(new ReportParameter("jPersonicaLogo", jPersonicaLogo));



            var renderbytes = model.RenderReport();

            //Record the Report Request
            PersonPracticeReport ReportRequest = new PersonPracticeReport();
            ReportRequest.AspNetUsersId = User.Identity.GetUserId();
            ReportRequest.PersonId = Candidate.Id;
            ReportRequest.PracticeReportId = report;
            ReportRequest.RunDate = DateTime.Now;
            ReportRequest.EventId = eventId;
            //ReportRequest.EventId = 
            UnitOfWork.IPersonPracticeReportRepository.Add(ReportRequest);
            UnitOfWork.Commit();
            // ------------------          
            var savedFileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Properties.Settings.Default.UploadFilesFolderName, uniqueFolder);
            System.IO.Directory.CreateDirectory(savedFileName);
            savedFileName = savedFileName + "\\" + model.ReportTitle + ".pdf";
            System.IO.File.WriteAllBytes(savedFileName, renderbytes);

            return savedFileName;
        }
        public string DownloadMatchReport(string hoganId, int language, int report, int siteId, int programId, int eventId, string uniqueFolder)
        {

            var PracticeReport = new PracticeReportModel();
            var Candidate = UnitOfWork.IPersonRepository.First(m => m.Hogan_Id == hoganId);
            var Report = UnitOfWork.IPracticeReportRepository.First(m => m.Id == report);            
            var programID = programId;
            var model = new PracticeMatchReportsViewModel(UnitOfWork.IUserPracticeCategoryTextRepository, hoganId, language, report, programID);
            var ReportSet = UnitOfWork.IUserPracticeCategoryTextRepository.GetUserPracticeCategoryText(hoganId, language, report, programID);
            var ReportFor = Candidate.FirstName + " " + Candidate.LastName;
            var ValidRating = UnitOfWork.IManual_Hogan_ImportRepository.AsQueryable().FirstOrDefault(m => m.Hogan_User_ID == hoganId).Valid;



            PracticeReport.HoganId = hoganId;
            PracticeReport.ReportFor = ReportFor;

            var jPersonicaLogo = "";

            Site Site;
            if (siteId == -1)
            {
                Site = new Site();
                Site.BrandingLogo = null;
            }
            else
            {
                Site = UnitOfWork.ISiteRepository.First(h => h.Id == siteId);
            }

            if (Site.BrandingLogo != null)
            {
                PracticeReport.Color = Site.BrandingColor;
                PracticeReport.BackgroundMimeType = Site.BrandingBackgroundMimeType;
                PracticeReport.Background = Site.BrandingBackground;
                PracticeReport.Logo = Site.BrandingLogo;
                PracticeReport.LogoMimeType = Site.BrandingLogoMimeType;
                //RS LOGO DOTS'
                jPersonicaLogo = Server.MapPath("~\\Reports\\images\\j3p_Logo_Distributed_dots.png");

            }
            else
            {
                PracticeReport.Color = Report.DefaultColor;
                PracticeReport.BackgroundMimeType = Report.DefaultBackgroundMimeType;
                PracticeReport.Background = Report.DefaultBackground;
                PracticeReport.Logo = Report.DefaultLogo;
                PracticeReport.LogoMimeType = Report.DefaultLogoMimeType;
                jPersonicaLogo = Server.MapPath("~\\Reports\\images\\j3p_Logo_Distributed.png");
            }

            var noTranslation = "No Translation Available";
            PracticeReport.Title = noTranslation;
            PracticeReport.Introduction = noTranslation;
            PracticeReport.IntroductionTwo = noTranslation;
            PracticeReport.IntroductionThree = noTranslation;


            //TODO: FIX THIS DUPLICATION
            if (Report.ReportTitleResx.ResxValues.FirstOrDefault(rx => rx.CultureId == language) != null)
                PracticeReport.Title = Report.ReportTitleResx.ResxValues.First(rx => rx.CultureId == language).Value;
            if (Report.IntroductionResx.ResxValues.FirstOrDefault(rx => rx.CultureId == language) != null)
                PracticeReport.Introduction = Report.IntroductionResx.ResxValues.First(rx => rx.CultureId == language).Value;
            if (Report.IntroductionTwoResx.ResxValues.FirstOrDefault(rx => rx.CultureId == language) != null)
                PracticeReport.IntroductionTwo = Report.IntroductionTwoResx.ResxValues.First(rx => rx.CultureId == language).Value;
            if (Report.IntroductionThreeResx.ResxValues.FirstOrDefault(rx => rx.CultureId == language) != null)
                PracticeReport.IntroductionThree = Report.IntroductionThreeResx.ResxValues.First(rx => rx.CultureId == language).Value;



            List<PracticeReportModel> ReportBarandingData = new List<PracticeReportModel>();
            ReportBarandingData.Add(PracticeReport);




            model.localReport.EnableExternalImages = true;

            //TODO need to fix these two hard codes some how?  Look from db first i guess
            if (report == 8)
            {
                model.FileName = "~/Reports/RangeReport.rdlc";
            }
            else if (report == 7)
            {
                model.FileName = "~/Reports/MatchReport.rdlc";                
            }
            else if (report == 9)
            {
                model.FileName = "~/Reports/CoachReport.rdlc";
            }
            else if (report == 10)
            {
                model.FileName = "~/Reports/CoachReport.rdlc";
            }
            else if (report == 12)
            {
                model.FileName = "~/Reports/ScalesReport.rdlc";
            }
            else if (report == 13)
            {
                model.FileName = "~/Reports/ScalesReport.rdlc";
            }
            else if (report == 14)
            {
                model.FileName = "~/Reports/ScalesReport.rdlc";
            }
            //model.ReportTitle = hoganId + "_" + Candidate.FirstName.Replace(' ', '_') + "_" + Candidate.LastName.Replace(' ', '_') + "_" + PracticeReport.Title.Replace(' ', '_');
            model.ReportTitle = Candidate.LastName.Replace(' ', '_') + "_" + Candidate.FirstName.Replace(' ', '_') + "_" + PracticeReport.Title.Replace(' ', '_') + "_" + hoganId;
            model.localReport.DataSources.Add(new ReportDataSource("MatchPracticeReports", ReportSet));
            model.localReport.DataSources.Add(new ReportDataSource("PracticeReportBranding", ReportBarandingData));
            model.Format = Models.Base.ReportViewModel.ReportFormat.PDF;
            model.localReport.ReportPath = System.Web.HttpContext.Current.Server.MapPath(model.FileName);
            model.ViewAsAttachment = true;
            //TODO need to fix these two hard codes some how?  Look from db first i guess
            int CountOFIN = ReportSet.Count(m => m.CategoryId == 1002 || m.CategoryId == 1003);
            int TotalCount = ReportSet.Count(m => m != null);
            model.localReport.SetParameters(new ReportParameter("CountOFIN", CountOFIN.ToString()));
            model.localReport.SetParameters(new ReportParameter("TotalCount", TotalCount.ToString()));
            model.localReport.SetParameters(new ReportParameter("jPersonicaLogo", jPersonicaLogo));
            if (report == 7)
            {
                string AssessmentValid = "Assessment Not Valid";
                if (ValidRating.HasValue)
                {
                    if (ValidRating.Value >= 10)
                        AssessmentValid = "Assessment Valid";
                }
                model.localReport.SetParameters(new ReportParameter("Valid", AssessmentValid));
            }

            var renderbytes = model.RenderReport();
            //Record the Report Request

            PersonPracticeReport ReportRequest = new PersonPracticeReport();
            ReportRequest.AspNetUsersId = User.Identity.GetUserId();
            ReportRequest.PersonId = Candidate.Id;
            ReportRequest.PracticeReportId = report;
            ReportRequest.RunDate = DateTime.Now;
            ReportRequest.EventId = eventId;
            UnitOfWork.IPersonPracticeReportRepository.Add(ReportRequest);
            UnitOfWork.Commit();
            // ------------------            
            var savedFileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Properties.Settings.Default.UploadFilesFolderName, uniqueFolder, model.ReportTitle + ".pdf");
            System.IO.File.WriteAllBytes(savedFileName, renderbytes);
            return savedFileName;
        }
	}
}