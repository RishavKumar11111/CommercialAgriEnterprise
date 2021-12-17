using System;

namespace CommercialAgriEnterprise.Models
{
    public class BLOSignatures
    {
        public int BlockCode { get; set; }
        public string BlockName { get; set; }
        public string Name { get; set; }
        public string MobileNo { get; set; }
        public string EmailID { get; set; }
        public byte[] BLOSignature { get; set; }
        public string Sign { get { return Convert.ToBase64String(BLOSignature); } }
    }
}