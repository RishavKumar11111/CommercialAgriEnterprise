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
    
    public partial class rTehsil
    {
        public string TCode { get; set; }
        public string DCode { get; set; }
        public string TName { get; set; }
        public string LGDBlockCode { get; set; }
    
        public virtual rDistrict rDistrict { get; set; }
    }
}
