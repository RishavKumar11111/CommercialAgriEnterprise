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
    
    public partial class KeyBusinessMatrix
    {
        public string ReferenceNo { get; set; }
        public decimal BreakEvenPoint { get; set; }
        public decimal DSCR { get; set; }
        public decimal IRR { get; set; }
    
        public virtual BeneficiaryDetail BeneficiaryDetail { get; set; }
    }
}
