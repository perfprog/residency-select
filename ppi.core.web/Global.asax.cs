using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Data.Entity;
using PPI.Core.Web.Models;
using Ninject;
using Ninject.Parameters;
using Ninject.Syntax;
using PPI.Core.Web.Infrastructure;



namespace PPI.Core.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public IKernel ninjectKernal;
        protected void Application_Start()
        {            
            
            //IoC Kernel;


            ninjectKernal = new StandardKernel();
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            ControllerBuilder.Current.SetControllerFactory(new PPI.Core.Web.Infrastructure.NinjectControllerFactory(ninjectKernal));                       
            Database.SetInitializer<PPI.Core.Web.Models.ApplicationDbContext>(null);
            
            
        }
    }
}
