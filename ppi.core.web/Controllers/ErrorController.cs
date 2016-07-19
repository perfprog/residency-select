using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Text;
using PPI.Core.Web.Models;
using PPI.Core.Domain.Entities;
using PPI.Core.Domain.Abstract;
using System.IO;
using System.Web.UI;
using System.Web.Routing;
using System.Configuration;
using Microsoft.AspNet.Identity;                    //for usermanager
using Microsoft.AspNet.Identity.EntityFramework;    //for usermanager

namespace PPI.Core.Web.Controllers
{    
    [Authorize(Roles = "Admin,SiteCordinator,J3PAdmin")]
    public class ErrorController : BaseController
    {
        public ErrorController(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public ActionResult Error()
        {
            HandleErrorInfo model = null;

            //this action is called as a result of ExceptionSvcHelper.HandleFatalException() call, which does a redirect.
            //so exception info has been logged but is not preserved in state to be displayed here.  additional work is required.
                        
            return (View(model));
        }
        
        public ActionResult Error404()
        {
            Response.StatusCode = 404;
            Response.TrySkipIisCustomErrors = true;
            string badUrl;            

            try
            {
                badUrl = Request.QueryString["aspxerrorpath"];
            }
            catch
            {
                badUrl = "Unknown";
            }

            string errMsg = string.Format("Error 404: The following page could not be found: {0}", badUrl);
            ExceptionSvcHelper.HandleFatalExceptionNoRedirect(new ApplicationException(errMsg));
            return (View());

            //NOTE: Can't get the bad url via Server.GetLastError here.  A redirect has already been done.
        }        
    }
}
        
        