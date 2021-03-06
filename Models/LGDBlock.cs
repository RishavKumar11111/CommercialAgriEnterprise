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
    
    public partial class LGDBlock
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public LGDBlock()
        {
            this.BeneFiciaryLandRecordDetails = new HashSet<BeneFiciaryLandRecordDetail>();
            this.BLOBlockLogs = new HashSet<BLOBlockLog>();
            this.BLOBlockMappings = new HashSet<BLOBlockMapping>();
            this.BLODetailEntries = new HashSet<BLODetailEntry>();
            this.LGDGPs = new HashSet<LGDGP>();
            this.LGDVillages = new HashSet<LGDVillage>();
        }
    
        public int DistrictCode { get; set; }
        public int BlockCode { get; set; }
        public string BlockName { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BeneFiciaryLandRecordDetail> BeneFiciaryLandRecordDetails { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BLOBlockLog> BLOBlockLogs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BLOBlockMapping> BLOBlockMappings { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BLODetailEntry> BLODetailEntries { get; set; }
        public virtual LGDDistrict LGDDistrict { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LGDGP> LGDGPs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LGDVillage> LGDVillages { get; set; }
    }
}
