using System.ComponentModel.DataAnnotations;

namespace CommercialAgriEnterprise.Models
{
    [MetadataType(typeof(BeneficiaryMemberDetailMetaData))]
    public partial class BeneficiaryMemberDetail
    {
        internal sealed class BeneficiaryMemberDetailMetaData
        {
            [RegularExpression(@"^[A-Z]{3}\/[A-Z]{3}\/2\d{3}\-(?!00)[0-9]{2}\/[1-9][0-9]{0,7}$")]
            public string ReferenceNo { get; set; }
            [RegularExpression(@"^[A-Z]{3}\/\d{1,8}$")]
            public string GroupMemberFarmerID { get; set; }
            [RegularExpression("^[1-5]$")]
            public string HighestEducationalQualificationCode { get; set; }
        }
    }
}