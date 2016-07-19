using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Diagnostics;
using System.Text;

namespace PPI.Core.Web.Controllers
{
    using PPI.Core.Domain.Entities;
    using PPI.Core.Domain.Abstract;
    using PPI.Core.Web.Models;
    using CsvHelper;

    public class HoganController : BaseController
    {        
		//Replace in Scafolding file
		//private CoreContainer db = new CoreContainer();
        [Log]
        public HoganController(IUnitOfWork unitOfWork)
            : base(unitOfWork) { }

        public ActionResult ExportTemplateSwitch(int? templateId)
        {
            return RedirectToAction("ZCOExportConfig", new { templateId = templateId });
        }

        [Log]
        [Authorize(Roles = "Admin,J3PAdmin")]
        public ActionResult ZCOLaunch()
        {
            return View();
        }

        [Log]
        [Authorize(Roles = "J3PAdmin")]        
        public ActionResult ZCOExport(int eventId, string selectedColIds)
        {
            //get people from specified event
            var personList = UnitOfWork.IPersonRepository.AsQueryable()
                .Where(p => p.PersonEmails.Count(t => t.Email.EmailTypeId == 3 && t.Email.EventId == eventId) > 0);
            var exportSource = new List<Manual_Hogan_Import>();

            foreach (var item in personList)
            {
                var manualHogan = UnitOfWork.IManual_Hogan_ImportRepository.AsQueryable()
                    .Where(p => p.Hogan_User_ID == item.Hogan_Id)
                    .FirstOrDefault();

                if (manualHogan != null)
                {
                    exportSource.Add(manualHogan);
                }
            }

            //DEBUGGING
            //var manualHogan = UnitOfWork.IManual_Hogan_ImportRepository.AsQueryable();
            //foreach (var item in manualHogan)
            //{
            //    exportSource.Add(item);
            //}

            CsvWriter writer;
            string exportName = "zcoHoganExport.csv";
            var streamoutput = new MemoryStream();
            var sw = new StreamWriter(streamoutput);
            TempData["zcoNumPersonsExported"] = exportSource.Count();

            if (exportSource.Count() > 0)
            {
                //generate zco column names from id's
                //StringBuilder selectedColNames = new StringBuilder(string.Empty);
                var zcoAll = UnitOfWork.IZCOExportMapRepository.AsQueryable();
                string[] arrSelectedColIds = selectedColIds.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);                                               
                Dictionary<string, ZCOExportMap> zcoAllCols = 
                    UnitOfWork.IZCOExportMapRepository.AsQueryable().ToDictionary(t => t.HoganColName, t => t);

                if (arrSelectedColIds.GetLength(0) > 0)
                {
                    Dictionary<string, ZCOExportMap> zcoSelectedCols = new Dictionary<string, ZCOExportMap>();

                    foreach (string id in arrSelectedColIds)
                    {
                        int idVal = Convert.ToInt32(id);
                        ZCOExportMap map = UnitOfWork.IZCOExportMapRepository.AsQueryable().Where(n => n.Id == idVal).First();
                        zcoSelectedCols.Add(map.HoganColName, map);
                    }

                    Session["zcoHoganExportSelectedCols"] = zcoSelectedCols;
                }
                else
                {
                    //no cols checked, so default to all.  not worth a validation error
                    Session["zcoHoganExportSelectedCols"] = zcoAllCols;
                }

                Session["zcoHoganExportAllCols"] = zcoAllCols;

                var CsvConfig = new CsvHelper.Configuration.CsvConfiguration();
                CsvConfig.RegisterClassMap<PPI.Core.Web.Infrastructure.ImportMaps.Zco_Hogan_Export_Map>();
                writer = new CsvWriter(sw, CsvConfig);
                IEnumerable<Manual_Hogan_Import> records = exportSource;
                writer.WriteRecords(records);
                sw.Flush();
                streamoutput.Seek(0, SeekOrigin.Begin);
                return File(streamoutput, "text/csv", exportName);
            }            
            else
            {
                //no people, no csv                
                return RedirectToAction("ZCOExportConfig");
            }
        }

