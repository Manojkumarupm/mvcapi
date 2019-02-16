using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DataLayer;
namespace ProductService.Controllers
{
    public class ProductController : ApiController
    {
        public IEnumerable<SUPPLIER> Get()
        {
            PODbEntities pd = new PODbEntities();
          return  pd.SUPPLIERs.ToList();
        }
    }
}
