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
    
    public partial class GoAhead
    {
        public string GoAheadNumber { get; set; }
        public string ReferenceNo { get; set; }
        public int GoAheadStatus { get; set; }
        public System.DateTime GoAheadDOI { get; set; }
        public System.DateTime GoAheadValidDate { get; set; }
        public string IPAddress { get; set; }
        public System.DateTime UserDateTime { get; set; }
        public string FinancialYear { get; set; }
    
        public virtual BeneficiaryCompletion BeneficiaryCompletion { get; set; }
        public virtual BeneficiaryDetail BeneficiaryDetail { get; set; }
    }
}