        [Log]
        [Authorize(Roles = "J3PAdmin")]        
        public ActionResult ZCOExportConfig(int? templateId)
        {
            var ExportTemplates = UnitOfWork.IZCOExportTemplateRepository.AsQueryable().ToList();
            var Events = UnitOfWork.IEventRepository.AsQueryable();

            var blankTemplate = new ZCOExportTemplate();
            blankTemplate.Id = 0;
            blankTemplate.TemplateName = string.Empty;

            ExportTemplates.Insert(0, blankTemplate);
            var model = new ZCOExportConfigViewModel();

            var exportChkBoxes = new List<ZCOCheckBoxItem>();
            var exportAllCols = UnitOfWork.IZCOExportMapRepository.AsQueryable().OrderBy(m => m.Position);            

            if (templateId == null)
            {
                foreach (var item in exportAllCols)
                {
                    var box = new ZCOCheckBoxItem();
                    //box.Disabled = false;
                    box.Name = item.HoganColName;
                    box.Value = item.Id.ToString();
                    box.Selected = false;
                    exportChkBoxes.Add(box);
                }
            }
            else
            {
                //a template was chosen
                var exportTemplateCols = UnitOfWork.IZCOExportMapRepository.AsQueryable()
                    .Where(t => t.ZCOExportTemplateMaps.Count(m => m.ZCOExportTemplateId == templateId) > 0);
                    
                foreach (var item in exportAllCols)
                {
                    var box = new ZCOCheckBoxItem();
                    //box.Disabled = false;
                    box.Name = item.HoganColName;
                    box.Value = item.Id.ToString();
                    box.Selected = (exportTemplateCols.Where(m => m.HoganColName == item.HoganColName).Count() > 0);
                    exportChkBoxes.Add(box);
                }
            }            

            model.ExportCols = exportChkBoxes;

            //For generating list of col names for XLS
            //var hoganColNames = typeof(Manual_Hogan_Import).GetProperties().Select(a => a.Name).ToList();
                                    
            ViewBag.EventList = new SelectList(Events, "Id", "Name", null); //model.EventId);
            //ViewBag.ExportTemplateList = new SelectList(ExportTemplates, "Id", "TemplateName", null);  
            ViewBag.TemplateId = new SelectList(ExportTemplates, "Id", "TemplateName", null);

            if (TempData["zcoNumPersonsExported"] != null)
            {
                model.NumPersonsExported = (int)TempData["zcoNumPersonsExported"];
            }
            else
            {
                model.NumPersonsExported = null;
            }            

            return View(model);
        }

        // GET: /Hogan/
        [Log]
        [Authorize(Roles="Admin")]
        public ActionResult Index()
        {
            var model = UnitOfWork.IManual_Hogan_ImportRepository.AsQueryable();
			return View(model);
        }

        [Log]
        [Authorize(Roles = "Admin")]
        public ActionResult HoganUsers()
        {
            var model = new HoganUsersViewModel();
            ViewBag.ProgramId = new SelectList(UnitOfWork.IProgramRepository.AsQueryable(),"Id","ProgramName");
            model.HoganPrograms = new List<HoganProgramCounts>();
            foreach (var item in UnitOfWork.IProgramRepository.AsQueryable())
            {
                var hogamprogram = new HoganProgramCounts();
                hogamprogram.ProgramName = item.ProgramName;
                hogamprogram.Count = UnitOfWork.IHoganUserInfoRepository.AsQueryable().Where(m => m.Used != true).Count(m => m.ProgramId == item.Id);
                model.HoganPrograms.Add(hogamprogram);
            }
            return View(model);
        }

