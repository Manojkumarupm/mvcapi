using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using WebAPIDemo.Controllers;
using WebAPIDemo.Models;

namespace WebAPIDemo.Tests.Controllers
{
    [TestClass]
    public class POMasterControllerTest
    {
        [TestMethod]
        public void GetBySpecificId()
        {
            // Set up Prerequisites   
            var controller = new POMasterController();
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();
            // Act on Test  
            var response = controller.Get("PO1");
            // Assert the result   
           Assert.IsNotNull(response.Content);
        }

        [TestMethod]
        public void GetAllData()
        {
            // Set up Prerequisites   
            var controller = new POMasterController();
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();
            // Act on Test  
            var response = controller.Get();
            // Assert the result  
            Assert.IsTrue(response.Count() > 0);
        }
        [TestMethod]
        public void GetErrorResponseCheck()
        {
            // Set up Prerequisites   
            var controller = new POMasterController();
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();
            // Act on Test  
            var response = controller.Get("uC1");
            // Assert the result  
            Assert.IsTrue(!response.IsSuccessStatusCode);
        }
        [TestMethod]
        public void GetResponseCheck()
        {
            // Set up Prerequisites   
            var controller = new POMasterController();
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();
            // Act on Test  
            var response = controller.Get("PO1 ");
            // Assert the result  
            Assert.IsTrue(response.IsSuccessStatusCode);
        }
        [TestMethod]
        public void GetNotFoundResponseCheck()
        {
            // Set up Prerequisites   
            var controller = new POMasterController();
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();
            // Act on Test  
            var response = controller.Get("SUP1");
            // Assert the result  
            Assert.IsTrue(response.StatusCode == HttpStatusCode.NotFound);
        }

        [TestMethod]
        public void PostBadRequest()
        {
            // Set up Prerequisites   
            var controller = new POMasterController();
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();

            // Act on Test  
            List<POInformation> poi = new List<POInformation>();
            poi.Add(new POInformation { PONO = "PO8", PODATE = DateTime.Now, SUPLNO = "C102", ITCODE = "ITM6", QTY = 2, ITDESC = null, SUPLNAME = null });
            var response = controller.Post(poi);
            // Assert the result  

            Assert.IsTrue(response.StatusCode == HttpStatusCode.BadRequest);
            // Assert.AreEqual(i.ITRATE, item.ITRATE);
        }
        [TestMethod]
        public void PostRequest()
        {
            // Set up Prerequisites   
            var controller = new POMasterController();
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();

            // Act on Test  
            List<POInformation> poi = new List<POInformation>();
            poi.Add(new POInformation { PONO = "PO8 ", PODATE = DateTime.Now, SUPLNO = "C2", ITCODE = "ITM6", QTY = 2, ITDESC = null, SUPLNAME = null });
            
            var response = controller.Post(poi);
            // Assert the result  

            Assert.IsTrue(response.StatusCode == HttpStatusCode.Created);
            // Assert.AreEqual(i.ITRATE, item.ITRATE);
        }
        [TestMethod]
        public void PutRequest()
        {
            // Set up Prerequisites   
            var controller = new POMasterController();
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();

            // Act on Test  
            List<POInformation> poi = new List<POInformation>();
            poi.Add(new POInformation { PONO = "PO8 ", PODATE = DateTime.Now, SUPLNO = "C3", ITCODE = "ITM6", QTY = 2, ITDESC = null, SUPLNAME = null });
            
            var response = controller.Put(poi);
            // Assert the result  

            Assert.IsTrue(response.StatusCode == HttpStatusCode.OK);
        }
        [TestMethod]
        public void PutBadRequest()
        {
            // Set up Prerequisites   
            var controller = new POMasterController();
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();

            // Act on Test  
            List<POInformation> poi = new List<POInformation>();
            poi.Add(new POInformation { PONO = "PO8", PODATE = DateTime.Now, SUPLNO = "C35", ITCODE = "ITM6", QTY = 2, ITDESC = null, SUPLNAME = null });

            var response = controller.Put(poi);
            // Assert the result  

            Assert.IsTrue(response.StatusCode == HttpStatusCode.BadRequest);
        }
        [TestMethod]
        public void DeleteNotFoundRequest()
        {
            // Set up Prerequisites   
            var controller = new POMasterController();
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();

            // Act on Test  
            var response = controller.Delete("IT09");
            // Assert the result  

            Assert.IsTrue(response.StatusCode == HttpStatusCode.NotFound);
        }
        [TestMethod]
        public void DeleteRequest()
        {
            // Set up Prerequisites   
            var controller = new POMasterController();
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();

            // Act on Test  
            var response = controller.Delete("PO8");
            // Assert the result  

            Assert.IsTrue(response.StatusCode == HttpStatusCode.OK);
        }
    }
}
