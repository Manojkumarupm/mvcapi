using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPIClient.Models
{
    public partial class SUPPLIER
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SUPPLIER()
        {
             
        }

        public string SUPLNO { get; set; }
        public string SUPLNAME { get; set; }
        public string SUPLADDR { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<POMASTER> POMASTERs { get; set; }
    }
}