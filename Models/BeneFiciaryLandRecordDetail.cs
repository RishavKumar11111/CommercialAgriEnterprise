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
    
    public partial class BeneFiciaryLandRecordDetail
    {
        public string ReferenceNo { get; set; }
        public string KhataNo { get; set; }
        public string PlotNo { get; set; }
        public int DistrictCode { get; set; }
        public int BlockCode { get; set; }
        public Nullable<int> TehsilCode { get; set; }
        public Nullable<int> GPCode { get; set; }
        public Nullable<int> RICircleCode { get; set; }
        public int VillageCode { get; set; }
        public string RelationshipCode { get; set; }
        public string TenantName { get; set; }
        public string Kisam { get; set; }
        public Nullable<decimal> AreaInHectare { get; set; }
        public Nullable<decimal> AreaInAcre { get; set; }
        public decimal ExecutionArea { get; set; }
        public string UnitExecution { get; set; }
        public string Locality { get; set; }
    
        public virtual LGDBlock LGDBlock { get; set; }
        public virtual LGDDistrict LGDDistrict { get; set; }
        public virtual LGDGP LGDGP { get; set; }
        public virtual Relationship Relationship { get; set; }
        public virtual BeneficiaryDetail BeneficiaryDetail { get; set; }
    }
}
