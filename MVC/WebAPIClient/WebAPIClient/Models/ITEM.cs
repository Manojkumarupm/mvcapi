using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPIClient.Models
{
    public partial class ITEM
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ITEM()
        {
            this.PODETAILs = new HashSet<PODETAIL>();
        }

        public string ITCODE { get; set; }
        public string ITDESC { get; set; }
        public Nullable<decimal> ITRATE { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PODETAIL> PODETAILs { get; set; }
    }
}