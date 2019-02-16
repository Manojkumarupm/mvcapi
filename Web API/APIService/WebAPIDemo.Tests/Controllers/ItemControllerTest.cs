using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebAPIDemo;
using WebAPIDemo.Controllers;
using Moq;
using WebAPIDemo.Models;
using System.Net;

namespace WebAPIDemo.Tests.Controllers
{
    [TestClass]
    public class ItemControllerTest
    {
        [TestMethod]
        public void GetBySpecificId()
        {
            // Set up Prerequisites   
            var controller = new ItemController();
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();
            // Act on Test  
            var response = controller.Get("ITM1");
            // Assert the result  
            ITEM item;
            Assert.IsTrue(response.TryGetContentValue<ITEM>(out item));
            Assert.AreEqual("Lux", item.ITDESC);
        }

        [TestMethod]
        public void GetAllData()
        {
            // Set up Prerequisites   
            var controller = new ItemController();
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
            var controller = new ItemController();
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();
            // Act on Test  
            var response = controller.Get("ITM9");
            // Assert the result  
            Assert.IsTrue(!response.IsSuccessStatusCode);
        }
        [TestMethod]
        public void GetResponseCheck()
        {
            // Set up Prerequisites   
            var controller = new ItemController();
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();
            // Act on Test  
            var response = controller.Get("ITM1");
            // Assert the result  
            Assert.IsTrue(response.IsSuccessStatusCode);
        }
        [TestMethod]
        public void GetNotFoundResponseCheck()
        {
            // Set up Prerequisites   
            var controller = new ItemController();
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();
            // Act on Test  
            var response = controller.Get("ITM112");
            // Assert the result  
            Assert.IsTrue(response.StatusCode == HttpStatusCode.NotFound);
        }
        
        [TestMethod]
        public void PostBadRequest()
        {
            // Set up Prerequisites   
            var controller = new ItemController();
            controller.Request = new HttpRequestMessage(); 
            controller.Configuration = new HttpConfiguration();
            ITEM i=new ITEM();
            i.ITCODE="ITM8";
            i.ITDESC="500ML Bottle";
            i.ITRATE=60;
            i.PODETAILs = null;
            // Act on Test  
            var response = controller.Post(new ITEM { ITCODE = "ITM8", ITDESC = "500ML Bottle", ITRATE= 78 });
            // Assert the result  
            
            Assert.IsTrue(response.StatusCode == HttpStatusCode.BadRequest);
           // Assert.AreEqual(i.ITRATE, item.ITRATE);
        }
        [TestMethod]
        public void PostRequest()
        {
            // Set up Prerequisites   
            var controller = new ItemController();
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();
            
            // Act on Test  
            var response = controller.Post(new ITEM { ITCODE = "ITM9", ITDESC = "1L Bottle", ITRATE = 138 });
            // Assert the result  

            Assert.IsTrue(response.StatusCode == HttpStatusCode.Created);
            // Assert.AreEqual(i.ITRATE, item.ITRATE);
        }
        [TestMethod]
        public void PutRequest()
        {
            // Set up Prerequisites   
            var controller = new ItemController();
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();

            // Act on Test  
            var response = controller.Put(new ITEM { ITCODE = "ITM9", ITDESC = "1Ltr Bottle", ITRATE = 148 });
            // Assert the result  

            Assert.IsTrue(response.StatusCode == HttpStatusCode.OK);
        }
        [TestMethod]
        public void PutBadRequest()
        {
            // Set up Prerequisites   
            var controller = new ItemController();
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();

            // Act on Test  
            var response = controller.Put(new ITEM { ITCODE = "ITM10", ITDESC = "1Ltr Bottle Out of Bound Exception", ITRATE = 148 });
            // Assert the result  

            Assert.IsTrue(response.StatusCode == HttpStatusCode.BadRequest);
        }
        [TestMethod]
        public void DeleteNotFoundRequest()
        {
            // Set up Prerequisites   
            var controller = new ItemController();
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();

            // Act on Test  
            var response = controller.Delete ( "IT09" );
            // Assert the result  

            Assert.IsTrue(response.StatusCode == HttpStatusCode.NotFound);
        }
        [TestMethod]
        public void DeleteRequest()
        {
            // Set up Prerequisites   
            var controller = new ItemController();
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();

            // Act on Test  
            var response = controller.Delete("ITM9");
            // Assert the result  

            Assert.IsTrue(response.StatusCode == HttpStatusCode.OK);
        }
    }
}
