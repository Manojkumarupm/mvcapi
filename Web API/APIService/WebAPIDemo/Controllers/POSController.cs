using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPIDemo.Models;

namespace WebAPIDemo.Controllers
{
    public class POSController : ApiController
    {
        public IEnumerable<SUPPLIER> GetSuppliers()
        {
            using (PODbEntities1 PE = new PODbEntities1())
            {
                return PE.SUPPLIERs.ToList();
            }
        }
        public SUPPLIER GetSupplier(string SupplierNumber)
        {
            using (PODbEntities1 PE = new PODbEntities1())
            {
                return PE.SUPPLIERs.Where(x => x.SUPLNO == SupplierNumber).First();
            }
        }
        public string PutSupplier(SUPPLIER s)
        {
            using (PODbEntities1 PE = new PODbEntities1())
            {
                PE.SUPPLIERs.Add(s);
                PE.SaveChanges();
                return "Success";
            }
        }

        public string DeleteSupplier(string supplierID)
        {
            using (PODbEntities1 PE = new PODbEntities1())
            {
                SUPPLIER s = PE.SUPPLIERs.Where(x => x.SUPLNO == supplierID).First();
                PE.SUPPLIERs.Remove(s);
                PE.SaveChanges();
                return "Success";
            }
        }

        public IEnumerable<POMASTER> GetPOMasters()
        {
            using (PODbEntities1 PE = new PODbEntities1())
            {
                return PE.POMASTERs.ToList();
            }
        }

        public POMASTER GetPOMaster(string PONumber)
        {
            using (PODbEntities1 PE = new PODbEntities1())
            {
                return PE.POMASTERs.Where(x => x.PONO == PONumber).First();
            }
        }
        public string PutPOMaster(POMASTER p)
        {
            using (PODbEntities1 PE = new PODbEntities1())
            {
                PE.POMASTERs.Add(p);
                PE.SaveChanges();
                return "Success";
            }
        }

        public string DeletePOMaster(string PONumber)
        {
            using (PODbEntities1 PE = new PODbEntities1())
            {
                POMASTER po = PE.POMASTERs.Where(x => x.PONO == PONumber).First();
                IEnumerable<PODETAIL> POD = PE.PODETAILs.Where(x => x.PONO == po.PONO);
                PE.PODETAILs.RemoveRange(POD);
                //foreach(PODETAIL podetail in POD)
                //{
                //    PE.PODETAILs.Remove(podetail);
                //}
                PE.POMASTERs.Remove(po);
                PE.SaveChanges();
                return "Success";
            }
        }

        public IEnumerable<PODETAIL> GetPODetails()
        {
            using (PODbEntities1 PE = new PODbEntities1())
            {
                return PE.PODETAILs.ToList();
            }
        }
        public IEnumerable<PODETAIL> GetPODetails(string PONumber)
        {
            using (PODbEntities1 PE = new PODbEntities1())
            {
                return PE.PODETAILs.Where(x => x.PONO == PONumber);
            }
        }

        public string PutPODetails(IEnumerable<PODETAIL> podetails)
        {
            using (PODbEntities1 PE = new PODbEntities1())
            {
                PE.PODETAILs.AddRange(podetails);
                PE.SaveChanges();
                return "Success";
            }
        }

        public string DeletePODetails(IEnumerable<PODETAIL> podetails)
        {
            using (PODbEntities1 PE = new PODbEntities1())
            {
                PE.PODETAILs.RemoveRange(podetails);
                PE.SaveChanges();
                return "Success";
            }
        }
        public string PutPODetails(string PONumber)
        {
            using (PODbEntities1 PE = new PODbEntities1())
            {
                IEnumerable<PODETAIL> podetails = PE.PODETAILs.Where(x => x.PONO == PONumber);
                PE.PODETAILs.RemoveRange(podetails);
                PE.SaveChanges();
                return "Success";
            }
        }
    }
}
