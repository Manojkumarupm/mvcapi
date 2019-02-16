using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DataLayer;

namespace WebAPIProduct.Controllers
{
    public class SupplierController : ApiController
    {
        public IEnumerable<SUPPLIER> Get()
        {
            try
            {
                PODbEntities entities = new PODbEntities();
              
                return entities.SUPPLIERs.ToList();
            }
            catch (Exception)
            {
                
                throw;
            }
            finally
            {

            }
        }
    }
}
