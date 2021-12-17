using System.ComponentModel.DataAnnotations;

namespace CommercialAgriEnterprise.Models
{
    [MetadataType(typeof(FeasibilityReportMetaData))]
    public partial class FeasibilityReport
    {
        internal sealed class FeasibilityReportMetaData
        {
            [Required]
            [RegularExpression(@"^[A-Z]{3}\/[A-Z]{3}\/2\d{3}\-(?!00)[0-9]{2}\/[1-9][0-9]{0,7}$")]
            public string ReferenceNo { get; set; }
            [Required]
            public bool PreviousExperience { get; set; }
            [Required]            
            public bool RoadConnectivity { get; set; }
            [Required]
            [RegularExpression(@"^(0|[1-9]\d{0,12}|[1-9]\d{0,12}\.\d{1,3}|0\.\d{1,3}|\.\d{1,3})$")]
            public decimal DistanceFromVillage { get; set; }
            [Required]
            public bool ElectrificationStatus { get; set; }
            [Required]
            public bool PollutionControlClearanceStatus { get; set; }
            [Required]
            [RegularExpression(@"^(0|[1-9]\d{0,12}|[1-9]\d{0,12}\.\d{1,2}|0\.\d{1,2}|\.\d{1,2})$")]
            public decimal SelfFinance { get; set; }
            [Required]
            [RegularExpression(@"^(0|[1-9]\d{0,12}|[1-9]\d{0,12}\.\d{1,2}|0\.\d{1,2}|\.\d{1,2})$")]
            public decimal UncensoredLoan { get; set; }
            [Required]
            [RegularExpression(@"^(0|[1-9]\d{0,12}|[1-9]\d{0,12}\.\d{1,2}|0\.\d{1,2}|\.\d{1,2})$")]
            public decimal MarginComponent { get; set; }
            [Required]
            public bool BankConsentLetterStatus { get; set; }
        }
    }
}