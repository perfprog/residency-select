using System.Net;
using System.Web;
using System.Web.Mvc;

using System.Linq;


namespace PPI.Core.Web.Controllers
{
    using PPI.Core.Domain.Entities;
    using PPI.Core.Domain.Abstract;

    [Authorize(Roles = "Admin")]
    public class OrganizationController : BaseController
    {

        public OrganizationController(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {

        }

        //
        // GET: /Organization/
        /// <summary>
        /// This action is used to display organization list page
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            var model = UnitOfWork.IOrganizationRepository.AsQueryable();
            return View(model);
        }

        // GET: /Organization/Create
        /// <summary>
        /// This action is used to display create organization page
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            return View();
        }


        // POST: /Organization/Create
        /// <summary>
        /// This action is used to create organization
        /// </summary>
        /// <param name="organization"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
         public ActionResult Create([Bind(Include = "Id,OrganizationName")] Organization organization)
        {
            if (ModelState.IsValid)
            {
                var org = UnitOfWork.IOrganizationRepository.First(x => x.OrganizationName == organization.OrganizationName);
                if (org == null) // update
                {
                    UnitOfWork.IOrganizationRepository.Add(organization);
                    UnitOfWork.Commit();
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["alertMessage"] = "Organization already exists. Please try with other name.";
                }

            }
            return View(organization);
        }


        // GET: /Organization/Edit/5
        /// <summary>
        /// This action is used to display edit organization page
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

            var model = UnitOfWork.IOrganizationRepository.AsQueryable().FirstOrDefault(m => m.Id == id);
            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }


        // POST: /Organization/Edit/5
       /// <summary>
        /// This action is used to  edit organization 
       /// </summary>
       /// <param name="organization"></param>
       /// <returns></returns>
        [Log]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,OrganizationName")] Organization organization)
        {
            if (ModelState.IsValid)
            {
                var org = UnitOfWork.IOrganizationRepository.First(x => x.OrganizationName == organization.OrganizationName && x.Id != organization.Id);
                if (org == null) // update
                {
                    UnitOfWork.IOrganizationRepository.Update(organization);
                    UnitOfWork.Commit();
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["alertMessage"] = "Organization already exists. Please try with other name.";
                }
            }
            return View(organization);
        }



        // GET: /Organization/Delete/5
        /// <summary>
        /// This action is used to display delete organization page
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

            var model = UnitOfWork.IOrganizationRepository.AsQueryable().FirstOrDefault(m => m.Id == id);
            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        // POST: /Organization/Delete/5
        /// <summary>
        /// This action is used to  delete organization 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Log]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {

            var model = UnitOfWork.IOrganizationRepository.First(m => m.Id == id);

            UnitOfWork.IOrganizationRepository.Delete(model);
            UnitOfWork.Commit();
            return RedirectToAction("Index");
        }
    }
}