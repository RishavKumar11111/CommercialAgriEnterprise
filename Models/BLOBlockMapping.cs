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
    
    public partial class BLOBlockMapping
    {
        public string UserID { get; set; }
        public string MobileNo { get; set; }
        public int BlockCode { get; set; }
        public string DeptCode { get; set; }
        public string IPAddress { get; set; }
        public string DNOUserName { get; set; }
        public System.DateTime DNOUserDateTime { get; set; }
        public string FinancialYear { get; set; }
        public int IsActive { get; set; }
    
        public virtual Department Department { get; set; }
        public virtual LGDBlock LGDBlock { get; set; }
    }
}
