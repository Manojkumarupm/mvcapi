using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPIDemo.Models;

namespace WebAPIDemo.Controllers
{
    public class ItemController : ApiController 
    {
        //IItemRepository _repository;

        //public ItemController(IItemRepository repository)
        //{
        //    _repository = repository;
        //}
        PODbEntities1 context = new PODbEntities1();
        public IEnumerable<ITEM> Get()
        {

            context.Configuration.ProxyCreationEnabled = false;
            return context.ITEMs.ToList();

        }
        [HttpGet]
        public HttpResponseMessage Get(string ItemNumber)
        {

            context.Configuration.ProxyCreationEnabled = false;
            ITEM i = context.ITEMs.Where(x => x.ITCODE == ItemNumber).FirstOrDefault();
            if (i != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, i);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Item Number : " + ItemNumber + " Not Found");
            }

        }
        public HttpResponseMessage Post([FromBody] ITEM i)
        {
            try
            {

                context.Configuration.ProxyCreationEnabled = false;
                context.ITEMs.Add(i);
                context.SaveChanges();
                HttpResponseMessage Message = Request.CreateResponse(HttpStatusCode.Created, i);
              //  Message.Headers.Location = new Uri(Request.RequestUri + i.ITCODE);
                return Message;

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        public HttpResponseMessage Put([FromBody] ITEM i)
        {
            try
            {

                context.Configuration.ProxyCreationEnabled = false;
                ITEM value = context.ITEMs.Where(x => x.ITCODE == i.ITCODE).FirstOrDefault();
                value.ITDESC = i.ITDESC;
                value.ITRATE = i.ITRATE;
                context.SaveChanges();
                HttpResponseMessage Message = Request.CreateResponse(HttpStatusCode.OK, value);
               // Message.Headers.Location = new Uri(Request.RequestUri + value.ITCODE);
                return Message;

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
        public HttpResponseMessage Delete(string ItemCode)
        {
            HttpResponseMessage httpResponse = null;
            try
            {

                context.Configuration.ProxyCreationEnabled = false;
                ITEM I = context.ITEMs.Where(x => x.ITCODE == ItemCode).FirstOrDefault();
                if (I == null)
                {
                    httpResponse = Request.CreateErrorResponse(HttpStatusCode.NotFound, "Item with Code " + ItemCode + " Not found");
                }
                else
                {
                    context.Entry(I).State = System.Data.Entity.EntityState.Deleted;
                    context.SaveChanges();
                    httpResponse = Request.CreateResponse(HttpStatusCode.OK);
                }

                return httpResponse;
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }

        }
        protected override void Dispose(bool disposing)
        {
            context.Dispose();
            base.Dispose(disposing);
        }
    }
}
