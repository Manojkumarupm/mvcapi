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
    public class SupplierControllerTest
    {
        [TestMethod]
        public void GetBySpecificId()
        {
            // Set up Prerequisites   
            var controller = new SupplierController();
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();
            // Act on Test  
            var response = controller.Get("C1");
            // Assert the result  
            SUPPLIER item;
            Assert.IsTrue(response.TryGetContentValue<SUPPLIER>(out item));
            Assert.AreEqual("Chain Merchant", item.SUPLNAME);
        }

        [TestMethod]
        public void GetAllData()
        {
            // Set up Prerequisites   
            var controller = new SupplierController();
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
            var controller = new SupplierController();
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
            var controller = new SupplierController();
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();
            // Act on Test  
            var response = controller.Get("C1");
            // Assert the result  
            Assert.IsTrue(response.IsSuccessStatusCode);
        }
        [TestMethod]
        public void GetNotFoundResponseCheck()
        {
            // Set up Prerequisites   
            var controller = new SupplierController();
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
            var controller = new SupplierController();
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();
            
            // Act on Test  
            var response = controller.Post(new SUPPLIER { SUPLNO = "C10", SUPLNAME = "Glitter & Corporation Office", SUPLADDR = "Glitter Street, Tambaram" });
            // Assert the result  

            Assert.IsTrue(response.StatusCode == HttpStatusCode.BadRequest);
            // Assert.AreEqual(i.ITRATE, item.ITRATE);
        }
        [TestMethod]
        public void PostRequest()
        {
            // Set up Prerequisites   
            var controller = new SupplierController();
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();

            // Act on Test  
            var response = controller.Post(new SUPPLIER { SUPLNO = "C10", SUPLNAME = "Glitter & CO", SUPLADDR = "Street,TBM" });
            // Assert the result  

            Assert.IsTrue(response.StatusCode == HttpStatusCode.Created);
            // Assert.AreEqual(i.ITRATE, item.ITRATE);
        }
        [TestMethod]
        public void PutRequest()
        {
            // Set up Prerequisites   
            var controller = new SupplierController();
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();

            // Act on Test  
            var response = controller.Put(new SUPPLIER { SUPLNO = "C10", SUPLNAME = "Gliter & CO", SUPLADDR = "Glitter Street" });
            // Assert the result  

            Assert.IsTrue(response.StatusCode == HttpStatusCode.OK);
        }
        [TestMethod]
        public void PutBadRequest()
        {
            // Set up Prerequisites   
            var controller = new SupplierController();
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();

            // Act on Test  
            var response = controller.Put(new SUPPLIER { SUPLNO = "C10", SUPLNAME = "Gliter & CO Exceeds Maximum Limit", SUPLADDR = "Glitter Street" });
            // Assert the result  

            Assert.IsTrue(response.StatusCode == HttpStatusCode.BadRequest);
        }
        [TestMethod]
        public void DeleteNotFoundRequest()
        {
            // Set up Prerequisites   
            var controller = new SupplierController();
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
            var controller = new SupplierController();
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();

            // Act on Test  
            var response = controller.Delete("C10");
            // Assert the result  

            Assert.IsTrue(response.StatusCode == HttpStatusCode.OK);
        }
    }
}