        public ActionResult UploadFiles(int ProgramId)
        {
            int i = 0;

            var r = new List<UploadFileModel>();
            string savedFileName = "";
            foreach (string key in Request.Files)
            {

                HttpPostedFileBase hpf = Request.Files[i] as HttpPostedFileBase;
                if (hpf.ContentLength == 0)
                    continue;
                savedFileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Properties.Settings.Default.UploadFilesFolderName, Path.GetFileName(hpf.FileName));
                hpf.SaveAs(savedFileName);

                r.Add(new UploadFileModel()
                {
                    Name = savedFileName,
                    Length = hpf.ContentLength
                });
                FileInfo fileInfo = new FileInfo(savedFileName);
                if (fileInfo.Exists)
                {
                    
                    using (var reader = new CsvHelper.CsvReader(new StreamReader(savedFileName)))
                    {
                        reader.Configuration.RegisterClassMap<PPI.Core.Web.Infrastructure.ImportMaps.HoganUser_Map>();
                        reader.Configuration.IgnoreReadingExceptions = true;
                        reader.Configuration.ReadingExceptionCallback = (exception, row) =>
                        {
                            Trace.TraceInformation("Row Exception {0}: Row Data {1}", exception.Message, row.Parser.RawRecord);                            
                        };
                        var records = reader.GetRecords<PPI.Core.Domain.Entities.HoganUserInfo>();
                        
                        foreach (var item in records)
                        {
                            if (UnitOfWork.IHoganUserInfoRepository.AsQueryable().FirstOrDefault(m => m.UserId == item.UserId) == null)
                            { 
                                item.UploadDate = DateTime.Now;
                                item.ProgramId = ProgramId;
                                UnitOfWork.IHoganUserInfoRepository.Add(item);                            
                                UnitOfWork.Commit();                                
                            }
                        }



                    }
                }
                i++;
            }

