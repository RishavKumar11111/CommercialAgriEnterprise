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
    
    public partial class DNOBackToBLOLog
    {
        public string ReferenceNo { get; set; }
        public string DNOUserName { get; set; }
        public System.DateTime UserDateTime { get; set; }
        public string IPAddress { get; set; }
        public string FinancialYear { get; set; }
        public int BLOStatus { get; set; }
        public System.DateTime BLODate { get; set; }
        public string BLORejectionReason { get; set; }
        public string BLOUserName { get; set; }
        public string BLOIPAddress { get; set; }
        public string BLOFinancialYear { get; set; }
    
        public virtual BeneficiaryDetail BeneficiaryDetail { get; set; }
    }
}
