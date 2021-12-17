using System;

namespace CommercialAgriEnterprise.Models
{
    public class FirstFormVM
    {
        public string FarmerID { get; set; }
        public string Apptype { get; set; }
        public int? GroupTypeCode { get; set; }
        public string GroupTypeName { get; set; }
        public string FirmName { get; set; }
        public int? NoOfMembers { get; set; }
        public string QualificationID { get; set; }
        public string Qualification { get; set; }
        public decimal Anualincome { get; set; }
        public string PresentOccupation { get; set; }
        public string PrevExp { get; set; }
        public string EmailID { get; set; }
        public string Pincode { get; set; }
        public string CorespondenceAddress { get; set; }
        public string PermanentAddress { get; set; }
    }
}