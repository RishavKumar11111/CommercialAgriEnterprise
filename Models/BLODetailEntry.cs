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
    
    public partial class BLODetailEntry
    {
        public string DepartmentCode { get; set; }
        public int DistrictCode { get; set; }
        public int BlockCode { get; set; }
        public string Name { get; set; }
        public string MobileNo { get; set; }
        public string EmailID { get; set; }
        public byte[] Signature { get; set; }
        public System.DateTime UserDateTime { get; set; }
        public string IPAddress { get; set; }
        public string FinancialYear { get; set; }
    
        public virtual Department Department { get; set; }
        public virtual LGDBlock LGDBlock { get; set; }
        public virtual LGDDistrict LGDDistrict { get; set; }
    }
}
