using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPIDemo.Models;

namespace WebAPIDemo.Controllers
{
    public class SupplierController : ApiController
    {
        public IEnumerable<SUPPLIER> Get()
        {
            using (PODbEntities1 PE = new PODbEntities1())
            {
                PE.Configuration.ProxyCreationEnabled = false;
                return PE.SUPPLIERs.ToList();
            }
        }
        [HttpGet]
        public HttpResponseMessage Get(string SUPPLIERNumber)
        {

            using (PODbEntities1 PE = new PODbEntities1())
            {
                PE.Configuration.ProxyCreationEnabled = false;
                SUPPLIER i = PE.SUPPLIERs.Where(x => x.SUPLNO == SUPPLIERNumber).FirstOrDefault();
                if (i != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, i);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "SUPPLIER Number : " + SUPPLIERNumber + " Not Found");
                }
            }
        }
        public HttpResponseMessage Post([FromBody] SUPPLIER i)
        {
            try
            {
                using (PODbEntities1 PE = new PODbEntities1())
                {
                    PE.Configuration.ProxyCreationEnabled = false;
                    PE.SUPPLIERs.Add(i);
                    PE.SaveChanges();
                    HttpResponseMessage Message = Request.CreateResponse(HttpStatusCode.Created, i);
                   // Message.Headers.Location = new Uri(Request.RequestUri + i.SUPLNO);
                    return Message;
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        public HttpResponseMessage Put([FromBody] SUPPLIER i)
        {
            try
            {
                using (PODbEntities1 PE = new PODbEntities1())
                {
                    PE.Configuration.ProxyCreationEnabled = false;
                    SUPPLIER value = PE.SUPPLIERs.Where(x => x.SUPLNO == i.SUPLNO).FirstOrDefault();
                    value.SUPLNAME = i.SUPLNAME;
                    value.SUPLADDR = i.SUPLADDR;
                    PE.SaveChanges();
                    HttpResponseMessage Message = Request.CreateResponse(HttpStatusCode.OK, value);
                   // Message.Headers.Location = new Uri(Request.RequestUri + value.SUPLNO);
                    return Message;
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
        public HttpResponseMessage Delete(string SUPPLIERCode)
        {
            HttpResponseMessage httpResponse = null;
            try
            {
                using (PODbEntities1 PE = new PODbEntities1())
                {
                    PE.Configuration.ProxyCreationEnabled = false;
                    SUPPLIER I = PE.SUPPLIERs.Where(x => x.SUPLNO == SUPPLIERCode).FirstOrDefault();
                    if (I == null)
                    {
                        httpResponse = Request.CreateErrorResponse(HttpStatusCode.NotFound, "SUPPLIER with Code " + SUPPLIERCode + " Not found");
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
