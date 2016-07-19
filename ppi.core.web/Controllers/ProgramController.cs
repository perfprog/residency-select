using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Linq;
using System.Collections.Generic;



namespace PPI.Core.Web.Controllers
{
    using PPI.Core.Domain.Entities;
    using PPI.Core.Domain.Abstract;
    using PPI.Core.Domain.Concrete;
    using PPI.Core.Web.Models;

    [Authorize(Roles = "Admin")]
    [RoutePrefix("Administration/Program")]
    public class ProgramController : BaseController
    {
        //Replace in Scafolding file
        //private CoreContainer db = new CoreContainer();

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="unitOfWork"></param>
        public ProgramController(IUnitOfWork unitOfWork)
            : base(unitOfWork) { }

        #region Program
        // GET: /Program/
        /// <summary>
        /// This action is used to display Specialties list page
        /// </summary>
        /// <returns></returns>
        [Log]
        public ActionResult Index()
        {
            var model = UnitOfWork.IProgramRepository.AsQueryable();
            return View(model);
        }

        // GET: /Program/Details/5
        /// <summary>
        /// This action is used to display Specialties details 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Log]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //replace scafolding
            //Program program = db.Programs.Find(id);
            var model = UnitOfWork.IProgramRepository.AsQueryable().FirstOrDefault(m => m.Id == id);
            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        // GET: /Program/Create
        /// <summary>
        /// This action is used to display create Specialty  page
        /// </summary>
        /// <returns></returns>
        [Log]
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Program/Create
        /// <summary>
        ///  This action is used to create Specialty  
        /// </summary>
        /// <param name="program"></param>
        /// <returns></returns>
        [Log]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ProgramName")] Program program)
        {
            if (ModelState.IsValid)
            {
                //db.Programs.Add(program);
                //db.SaveChanges();
                UnitOfWork.IProgramRepository.Add(program);
                UnitOfWork.Commit();
                //Add the clones           
                UnitOfWork.IGenericRepository.ClonePraciceReports(12, 1, program.Id);
                UnitOfWork.IGenericRepository.ClonePraciceReports(13, 1, program.Id);
                UnitOfWork.IGenericRepository.ClonePraciceReports(14, 1, program.Id);

                return RedirectToAction("Index");
            }

            return View(program);
        }

        // GET: /Program/Edit/5
        /// <summary>
        ///  This action is used to display edit Specialty  page
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Log]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Program program = db.Programs.Find(id);
            var model = UnitOfWork.IProgramRepository.AsQueryable().FirstOrDefault(m => m.Id == id);
            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        // POST: /Program/Edit/5
        /// <summary>
        /// This action is used to edit Specialty  
        /// </summary>
        /// <param name="program"></param>
        /// <returns></returns>
        [Log]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ProgramName")] Program program)
        {
            if (ModelState.IsValid)
            {
                UnitOfWork.IProgramRepository.Update(program);
                UnitOfWork.Commit();
                return RedirectToAction("Index");
            }
            return View(program);
        }

