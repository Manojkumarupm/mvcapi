using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPIClient.Models
{
    public partial class POMASTER
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public POMASTER()
        {
            this.PODETAILs = new HashSet<PODETAIL>();
        }

        public string PONO { get; set; }
        public Nullable<System.DateTime> PODATE { get; set; }
        public string SUPLNO { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PODETAIL> PODETAILs { get; set; }
        public virtual SUPPLIER SUPPLIER { get; set; }
    }
}