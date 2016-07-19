using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Ninject;
using Ninject.Parameters;
using Ninject.Syntax;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using PPI.Core.Web.Models;
using PPI.Core.Domain.Entities;
using PPI.Core.Domain.Concrete;
using PPI.Core.Domain.Abstract;

namespace PPI.Core.Web.Infrastructure
{
    public class GetEventListAttribute : ActionFilterAttribute, IActionFilter
    {
        
        void IActionFilter.OnActionExecuted(ActionExecutedContext filterContext)
        {
            
            
        }

        void IActionFilter.OnActionExecuting(ActionExecutingContext filterContext)
        {
            //
        }
    }
}