using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PPI.Core.Web;
using PPI.Core.Web.Controllers;
using Moq;
using System.Web;
using System.Web.Routing;
using PPI.Core.Web.Models;
using System.IO;

namespace PPI.Core.Web.Tests.Controllers
{
    [TestClass]
    public class AdministrationControllerTest
    {
        [TestMethod]
        public void Index()
        {
            //AdministrationController AdminController = new AdministrationController();
            //ViewResult result = AdminController.Index() as ViewResult;
            //Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Reports()
        {            
            ReportsController ReportsController = new ReportsController();
            ViewResult result = ReportsController.Index() as ViewResult;
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void FakeUploadFiles()
        {

            //We'll need mocks (fake) of Context, Request and a fake PostedFile
            var request = new Mock<HttpRequestBase>();
            var context = new Mock<HttpContextBase>();
            var postedfile = new Mock<HttpPostedFileBase>();

            //Someone is going to ask for Request.File and we'll need a mock (fake) of that.
            var postedfilesKeyCollection = new Mock<HttpFileCollectionBase>();
            var fakeFileKeys = new List<string>() { "file" };

            //OK, Mock Framework! Expect if someone asks for .Request, you should return the Mock!
            context.Setup(ctx => ctx.Request).Returns(request.Object);
            
            //OK, Mock Framework! Expect if someone starts foreach'ing their way over .Files, give them the fake strings instead!
            postedfilesKeyCollection.Setup(keys => keys.GetEnumerator()).Returns(fakeFileKeys.GetEnumerator());

            //OK, Mock Framework! Expect if someone asks for file you give them the fake!
            postedfilesKeyCollection.Setup(keys => keys[0]).Returns(postedfile.Object);
            
            //OK, Mock Framework! Give back these values when asked, and I will want to Verify that these things happened
            postedfile.Setup(f => f.ContentLength).Returns(8192).Verifiable();
            postedfile.Setup(f => f.FileName).Returns("foo.doc").Verifiable();

            //OK, Mock Framework! Someone is going to call SaveAs, but only once!
            postedfile.Setup(f => f.SaveAs(It.IsAny<string>())).Verifiable();

            //OK, Mock Framework! Expect if someone asks for .Files, you should return the Mock with fake keys!
            request.Setup(req => req.Files).Returns(postedfilesKeyCollection.Object);

            ReportsController controller = new ReportsController();            
            //Set the controller's context to the mock! (fake)
            controller.ControllerContext = new ControllerContext(context.Object, new RouteData(), controller);

            //DO IT!
            ViewResult result = controller.UploadFiles() as ViewResult;

            //Now, go make sure that the Controller did its job
            var uploadedResult = result.ViewData.Model as List<UploadFileModel>;
            //Assumes default settings path
            //C:\Users\nrm10_000\Documents\Visual Studio 2013\Projects\ppi.core.web\ppi.core.web.tests\bin\Debug\Uploadfiles\foo.doc

            Assert.AreEqual(Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"Uploadfiles", "foo.doc"), uploadedResult[0].Name);
            Assert.AreEqual(8192, uploadedResult[0].Length);

            postedfile.Verify();
          
  
        }
    }

}
