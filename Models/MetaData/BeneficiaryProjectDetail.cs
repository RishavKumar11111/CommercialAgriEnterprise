using System.ComponentModel.DataAnnotations;

namespace CommercialAgriEnterprise.Models
{
    [MetadataType(typeof(BeneficiaryProjectDetailMetadata))]
    public partial class BeneficiaryProjectDetail
    {
        internal sealed class BeneficiaryProjectDetailMetadata
        {
            [RegularExpression(@"^[A-Z]{3}\/[A-Z]{3}\/2\d{3}\-(?!00)[0-9]{2}\/[1-9][0-9]{0,7}$")]
            public string ReferenceNo { get; set; }
            [Required]
            [RegularExpression("^0[1-4]$")]
            public string DepartmentCode { get; set; }
            [Required]
            [RegularExpression(@"^0[1-4]P\d{1,3}$")]
            public string ProjectTypeCode { get; set; }
            [Required]
            [RegularExpression("^(Self|Bank)$")]
            public string FinanceType { get; set; }
            [Required]
            [RegularExpression(@"^[a-zA-Z\s]+$")]
            public string ProductToBeProducedOrMarketed { get; set; }
            [Required]
            [RegularExpression("^(Yes|No)$")]
            public string IsCISAvailedPreviously { get; set; }
            [RegularExpression(@"^0[1-4]P\d{1,3}$")]
            public string CISProjectTypeCode { get; set; }
            [RegularExpression(@"^[a-zA-Z\s]+$")]
            public string CISLocation { get; set; }
            [RegularExpression(@"^[1-2]\d{3}$")]
            public string CISAvailedYear { get; set; }
            [RegularExpression("^(Self|Bank)$")]
            public string CISFinanceType { get; set; }
            [RegularExpression(@"^(0|[1-9]\d{0,12}|[1-9]\d{0,12}\.\d{1,2}|0\.\d{1,2}|\.\d{1,2})$")]
            public decimal CISAmount { get; set; }
            public byte[] CISBankClearanceCertificate { get; set; }
            [Required]
            [RegularExpression("^(Below 1 Crore|1 Crore or Above)$")]
            public string ApproximateCost { get; set; }
            [Required]
            [RegularExpression(@"^([1-9]\d{0,12}|[1-9]\d{0,12}\.\d{1,2}|0\.\d{1,2}|\.\d{1,2})$")]
            public decimal OwnContribution { get; set; }
            [RegularExpression(@"^(0|[1-9]\d{0,12}|[1-9]\d{0,12}\.\d{1,2}|0\.\d{1,2}|\.\d{1,2})$")]
            public decimal BankLoan { get; set; }
            [RegularExpression(@"^([1-9]\d{0,7}|[1-9]\d{0,7}\.\d{1,2}|0\.\d{1,2}|\.\d{1,2})$")]
            public int Capacity { get; set; }
            [RegularExpression("^(Acres|Bottles/Sitting|Boxes|Eggs/Week|Hectares|LPD|MT|MT/Annum|MT/Day|Numbers|Sample/Annum|Sq m)$")]
            public string CapacityUnit { get; set; }
            [RegularExpression(@"^[1-9]\d{0,2}$")]
            public string BankCode { get; set; }
            [RegularExpression(@"^[1-9]\d{0,4}$")]
            public string BranchCode { get; set; }
        }
    }
}