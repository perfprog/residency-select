using System.Web.Mvc;

namespace PPI.Core.Web.Controllers
{
    using PPI.Core.Domain.Abstract;
    public class SurveyController : BaseController
    {
        
        public SurveyController(IUnitOfWork unitOfWork)
            : base(unitOfWork) { }
        //
        // GET: /Survey/
        [Log]
        public ActionResult Index(int? page)
        {
            string Page = "Page" + page.GetValueOrDefault(1).ToString();
            return View(Page);
        }
	}
}