using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ViaGoGo.CD.Controllers;
using System.Web.Mvc;

namespace ViaGoGo.CD.Tests
{
    [TestClass]
    public class IndexView
    {
        [TestMethod]
        public void TestMethod1()
        {
            var controller = new HomeController();
            var result = controller.Index() as ViewResult;
            Assert.AreEqual("", result.ViewName);
        }
    }
}
