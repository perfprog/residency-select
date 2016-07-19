using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PPI.Core.Domain.Entities;
using PPI.Core.Domain.Abstract;

namespace PPI.Core.Web.Controllers
{
    public class ImagesController : BaseController
    {
        // GET: Images
        

        public ImagesController(IUnitOfWork unitOfWork)
            : base(unitOfWork) { }
        
        [HttpGet]
        public ActionResult GetSiteImage(int Id)
        {
            var Site = UnitOfWork.ISiteRepository.First(m => m.Id == Id);
            return new FileContentResult(Site.BrandingLogo, "image"); 
        }
    }
}