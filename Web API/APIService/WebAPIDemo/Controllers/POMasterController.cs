using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPIDemo.Models;

namespace WebAPIDemo.Controllers
{
    public class POMasterController : ApiController
    {
        public IEnumerable<POInformation> Get()
        {
            using (PODbEntities1 PE = new PODbEntities1())
            {
                PE.Configuration.ProxyCreationEnabled = false;
                IEnumerable<POMASTER> pomasters = PE.POMASTERs.ToList();
                IEnumerable<PODETAIL> podetails = PE.PODETAILs.ToList();
                IEnumerable<ITEM> items = PE.ITEMs.ToList();
                IEnumerable<SUPPLIER> suppliers = PE.SUPPLIERs.ToList();
                List<POInformation> poinformation = new List<POInformation>();
                var value = from p in pomasters
                            join q in podetails on p.PONO equals q.PONO
                            join i in items on q.ITCODE equals i.ITCODE
                            join s in suppliers on p.SUPLNO equals s.SUPLNO
                            select new { p.PONO, p.PODATE, p.SUPLNO, s.SUPLNAME, q.ITCODE, i.ITDESC, q.QTY };
                foreach (var v in value)
                {
                    POInformation poi = new POInformation();
                    poi.PONO = v.PONO;
                    poi.ITDESC = v.ITDESC;
                    poi.QTY = v.QTY;
                    poi.SUPLNAME = v.SUPLNAME;
                    poi.SUPLNO = v.SUPLNO;
                    poi.ITCODE = v.ITCODE;
                    poi.PODATE = v.PODATE;
                    poinformation.Add(poi);
                }
                return poinformation;
            }
        }
        [HttpGet]
        public HttpResponseMessage Get(string PoNumber)
        {
            using (PODbEntities1 PE = new PODbEntities1())
            {
                PE.Configuration.ProxyCreationEnabled = false;
                IEnumerable<POMASTER> pomasters = PE.POMASTERs.ToList();
                IEnumerable<PODETAIL> podetails = PE.PODETAILs.ToList();
                IEnumerable<ITEM> items = PE.ITEMs.ToList();
                IEnumerable<SUPPLIER> suppliers = PE.SUPPLIERs.ToList();
                List<POInformation> poinformation = new List<POInformation>();
                var value = from p in pomasters
                            join q in podetails on p.PONO equals q.PONO
                            join i in items on q.ITCODE equals i.ITCODE
                            join s in suppliers on p.SUPLNO equals s.SUPLNO
                            where p.PONO == PoNumber
                            select new { p.PONO, p.PODATE, p.SUPLNO, s.SUPLNAME, q.ITCODE, i.ITDESC, q.QTY };
                foreach (var v in value)
                {
                    POInformation poi = new POInformation();
                    poi.PONO = v.PONO;
                    poi.ITDESC = v.ITDESC;
                    poi.QTY = v.QTY;
                    poi.SUPLNAME = v.SUPLNAME;
                    poi.SUPLNO = v.SUPLNO;
                    poi.ITCODE = v.ITCODE;
                    poi.PODATE = v.PODATE;
                    poinformation.Add(poi);
                }
                if (poinformation.Count > 0)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, poinformation);
                }
                else
                {

                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "POMASTER Number : " + PoNumber + " Not Found");
                }
            }

        }
        public HttpResponseMessage Post([FromBody] List<POInformation> i)
        {
            try
            {
                using (PODbEntities1 PE = new PODbEntities1())
                {
                    PE.Configuration.ProxyCreationEnabled = false;
                    // convert PO Information to PO Master and PO Details

                    foreach (POInformation pi in i.GroupBy(x => x.PONO).Select(x => x.FirstOrDefault()))
                    {
                        POMASTER po = new POMASTER();
                        po.PONO = pi.PONO;
                        po.SUPLNO = pi.SUPLNO;
                        po.PODATE = DateTime.Now;
                        PE.POMASTERs.Add(po);
                    }

                    foreach (POInformation pi in i)
                    {
                        PODETAIL pod = new PODETAIL();
                        pod.ITCODE = pi.ITCODE;
                        pod.QTY = pi.QTY;
                        pod.PONO = pi.PONO;
                        PE.PODETAILs.Add(pod);
                    }

                    PE.SaveChanges();
                    HttpResponseMessage Message = Request.CreateResponse(HttpStatusCode.Created, i);
                  //  Message.Headers.Location = new Uri(Request.RequestUri + i.First().PONO);
                    return Message;
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        public HttpResponseMessage Put([FromBody] List<POInformation> i)
        {
            try
            {
                using (PODbEntities1 PE = new PODbEntities1())
                {
                    PE.Configuration.ProxyCreationEnabled = false;
                    // convert PO Information to PO Master and PO Details

                    foreach (POInformation pi in i.GroupBy(x => x.PONO).Select(x => x.FirstOrDefault()))
                    {

                        POMASTER po = PE.POMASTERs.Where(x => x.PONO == pi.PONO).FirstOrDefault();

                        po.SUPLNO = pi.SUPLNO;
                        po.PODATE = DateTime.Now;
                        PE.SaveChanges();
                    }

                    foreach (POInformation pi in i)
                    {
                        PODETAIL pod = PE.PODETAILs.Where(x => x.PONO == pi.PONO && pi.ITCODE == x.ITCODE).FirstOrDefault();

                        pod.QTY = pi.QTY;
                        PE.SaveChanges();
                    }

                    PE.SaveChanges();
                    HttpResponseMessage Message = Request.CreateResponse(HttpStatusCode.OK, i);
                   // Message.Headers.Location = new Uri(Request.RequestUri + i.First().PONO);
                    return Message;
                }

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
        public HttpResponseMessage Delete(string POMASTERCode)
        {
            HttpResponseMessage httpResponse = null;
            try
            {
                using (PODbEntities1 PE = new PODbEntities1())
                {
                    PE.Configuration.ProxyCreationEnabled = false;
                    IEnumerable<PODETAIL> p = PE.PODETAILs.Where(x => x.PONO == POMASTERCode);
                    foreach (PODETAIL value in p)
                    {
                        PE.Entry(value).State = System.Data.Entity.EntityState.Deleted;
                    }
                    POMASTER I = PE.POMASTERs.Where(x => x.PONO == POMASTERCode).FirstOrDefault();
                    if (I == null)
                    {
                        httpResponse = Request.CreateErrorResponse(HttpStatusCode.NotFound, "POMASTER with Code " + POMASTERCode + " Not found");
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
