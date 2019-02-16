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
        public IEnumerable<ITEM> Get()
        {
            using (PODbEntities1 PE = new PODbEntities1())
            {
                PE.Configuration.ProxyCreationEnabled = false;
                return PE.ITEMs.ToList();
            }
        }
        [HttpGet]
        public HttpResponseMessage Get(string ItemNumber)
        {

            using (PODbEntities1 PE = new PODbEntities1())
            {
                PE.Configuration.ProxyCreationEnabled = false;
                ITEM i = PE.ITEMs.Where(x => x.ITCODE == ItemNumber).FirstOrDefault();
                if (i != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, i);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Item Number : " + ItemNumber + " Not Found");
                }
            }
        }
        public HttpResponseMessage Post([FromBody] ITEM i)
        {
            try
            {
                using (PODbEntities1 PE = new PODbEntities1())
                {
                    PE.Configuration.ProxyCreationEnabled = false;
                    PE.ITEMs.Add(i);
                    PE.SaveChanges();
                    HttpResponseMessage Message = Request.CreateResponse(HttpStatusCode.Created, i);
                    Message.Headers.Location = new Uri(Request.RequestUri + i.ITCODE);
                    return Message;
                }
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
                using (PODbEntities1 PE = new PODbEntities1())
                {
                    PE.Configuration.ProxyCreationEnabled = false;
                    ITEM value = PE.ITEMs.Where(x => x.ITCODE == i.ITCODE).FirstOrDefault();
                    value.ITDESC = i.ITDESC;
                    value.ITRATE = i.ITRATE;
                    PE.SaveChanges();
                    HttpResponseMessage Message = Request.CreateResponse(HttpStatusCode.OK, value);
                    Message.Headers.Location = new Uri(Request.RequestUri + value.ITCODE);
                    return Message;
                }
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
                using (PODbEntities1 PE = new PODbEntities1())
                {
                    PE.Configuration.ProxyCreationEnabled = false;
                    ITEM I = PE.ITEMs.Where(x => x.ITCODE == ItemCode).FirstOrDefault();
                    if (I == null)
                    {
                        httpResponse = Request.CreateErrorResponse(HttpStatusCode.NotFound, "Item with Code " + ItemCode + " Not found");
                    }
                    else
                    {
                        PE.Entry(I).State = System.Data.Entity.EntityState.Deleted;
                        PE.SaveChanges();
                        httpResponse = Request.CreateResponse(HttpStatusCode.OK);
                    }

                }
                return httpResponse;
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }

        }
    }
}
