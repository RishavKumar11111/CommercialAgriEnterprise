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
    
    public partial class CapitalInvestmentCivilConstruction
    {
        public string ReferenceNo { get; set; }
        public decimal Building { get; set; }
        public decimal Shed { get; set; }
        public decimal OfficeCumStore { get; set; }
        public decimal Godown { get; set; }
        public decimal PlantArea { get; set; }
        public decimal OtherNecessaryConstruction { get; set; }
        public decimal Total { get; set; }
    
        public virtual BeneficiaryDetail BeneficiaryDetail { get; set; }
    }
}
