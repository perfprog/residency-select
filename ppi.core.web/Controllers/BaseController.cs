using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace PPI.Core.Web.Controllers
{
    using PPI.Core.Domain.Abstract;
    using PPI.Core.Domain.Entities;
    public partial class BaseController : Controller
    {
        protected IUnitOfWork UnitOfWork;
        public BaseController(IUnitOfWork unitOfWork )
        {
            UnitOfWork = unitOfWork;
        }

        public int CurrentCulture
        {
            get { return int.Parse(PPI.Core.Web.Infrastructure.Utility.GetCookie("cultureId")); }            
        }

        public string CurrentPageSize
        {
            get { return PPI.Core.Web.Infrastructure.Utility.GetCookie("pageSize"); }
        }        
        public int CurrentSite
        {
            get { return int.Parse(PPI.Core.Web.Infrastructure.Utility.GetCookie("DefaultSite")); }            
        }
        public int CurrentEvent
        {
            get {
                var cookieValue = PPI.Core.Web.Infrastructure.Utility.GetCookie("CurrentEvent");
                int retVal = -1;
                if (!String.IsNullOrWhiteSpace(cookieValue))
                {
                    retVal = int.Parse(cookieValue); 
                }
                return retVal;
                }

            set { PPI.Core.Web.Infrastructure.Utility.SetCookie("CurrentEvent", value.ToString(), TimeSpan.FromDays(1)); }
        }

        public SelectList Events
        { 
            get
            {
                IEnumerable<Event> filterEvents = null;
                if (User.IsInRole("SiteCordinator"))
                    {
                        filterEvents = UnitOfWork.IEventRepository.AsQueryable().Where(m => m.ProgramSite.SiteId == CurrentSite).OrderBy(m => m.Name.Trim());
                    }
                else if (User.IsInRole("Admin") || User.IsInRole("J3PAdmin"))
                    {
                        filterEvents = UnitOfWork.IEventRepository.AsQueryable().OrderBy(m => m.Name.Trim());
                    }
                if (filterEvents.Count() > 0)
                 {
                     var UploadEvents = new List<SelectListItem>();
                     //Globally add Select Event with a -1 value
                     var GlobalItem = new SelectListItem();
                     GlobalItem.Text = " --  No Event Selected --";
                     GlobalItem.Value = "-1";
                     UploadEvents.Add(GlobalItem);
                     ///    
                     foreach (var item in filterEvents)
                     {
                         var newItem = new SelectListItem();
                         newItem.Text = item.Name + "-" + item.ProgramSite.Site.FriendlyName + "     [Created " + item.CreateDate.ToShortDateString()  +"]";
                         newItem.Value = item.Id.ToString();
                         UploadEvents.Add(newItem);
                     }          
                        
                    return new SelectList(UploadEvents, "Value", "Text",GlobalItem);
                 }
                return new SelectList(filterEvents);
            }
        }

        protected override IAsyncResult BeginExecute(System.Web.Routing.RequestContext requestContext, AsyncCallback callback, object state)
        {
            //Culture Cookie
            var cultureName = PPI.Core.Web.Infrastructure.Utility.GetCookie("cultureId");
            if (String.IsNullOrEmpty(cultureName))
            {

                var localcultureName = requestContext.HttpContext.Request.UserLanguages != null && requestContext.HttpContext.Request.UserLanguages.Length > 0 ?
                requestContext.HttpContext.Request.UserLanguages[0] : "en-US"; //default to en-US
                cultureName = UnitOfWork.ICultureRepository.AsQueryable().FirstOrDefault(m => m.Value == localcultureName).Id.ToString();
                PPI.Core.Web.Infrastructure.Utility.SetCookie("cultureId", cultureName, TimeSpan.FromDays(1));
            }
            //Page SIze
            if (CurrentPageSize == string.Empty)
                PPI.Core.Web.Infrastructure.Utility.SetCookie("pageSize", "10000", TimeSpan.FromDays(1));
            //Default Site
          
            
            return base.BeginExecute(requestContext, callback, state);
        }
        protected override IAsyncResult BeginExecuteCore(AsyncCallback callback, object state)
        {

            ViewBag.CultureId = CurrentCulture;
            ViewBag.PageSize = CurrentPageSize;
            if (!string.IsNullOrEmpty(User.Identity.GetUserId()))
            {
                var userId = User.Identity.GetUserId();
                var item = UnitOfWork.ISiteUserRepository.AsQueryable().FirstOrDefault(m => m.AspNetUsersId == userId);
                if (item != null)
                {                    
                    PPI.Core.Web.Infrastructure.Utility.SetCookie("DefaultSite", item.SiteId.ToString(), TimeSpan.FromDays(1));
                }
                else
                {
                    PPI.Core.Web.Infrastructure.Utility.SetCookie("DefaultSite", "-1", TimeSpan.FromDays(1));
                }
                
            }            
            return base.BeginExecuteCore(callback, state);
        }

        public ActionResult SetPageSize(int pageSize,string redirectTo,string searchFilter)
        {
            PPI.Core.Web.Infrastructure.Utility.SetCookie("pageSize", pageSize.ToString(), TimeSpan.FromDays(1));
            string urlRedirect = "Index";
            if (!String.IsNullOrEmpty(redirectTo))
                urlRedirect = redirectTo;
            if (!String.IsNullOrEmpty(searchFilter))
            { 
                return RedirectToAction(urlRedirect, new {@search = searchFilter});
            }
            else
                return RedirectToAction(urlRedirect);
                //Clear the Session information if you switch events           
        }

        public ActionResult SetCurrentEvent(int EventId, string redirectTo)
        {
            PPI.Core.Web.Infrastructure.Utility.SetCookie("CurrentEvent", EventId.ToString(), TimeSpan.FromDays(1));
            string urlRedirect = "Index";
            if (redirectTo != "" || redirectTo != null)
                urlRedirect = redirectTo;
            return RedirectToAction(urlRedirect);                   
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                UnitOfWork.Dispose();
            }
            base.Dispose(disposing);
        }
    }
    
}