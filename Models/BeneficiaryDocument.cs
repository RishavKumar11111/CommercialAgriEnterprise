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
    
    public partial class BeneficiaryDocument
    {
        public string ReferenceNo { get; set; }
        public byte[] Photograph { get; set; }
        public byte[] IdentityProof { get; set; }
        public byte[] Signature { get; set; }
    
        public virtual BeneficiaryDetail BeneficiaryDetail { get; set; }
    }
}
