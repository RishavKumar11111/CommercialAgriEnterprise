using System;

namespace CommercialAgriEnterprise.Models
{
    public class GetUserVM
    {
        public string UserName { get; set; }
        public string refno { get; set; }
        public string EMAILID { get; set; }
        public DateTime? DOA { get; set; }
        public string distnm { get; set; }
        public string pdf { get; set; }
        public string Address { get; set; }
    }
}