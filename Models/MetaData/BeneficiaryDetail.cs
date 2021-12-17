using System;
using System.ComponentModel.DataAnnotations;

namespace CommercialAgriEnterprise.Models
{
    [MetadataType(typeof(BeneficiaryDetailMetaData))]
    public partial class BeneficiaryDetail
    {
        internal sealed class BeneficiaryDetailMetaData
        {
            [RegularExpression(@"^[A-Z]{3}\/[A-Z]{3}\/2\d{3}\-(?!00)[0-9]{2}\/[1-9][0-9]{0,7}$")]
            public string ReferenceNo { get; set; }
            [Required]
            [RegularExpression(@"^[A-Z]{3}\/\d{1,8}$")]
            public string FarmerID { get; set; }
            [RegularExpression(@"^[a-zA-Z\s]+$")]
            public string RelationApplicantName { get; set; }
            [Required]
            [RegularExpression("^(Self|Relative)$")]
            public string RelationWithFIDType { get; set; }
            [RegularExpression("^(1|2|3|4|5|7|8|10|11|12|13|14|15)$")]
            public int? RelationWithFID { get; set; }
            [RegularExpression(@"^\d{4}\d{4}\d{4}$")]
            public string RAadhaarNo { get; set; }
            [RegularExpression(@"^[\/\w]{1,15}$")]
            public string RVoterIDNo { get; set; }
            [DataType(DataType.DateTime)]
            public DateTime DateOfRegd { get; set; }
            [Required]
            [RegularExpression(@"^[6-9]\d{9}$")]
            public string MobileNo { get; set; }
            [Required]
            [RegularExpression(@"^(\d{6})$")]
            public string Pin { get; set; }
            [Required]
            [RegularExpression("^(Individual|Group)$")]
            public string BeneficiaryType { get; set; }
            [RegularExpression("^[1-9][0-9]{0,2}$")]
            public byte? NoOfMember { get; set; }
            [RegularExpression(@"^[a-zA-Z\s]+$")]
            public string FirmName { get; set; }
            [RegularExpression("^[1-7]$")]
            public int GroupTypeCode { get; set; }
            [RegularExpression(@"^([4-5][0]|[4-5][0]\.[0]{1,2})$")]
            public decimal PercentageOfSubsidy { get; set; }
            [Required]
            [RegularExpression(@"^[-\/&,.\w\s]+$")]
            public string CorrespondenceAddress { get; set; }
            [Required]
            [RegularExpression(@"^[-\/&,.\w\s]+$")]
            public string PermanentAddress { get; set; }
            [Required]
            [RegularExpression("^(Yes|No)$")]
            public string PreviousExperience { get; set; }
            [DataType(DataType.EmailAddress)]
            public string EmailID { get; set; }
            [Required]
            [RegularExpression("^[1-5]$")]
            public string HighestEducationalQualificationCode { get; set; }
            public byte[] CastGraduationCertificate { get; set; }
            [Required]
            [RegularExpression(@"^(0|[1-9]\d{0,12}|[1-9]\d{0,12}\.\d{1,2}|0\.\d{1,2}|\.\d{1,2})$")]
            public decimal AnnualIncome { get; set; }
            [Required]
            [RegularExpression(@"^[a-zA-Z\s]+$")]
            public string PresentOccupation { get; set; }
            [RegularExpression(@"^\d{4}$")]
            public string NICBlockCode { get; set; }
            [RegularExpression(@"^\d{3}$")]
            public string NICDistrictCode { get; set; }
            [RegularExpression("^(incomplete|completed)$")]
            public string RegistrationStatus { get; set; }
            public decimal RegistrationAmount { get; set; }
            [RegularExpression("^(Pending|Success)$")]
            public string PaymentStatus { get; set; }
            public byte[] LandPdf1 { get; set; }
            public byte[] LandPdf2 { get; set; }
        }
    }
}