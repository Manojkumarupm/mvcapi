using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPIClient.Models
{
    public partial class PODETAIL
    {
        public string PONO { get; set; }
        public string ITCODE { get; set; }
        public Nullable<int> QTY { get; set; }

        public virtual ITEM ITEM { get; set; }
        public virtual POMASTER POMASTER { get; set; }
    }
}