using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Linq;
using System.Collections.Generic;
using PPI.Core.Web.Models;


namespace PPI.Core.Web.Controllers
{
    using PPI.Core.Domain.Entities;
    using PPI.Core.Domain.Abstract;

    [Authorize(Roles = "Admin")]
    public class SiteController : BaseController
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="unitOfWork"></param>
        public SiteController(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {

        }

        // GET: /Site/
        /// <summary>
        /// This action is used to display Department list page
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            var model = UnitOfWork.ISiteRepository.AsQueryable();
            return View(model);
        }

        /// <summary>
        /// Method to get Organization List
        /// </summary>
        /// <returns>List<SelectListItem></returns>
        private List<SelectListItem> OrganizationsLists(int? organizationId)
        {
            var Organizations = UnitOfWork.IOrganizationRepository.AsQueryable();
            var OrganizationsList = new List<SelectListItem>();

            var selectItem = new SelectListItem();
            selectItem.Text = "Select Organization";
            OrganizationsList.Add(selectItem);

            foreach (var item in Organizations)
            {
                var newItem = new SelectListItem();
                newItem.Text = item.OrganizationName;
                newItem.Value = item.Id.ToString();
                if (organizationId != null && item.Id == organizationId)
                    newItem.Selected = true;
                OrganizationsList.Add(newItem);
            }
            return OrganizationsList;
        }


        // GET: /Site/Details/5
        /// <summary>
        /// This action is used to display Department details page
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var model = UnitOfWork.ISiteRepository.First(m => m.Id == id);
            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        // GET: /Site/Create
        /// <summary>
        /// This action is used to display create Department  page
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            ViewBag.SiteOrganizations = new SelectList(OrganizationsLists(null), "Value", "Text");
            return View();
        }

        // POST: /Site/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// This action is used to create Department 
        /// </summary>
        /// <param name="site"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,OrganizationId,SiteName,FriendlyName,BrandingColor,BrandingLogo,BrandingBackground")] Site site)
        {
            if (ModelState.IsValid)
            {

                var department = UnitOfWork.ISiteRepository.First(x => x.SiteName == site.SiteName && x.OrganizationId == site.OrganizationId);
                if (department == null) // update
                {
                    UnitOfWork.ISiteRepository.Add(site);
                    UnitOfWork.Commit();
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["alertMessage"] = "Department already exists. Please try with other name.";
                }
            }
            ViewBag.SiteOrganizations = new SelectList(OrganizationsLists(null), "Value", "Text");
            return View(site);
        }

        // GET: /Site/Edit/5
        /// <summary>
        /// This action is used to display Department edit page
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var model = UnitOfWork.ISiteRepository.First(m => m.Id == id);

            if (model == null)
            {
                return HttpNotFound();
            }
            ViewBag.SiteOrganizations = new SelectList(OrganizationsLists(model.OrganizationId), "Value", "Text");
            return View(model);
        }

        // POST: /Site/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// This action is used to edit Department 
        /// </summary>
        /// <param name="site"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,OrganizationId,SiteName,FriendlyName,BrandingColor,BrandingLogo,BrandingLogoMimeType,BrandingBackground,BrandingBackgroundMimeType")] Site site)
        {
            var i = 0;
            if (ModelState.IsValid)
            {
                foreach (string key in Request.Files)
                {

                    HttpPostedFileBase hpf = Request.Files[i] as HttpPostedFileBase;
                    if (hpf.ContentLength != 0)
                    {
                        if (key == "File1")
                        {
                            using (var memoryArray = new System.IO.MemoryStream())
                            {
                                hpf.InputStream.CopyTo(memoryArray);
                                site.BrandingLogo = memoryArray.ToArray();
                                site.BrandingLogoMimeType = hpf.ContentType;
                            }
                        }
                        else if (key == "File2")
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

                var department = UnitOfWork.ISiteRepository.First(x => x.SiteName == site.SiteName && x.Id != site.Id);
                if (department == null) // Checking duplicate
                {
                    UnitOfWork.ISiteRepository.Update(site);
                    UnitOfWork.Commit();
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["alertMessage"] = "Department already exists. Please try with other name.";
                }
            }
            ViewBag.SiteOrganizations = new SelectList(OrganizationsLists(null), "Value", "Text");
            return View(site);
        }

        // GET: /Site/Delete/5
        /// <summary>
        /// This action is used to display delete Department page
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var model = UnitOfWork.ISiteRepository.First(m => m.Id == id);
            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        // POST: /Site/Delete/5
        /// <summary>
        /// This action is used to delete Department 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            // Site Site = db.Sites.Find(id);
            // db.Sites.Remove(Site);
            // db.SaveChanges();
            var model = UnitOfWork.ISiteRepository.First(m => m.Id == id);
            UnitOfWork.ISiteRepository.Delete(model);
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
