//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CommercialAgriEnterprise.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class LGBlock
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public LGBlock()
        {
            this.LGGps = new HashSet<LGGp>();
            this.LGVillages = new HashSet<LGVillage>();
        }
    
        public Nullable<int> Dist_Code { get; set; }
        public int Block_Code { get; set; }
        public string Block_Name { get; set; }
    
        public virtual LGDistrict LGDistrict { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LGGp> LGGps { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LGVillage> LGVillages { get; set; }
    }
}
