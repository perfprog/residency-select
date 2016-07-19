using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ninject;
using Ninject.Parameters;
using Ninject.Syntax;
using System.Configuration;
using PPI.Core.Domain.Abstract;
using PPI.Core.Domain.Concrete;
using PPI.Core.Domain.Entities;
using Moq;



namespace PPI.Core.Web.Infrastructure
{
    public class NinjectControllerFactory : DefaultControllerFactory
    {
        private IKernel ninjectKernal;
        [Log]
        public NinjectControllerFactory(IKernel iocKernal)
        {
            ninjectKernal = iocKernal;
            AddBindings();
        }        
        protected override IController GetControllerInstance(System.Web.Routing.RequestContext requestContext, Type controllerType)
        {
            return controllerType == null ? null : (IController)ninjectKernal.Get(controllerType);
        }
        [Log]
        private void AddBindings()
        {
            try
            {               
                #region Bindings                
                ninjectKernal.Bind<IUnitOfWork>().To<EfUnitOfWork>();                
                #endregion

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


    }    
}