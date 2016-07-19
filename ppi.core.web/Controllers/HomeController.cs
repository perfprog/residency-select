using System.Web.Mvc;

namespace PPI.Core.Web.Controllers
{
    using PPI.Core.Domain.Abstract;
    public class HomeController : BaseController
    {
        public HomeController(IUnitOfWork unitOfWork)
            : base(unitOfWork) { }
        [Log]
        public ActionResult Index()
        {
            return RedirectToAction("Login", "Account");
        }
        [Log]
        public ActionResult About()
        {
            ViewBag.Message = "Residency Select - Science in the Selection";

            return RedirectToAction("Login","Account");
        }
        [Log]
        public ActionResult Contact(int id, string name)
        {           
            return View();
        }
    }
}