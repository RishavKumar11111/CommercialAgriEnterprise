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
    
    public partial class CapitalInvestmentMiscellaneou
    {
        public string ReferenceNo { get; set; }
        public decimal FixedAsset { get; set; }
        public decimal Livestock { get; set; }
        public decimal Bird { get; set; }
        public decimal Cultivation { get; set; }
        public decimal Total { get; set; }
    
        public virtual BeneficiaryDetail BeneficiaryDetail { get; set; }
    }
}