            return RedirectToAction("HoganUsers");

        }


        // GET: /Hogan/Details/5
        [Log]
        public ActionResult Details(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
			//replace scafolding
            //Manual_Hogan_Import manual_hogan_import = db.Manual_Hogan_Import.Find(id);
			var model = UnitOfWork.IManual_Hogan_ImportRepository.First(m => m.Id == id);
            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        // GET: /Hogan/Create
        [Log]
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Hogan/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Log]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,ClientID,ClientName,GroupName,Hogan_User_ID,First_Name,Last_Name,Gender,HPIDate,Valid,Empathy,NotAnxious,NoGuilt,Calmness,EvenTempered,NoSomaticComplaint,Trusting,GoodAttachment,Competitive,SelfConfidence,NoDepression,Leadership,Identity,NoSocialAnxiety,LikesParties,LikesCrowds,ExperienceSeeking,Exhibitionistic,Entertaining,EasyToLiveWith,Sensitive,Caring,LikesPeople,NoHostility,Moralistic,Mastery,Virtuous,NotAutonomous,NotSpontaneous,ImpulseControl,AvoidsTrouble,ScienceAbility,Curiosity,ThrillSeeking,IntellectualGames,GeneratesIdeas,Culture,Education,MathAbility,GoodMemory,Reading,RValidity,RAdjustment,RAmbition,RSociability,RInterpersonalSensitivity,RPrudence,RInquisitive,RLearningApproach,RServiceOrientation,RStressTolerance,RReliability,RClericalPotential,RSalesPotential,RManagerialPotential,PValidity,PAdjustment,PAmbition,PSociability,PInterpersonalSensitivity,PPrudence,PInquisitive,PLearningApproach,PServiceOrientation,PStressTolerance,PReliability,PClericalPotential,PSalesPotential,PManagerialPotential,HDSDate,RExcitable,RSkeptical,RCautious,RReserved,RLeisurely,RBold,RMischievous,RColorful,RImaginative,RDiligent,RDutiful,RUnlikeness,PExcitable,PSkeptical,PCautious,PReserved,PLeisurely,PBold,PMischievous,PColorful,PImaginative,PDiligent,PDutiful,PUnlikeness,MVPIDate,RAesthetic_Lifestyles,RAesthetic_Beliefs,RAesthetic_Occupational_Prferences,RAesthetic_Aversions,RAesthetic_Preferred_Associates,RAffiliation_Lifestyles,RAffiliation_Beliefs,RAffiliation_Occupational_Prferences,RAffiliation_Aversions,RAffiliation_Preferred_Associates,RAltruistic_Lifestyles,RAltruistic_Beliefs,RAltruistic_Occupational_Prferences,RAltruistic_Aversions,RAltruistic_Preferred_Associates,RCommercial_Lifestyles,RCommercial_Beliefs,RCommercial_Occupational_Prferences,RCommercial_Aversions,RCommercial_Preferred_Associates,RHedonistic_Lifestyles,RHedonistic_Beliefs,RHedonistic_Occupational_Prferences,RHedonistic_Aversions,RHedonistic_Preferred_Associates,RPower_Lifestyles,RPower_Beliefs,RPower_Occupational_Prferences,RPower_Aversions,RPower_Preferred_Associates,RRecognition_Lifestyles,RRecognition_Beliefs,RRecognition_Occupational_Prferences,RRecognition_Aversions,RRecognition_Preferred_Associates,RScientific_Lifestyles,RScientific_Beliefs,RScientific_Occupational_Prferences,RScientific_Aversions,RScientific_Preferred_Associates,RSecurity_Lifestyles,RSecurity_Beliefs,RSecurity_Occupational_Prferences,RSecurity_Aversions,RSecurity_Preferred_Associates,RTradition_Lifestyles,RTradition_Beliefs,RTradition_Occupational_Prferences,RTradition_Aversions,RTradition_Preferred_Associates,RAesthetic,RAffiliation,RAltruistic,RCommercial,RHedonistic,RPower,RRecognition,RScientific,RSecurity,RTradition,PAesthetic,PAffiliation,PAltruistic,PCommercial,PHedonistic,PPower,PRecognition,PScientific,PSecurity,PTradition")] Manual_Hogan_Import manual_hogan_import)
        {
            if (ModelState.IsValid)
            {
                //db.Manual_Hogan_Import.Add(manual_hogan_import);
                //db.SaveChanges();
				 UnitOfWork.IManual_Hogan_ImportRepository.Add(manual_hogan_import);
				 UnitOfWork.Commit();		
                return RedirectToAction("Index");
            }

            return View(manual_hogan_import);
        }

        // GET: /Hogan/Edit/5
        [Log]
        public ActionResult Edit(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Manual_Hogan_Import manual_hogan_import = db.Manual_Hogan_Import.Find(id);
			var model = UnitOfWork.IManual_Hogan_ImportRepository.First(m => m.Id == id);
            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        // POST: /Hogan/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Log]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,ClientID,ClientName,GroupName,Hogan_User_ID,First_Name,Last_Name,Gender,HPIDate,Valid,Empathy,NotAnxious,NoGuilt,Calmness,EvenTempered,NoSomaticComplaint,Trusting,GoodAttachment,Competitive,SelfConfidence,NoDepression,Leadership,Identity,NoSocialAnxiety,LikesParties,LikesCrowds,ExperienceSeeking,Exhibitionistic,Entertaining,EasyToLiveWith,Sensitive,Caring,LikesPeople,NoHostility,Moralistic,Mastery,Virtuous,NotAutonomous,NotSpontaneous,ImpulseControl,AvoidsTrouble,ScienceAbility,Curiosity,ThrillSeeking,IntellectualGames,GeneratesIdeas,Culture,Education,MathAbility,GoodMemory,Reading,RValidity,RAdjustment,RAmbition,RSociability,RInterpersonalSensitivity,RPrudence,RInquisitive,RLearningApproach,RServiceOrientation,RStressTolerance,RReliability,RClericalPotential,RSalesPotential,RManagerialPotential,PValidity,PAdjustment,PAmbition,PSociability,PInterpersonalSensitivity,PPrudence,PInquisitive,PLearningApproach,PServiceOrientation,PStressTolerance,PReliability,PClericalPotential,PSalesPotential,PManagerialPotential,HDSDate,RExcitable,RSkeptical,RCautious,RReserved,RLeisurely,RBold,RMischievous,RColorful,RImaginative,RDiligent,RDutiful,RUnlikeness,PExcitable,PSkeptical,PCautious,PReserved,PLeisurely,PBold,PMischievous,PColorful,PImaginative,PDiligent,PDutiful,PUnlikeness,MVPIDate,RAesthetic_Lifestyles,RAesthetic_Beliefs,RAesthetic_Occupational_Prferences,RAesthetic_Aversions,RAesthetic_Preferred_Associates,RAffiliation_Lifestyles,RAffiliation_Beliefs,RAffiliation_Occupational_Prferences,RAffiliation_Aversions,RAffiliation_Preferred_Associates,RAltruistic_Lifestyles,RAltruistic_Beliefs,RAltruistic_Occupational_Prferences,RAltruistic_Aversions,RAltruistic_Preferred_Associates,RCommercial_Lifestyles,RCommercial_Beliefs,RCommercial_Occupational_Prferences,RCommercial_Aversions,RCommercial_Preferred_Associates,RHedonistic_Lifestyles,RHedonistic_Beliefs,RHedonistic_Occupational_Prferences,RHedonistic_Aversions,RHedonistic_Preferred_Associates,RPower_Lifestyles,RPower_Beliefs,RPower_Occupational_Prferences,RPower_Aversions,RPower_Preferred_Associates,RRecognition_Lifestyles,RRecognition_Beliefs,RRecognition_Occupational_Prferences,RRecognition_Aversions,RRecognition_Preferred_Associates,RScientific_Lifestyles,RScientific_Beliefs,RScientific_Occupational_Prferences,RScientific_Aversions,RScientific_Preferred_Associates,RSecurity_Lifestyles,RSecurity_Beliefs,RSecurity_Occupational_Prferences,RSecurity_Aversions,RSecurity_Preferred_Associates,RTradition_Lifestyles,RTradition_Beliefs,RTradition_Occupational_Prferences,RTradition_Aversions,RTradition_Preferred_Associates,RAesthetic,RAffiliation,RAltruistic,RCommercial,RHedonistic,RPower,RRecognition,RScientific,RSecurity,RTradition,PAesthetic,PAffiliation,PAltruistic,PCommercial,PHedonistic,PPower,PRecognition,PScientific,PSecurity,PTradition")] Manual_Hogan_Import manual_hogan_import)
        {
            if (ModelState.IsValid)
            {
               // db.Entry(manual_hogan_import).State = EntityState.Modified;
                //db.SaveChanges();
                UnitOfWork.IManual_Hogan_ImportRepository.Update(manual_hogan_import);	
				UnitOfWork.Commit();	
				return RedirectToAction("Index");
            }
            return View(manual_hogan_import);
        }

        [Log]
        // GET: /Hogan/Delete/5
        public ActionResult Delete(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Manual_Hogan_Import manual_hogan_import = db.Manual_Hogan_Import.Find(id);
			var model = UnitOfWork.IManual_Hogan_ImportRepository.First(m => m.Id == id);
            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        // POST: /Hogan/Delete/5
        [Log]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(short id)
        {
           // Manual_Hogan_Import manual_hogan_import = db.Manual_Hogan_Import.Find(id);
           // db.Manual_Hogan_Import.Remove(manual_hogan_import);
           // db.SaveChanges();
		   var model = UnitOfWork.IManual_Hogan_ImportRepository.First(m => m.Id == id);
		   UnitOfWork.IManual_Hogan_ImportRepository.Delete(model);	
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
    }
}
