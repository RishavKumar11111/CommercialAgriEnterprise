using System;
using System.Linq;

namespace CommercialAgriEnterprise.Models
{
    public class FeasibilityReportVM
    {
        public string ReferenceNo { get; set; }
        public bool PreviousExperience { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public bool RoadConnectivity { get; set; }
        public decimal DistanceFromVillage { get; set; }
        public bool ElectrificationStatus { get; set; }
        public bool PollutionControlClearanceStatus { get; set; }
        public decimal SelfFinance { get; set; }
        public decimal UncensoredLoan { get; set; }
        public decimal MarginComponent { get; set; }
        public bool BankConsentLetterStatus { get; set; }
        public string UserName { get; set; }
        public string BlockCd { get { return new CommercialAgriEnterpriseEntities().BeneficiaryDetails.Where(z => z.ReferenceNo == ReferenceNo).Select(x => x.NICBlockCode).FirstOrDefault(); } }
        public int BlockCode { get { return Convert.ToInt32(BlockCd); } }
        public string BlockName { get { return new CommercialAgriEnterpriseEntities().LGDBlocks.Where(z => z.BlockCode == BlockCode).Select(x => x.BlockName).FirstOrDefault(); } }
        public byte[] Photograph { get; set; }
        public string Photo { get { return Convert.ToBase64String(Photograph); } }
        public string ProjectName { get; set; }
        public decimal? Capacity { get; set; }
        public string CapacityUnit { get; set; }
        public decimal TotalProjectCost { get; set; }
    }
}