        // GET: /Program/Delete/5
        /// <summary>
        /// This action is used to display delete Specialty  page
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Log]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Program program = db.Programs.Find(id);
            var model = UnitOfWork.IProgramRepository.AsQueryable().FirstOrDefault(m => m.Id == id);
            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        // POST: /Program/Delete/5
        /// <summary>
        /// This action is used to delete Specialty  
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Log]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            // Program program = db.Programs.Find(id);
            // db.Programs.Remove(program);
            // db.SaveChanges();
            var model = UnitOfWork.IProgramRepository.First(m => m.Id == id);
            //model.ProgramSites.Clear();
            //model.PracticeScales.Clear();
            //model.ProgramPracticeReports.Clear();            
            UnitOfWork.IProgramRepository.Delete(model);
            UnitOfWork.Commit();
            return RedirectToAction("Index");
        }
        #endregion

        #region Site
        // GET: /Site/Detail/5
        /// <summary>
        /// This action is used to department details
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Log]
        [Route("Site/Detail/{id:int}")]
        public ActionResult DetailsSite(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //replace scafolding
            //Program program = db.Programs.Find(id);
            var ProgramSite = UnitOfWork.IProgramSiteRepository.AsQueryable().FirstOrDefault(m => m.Id == id);
            var model = new SiteMVPIViewModel();
            model.Site = ProgramSite.Site;
            model.HoganMVPI = UnitOfWork.IProgramSiteHoganMVPIRepository.SingleOrDefault(m => m.ProgramSiteId == id);
            model.ProgramSiteId = id.GetValueOrDefault();
            model.programId = ProgramSite.ProgramId;
            //find the select by the name
            int selectItem = 0;
            if (model.HoganMVPI != null)
                selectItem = UnitOfWork.IHoganMVPIRepository.SingleOrDefault(m => m.FieldName == model.HoganMVPI.HoganFieldName).Id;
            //Pass in the Select
            ViewData["MVPISelect"] = new SelectList(UnitOfWork.IHoganMVPIRepository.AsQueryable(), "Id", "FieldName", selectItem);


            if (ProgramSite == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        // GET: /5/Sites
        /// <summary>
        /// This action is used to display specialties-department list
        /// </summary>
        /// <param name="programId"></param>
        /// <returns></returns>
        [Log]
        [Route("{programId:int}/Sites")]
        public ActionResult IndexSite(int? programId)
        {
            var Program = UnitOfWork.IProgramRepository.SingleOrDefault(m => m.Id == programId);
            var model = new ProgramSiteViewModel();
            if (Program != null)
            {
                model.ProgramSites = Program.ProgramSites.AsQueryable();
                model.ProgramID = programId.GetValueOrDefault(0);
                model.ProgramName = Program.ProgramName;
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewData["Departments"] = new SelectList(new List<SelectListItem>(), "Value", "Text");
            ViewData["Organizations"] = new SelectList(UnitOfWork.IOrganizationRepository.AsQueryable(), "Id", "OrganizationName");
            return View(model);
        }

        /// <summary>
        /// This action is used to get department list- to bind department dropdown based organization selection
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Log]
        [HttpGet]
        [ActionName("FillDepartment")]
        public ActionResult FillDepartment(int? id)
        {
            ViewData["Departments"] = new SelectList(UnitOfWork.ISiteRepository.AsQueryable().Where(m => m.OrganizationId == id), "Id", "FriendlyName");
            return PartialView("_PartialSites", ViewData["Departments"]);
        }

        /// <summary>
        /// This action is used to get department list- to bind department dropdown based organization selection
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ActionName("FillDepartmentAjax")]
        public JsonResult FillDepartmentAjax(int id)
        {
            return Json(new SelectList(UnitOfWork.ISiteRepository.AsQueryable().Where(m => m.OrganizationId == id), "Id", "FriendlyName"));
        }

        /// <summary>
        /// This action is used to add department to specialty
        /// </summary>
        /// <param name="siteId"></param>
        /// <param name="programId"></param>
        /// <returns></returns>
        [Log]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Site/Add")]
        public ActionResult AddSite(int siteId, int programId)
        {
            var program = UnitOfWork.IProgramRepository.SingleOrDefault(m => m.Id == programId);

            var tempProgramSite = program.ProgramSites.Where(m => m.SiteId == siteId).FirstOrDefault();

            if (tempProgramSite == null)
            {
                var programSite = new ProgramSite();
                programSite.ProgramId = programId;
                programSite.SiteId = siteId;
                program.ProgramSites.Add(programSite);
                UnitOfWork.IProgramRepository.Update(program);
                UnitOfWork.Commit();
            }
            else
            {
                TempData["alertMessage"] = "Department already exists. Please select other department.";
            }
            ViewData["Organizations"] = new SelectList(UnitOfWork.IOrganizationRepository.AsQueryable(), "Id", "OrganizationName");
            ViewData["Departments"] = new SelectList(new List<SelectListItem>(), "Value", "Text");
            return RedirectToAction("IndexSite", new { programId = programId });

        }

        /// <summary>
        /// This action is used to delete department from specialty
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Log]
        [Route("Site/Delete/{id:int}")]
        [ActionName("DeleteSite")]
        public ActionResult DeleteSite(int id)
        {
            if (id <= 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Program program = db.Programs.Find(id);
            var model = UnitOfWork.IProgramSiteRepository.AsQueryable().FirstOrDefault(m => m.Id == id);

            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        [Log]
        [Route("Site/Delete/{id:int}")]
        [HttpPost, ActionName("DeleteSite")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteSiteConfirmed(int id)
        {
            // Program program = db.Programs.Find(id);
            // db.Programs.Remove(program);
            // db.SaveChanges();
            var mvpiCheck = UnitOfWork.IProgramSiteHoganMVPIRepository.AsQueryable().FirstOrDefault(m => m.ProgramSiteId == id);
            if (mvpiCheck != null)
                UnitOfWork.IProgramSiteHoganMVPIRepository.Delete(mvpiCheck);
            var model = UnitOfWork.IProgramSiteRepository.First(m => m.Id == id);
            UnitOfWork.IProgramSiteRepository.Delete(model);
            UnitOfWork.Commit();
            return RedirectToAction("IndexSite", new { programId = model.ProgramId });
        }
        #endregion

        #region FitMVPI
        [Log]
        [HttpPost]
        [Route("Site/MVPI/Add")]
        [ValidateAntiForgeryToken]
        public ActionResult AddMVPI(int id, int hoganMVPIId)
        {
            var ProgramSite = UnitOfWork.IProgramSiteRepository.AsQueryable().FirstOrDefault(m => m.Id == id);
            var MVPI = new ProgramSiteHoganMVPI();
            MVPI.HoganFieldName = UnitOfWork.IHoganMVPIRepository.AsQueryable().FirstOrDefault(m => m.Id == hoganMVPIId).FieldName;
            MVPI.ProgramSiteId = id;
            ProgramSite.ProgramSiteHoganMVPIs.Add(MVPI);
            UnitOfWork.Commit();
            var model = new SiteMVPIViewModel();
            model.Site = ProgramSite.Site;
            model.HoganMVPI = MVPI;
            model.ProgramSiteId = ProgramSite.Id;
            int selectItem = 0;
            if (model.HoganMVPI != null)
                selectItem = UnitOfWork.IHoganMVPIRepository.SingleOrDefault(m => m.FieldName == model.HoganMVPI.HoganFieldName).Id;
            ViewData["MVPISelect"] = new SelectList(UnitOfWork.IHoganMVPIRepository.AsQueryable(), "Id", "FieldName", model.HoganMVPI);
            if (ProgramSite == null)
            {
                return HttpNotFound();
            }
            return View("DetailsSite", model);
        }

        [Log]
        [HttpPost]
        [Route("Site/MVPI/Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditMVPI(int id, int hoganMVPIId)
        {
            var ProgramSite = UnitOfWork.IProgramSiteRepository.SingleOrDefault(m => m.Id == id);
            var MVPI = UnitOfWork.IProgramSiteHoganMVPIRepository.SingleOrDefault(m => m.ProgramSiteId == id);
            MVPI.HoganFieldName = UnitOfWork.IHoganMVPIRepository.SingleOrDefault(m => m.Id == hoganMVPIId).FieldName;
            UnitOfWork.IProgramSiteHoganMVPIRepository.Update(MVPI);
            UnitOfWork.Commit();
            var model = new SiteMVPIViewModel();
            model.Site = ProgramSite.Site;
            model.HoganMVPI = MVPI;
            model.ProgramSiteId = id;
            int selectItem = 0;
            if (model.HoganMVPI != null)
                selectItem = UnitOfWork.IHoganMVPIRepository.SingleOrDefault(m => m.FieldName == model.HoganMVPI.HoganFieldName).Id;
            ViewData["MVPISelect"] = new SelectList(UnitOfWork.IHoganMVPIRepository.AsQueryable(), "Id", "FieldName", selectItem);
            if (ProgramSite == null)
            {
                return HttpNotFound();
            }
            return View("DetailsSite", model);
        }
        [Log]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public void DeleteMVPI(int id, int ProgramSiteid)
        {
            var MVPI = UnitOfWork.IProgramSiteHoganMVPIRepository.Single(m => m.Id == id);
            UnitOfWork.IProgramSiteHoganMVPIRepository.Delete(MVPI);
            UnitOfWork.Commit();
        }
        #endregion

        [Log]
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
