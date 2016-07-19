using System.Net;
using System.Web.Mvc;


namespace PPI.Core.Web.Controllers
{
    using PPI.Core.Domain.Entities;
    using PPI.Core.Domain.Abstract;
    
    public class PracticeScaleController : BaseController
    {
        

		//Replace in Scafolding file
		//private CoreContainer db = new CoreContainer();

        public PracticeScaleController(IUnitOfWork unitOfWork)
            : base(unitOfWork) { }
        

        // GET: /PracticeScale/
        public ActionResult Index()
        {
			// replace in scafoding file
            //var practicescales = db.PracticeScales.Include(p => p.HoganField).Include(p => p.PracticeCategory).Include(p => p.PracticeLevel);
			var model = UnitOfWork.IPracticeScaleRepository.AsQueryable();
            
			return View(model);
        }

        // GET: /PracticeScale/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
			//replace scafolding
            //PracticeScale practicescale = db.PracticeScales.Find(id);
			var model = UnitOfWork.IPracticeScaleRepository.First(m => m.Id == id);
            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        // GET: /PracticeScale/Create
        public ActionResult Create()
        {
       
	        ViewBag.HoganFieldId = new SelectList(UnitOfWork.IHoganFieldRepository.AsQueryable(), "Id", "FieldName");
       
	        ViewBag.PracticeCategoryId = new SelectList(UnitOfWork.IPracticeCategoryRepository.AsQueryable(), "Id", "Name");
       
	        ViewBag.PracticeLevelId = new SelectList(UnitOfWork.IPracticeLevelRepository.AsQueryable(), "Id", "Name");
            return View();
        }

        // POST: /PracticeScale/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,HoganFieldId,PracticeCategoryId,PracticeLevelId,LowerBound,UpperBound")] PracticeScale practicescale)
        {
            if (ModelState.IsValid)
            {
                //db.PracticeScales.Add(practicescale);
                //db.SaveChanges();
				 UnitOfWork.IPracticeScaleRepository.Add(practicescale);
				 UnitOfWork.Commit();		
                return RedirectToAction("Index");
            }

            ViewBag.HoganFieldId = new SelectList(UnitOfWork.IHoganFieldRepository.AsQueryable(), "Id", "FieldName", practicescale.HoganFieldId);
            ViewBag.PracticeCategoryId = new SelectList(UnitOfWork.IPracticeCategoryRepository.AsQueryable(), "Id", "Name", practicescale.PracticeCategoryId);
            ViewBag.PracticeLevelId = new SelectList(UnitOfWork.IPracticeLevelRepository.AsQueryable(), "Id", "Name", practicescale.PracticeLevelId);
            return View(practicescale);
        }

        // GET: /PracticeScale/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //PracticeScale practicescale = db.PracticeScales.Find(id);
			var model = UnitOfWork.IPracticeScaleRepository.First(m => m.Id == id);
            if (model == null)
            {
                return HttpNotFound();
            }
            ViewBag.HoganFieldId = new SelectList(UnitOfWork.IHoganFieldRepository.AsQueryable(), "Id", "FieldName", model.HoganFieldId);
            ViewBag.PracticeCategoryId = new SelectList(UnitOfWork.IPracticeCategoryRepository.AsQueryable(), "Id", "Name", model.PracticeCategoryId);
            ViewBag.PracticeLevelId = new SelectList(UnitOfWork.IPracticeLevelRepository.AsQueryable(), "Id", "Name", model.PracticeLevelId);
            return View(model);
        }

        // POST: /PracticeScale/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,HoganFieldId,PracticeCategoryId,PracticeLevelId,LowerBound,UpperBound")] PracticeScale practicescale)
        {
            if (ModelState.IsValid)
            {
               // db.Entry(practicescale).State = EntityState.Modified;
                //db.SaveChanges();
                UnitOfWork.IPracticeScaleRepository.Update(practicescale);	
				UnitOfWork.Commit();	
				return RedirectToAction("Index");
            }
            ViewBag.HoganFieldId = new SelectList(UnitOfWork.IHoganFieldRepository.AsQueryable(), "Id", "FieldName", practicescale.HoganFieldId);
            ViewBag.PracticeCategoryId = new SelectList(UnitOfWork.IPracticeCategoryRepository.AsQueryable(), "Id", "Name", practicescale.PracticeCategoryId);
            ViewBag.PracticeLevelId = new SelectList(UnitOfWork.IPracticeLevelRepository.AsQueryable(), "Id", "Name", practicescale.PracticeLevelId);
            return View(practicescale);
        }

        // GET: /PracticeScale/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //PracticeScale practicescale = db.PracticeScales.Find(id);
			var model = UnitOfWork.IPracticeScaleRepository.First(m => m.Id == id);
            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        // POST: /PracticeScale/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
           // PracticeScale practicescale = db.PracticeScales.Find(id);
           // db.PracticeScales.Remove(practicescale);
           // db.SaveChanges();
		   var model = UnitOfWork.IPracticeScaleRepository.First(m => m.Id == id);
		   UnitOfWork.IPracticeScaleRepository.Delete(model);	
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
