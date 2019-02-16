using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPIDemo.Models;

namespace WebAPIDemo.Controllers
{
    public class PODETAILController : ApiController
    {
        public IEnumerable<PODETAIL> Get()
        {
            using (PODbEntities1 PE = new PODbEntities1())
            {
                PE.Configuration.ProxyCreationEnabled = false;
                IEnumerable<PODETAIL> podetails = PE.PODETAILs.ToList();
                return PE.PODETAILs.ToList();
            }
        }
        [HttpGet]
        public HttpResponseMessage Get(string PoNumber)
        {

            using (PODbEntities1 PE = new PODbEntities1())
            {
                PE.Configuration.ProxyCreationEnabled = false;
                PODETAIL i = PE.PODETAILs.Where(x => x.PONO == PoNumber).FirstOrDefault();
                if (i != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, i);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "PODETAIL Number : " + PoNumber + " Not Found");
                }
            }
        }
        public HttpResponseMessage Post([FromBody] PODETAIL i)
        {
            try
            {
                using (PODbEntities1 PE = new PODbEntities1())
                {
                    PE.Configuration.ProxyCreationEnabled = false;
                    PE.PODETAILs.Add(i);
                    PE.SaveChanges();
                    HttpResponseMessage Message = Request.CreateResponse(HttpStatusCode.Created, i);
                    Message.Headers.Location = new Uri(Request.RequestUri + i.PONO);
                    return Message;
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        public HttpResponseMessage Put([FromBody] PODETAIL i)
        {
            try
            {
                using (PODbEntities1 PE = new PODbEntities1())
                {
                    PE.Configuration.ProxyCreationEnabled = false;
                    PODETAIL value = PE.PODETAILs.Where(x => x.PONO == i.PONO).FirstOrDefault();
                    value.ITCODE = i.ITCODE;
                    value.QTY = i.QTY;

                    PE.SaveChanges();
                    HttpResponseMessage Message = Request.CreateResponse(HttpStatusCode.OK, value);
                    Message.Headers.Location = new Uri(Request.RequestUri + value.PONO);
                    return Message;
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
        public HttpResponseMessage Delete(string PONumber)
        {
            HttpResponseMessage httpResponse = null;
            try
            {
                using (PODbEntities1 PE = new PODbEntities1())
                {
                    PE.Configuration.ProxyCreationEnabled = false;
                    PODETAIL I = PE.PODETAILs.Where(x => x.PONO == PONumber).FirstOrDefault();
                    if (I == null)
                    {
                        httpResponse = Request.CreateErrorResponse(HttpStatusCode.NotFound, "PODETAIL with Code " + PONumber + " Not found");
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
