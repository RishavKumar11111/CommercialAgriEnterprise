using System.ComponentModel.DataAnnotations;

namespace CommercialAgriEnterprise.Models
{
    [MetadataType(typeof(IntegratedProjectLogMetaData))]
    public partial class IntegratedProjectLog
    {
        internal sealed class IntegratedProjectLogMetaData
        {
            [Required]
            [RegularExpression(@"^[A-Z]{3}\/[A-Z]{3}\/2\d{3}\-(?!00)[0-9]{2}\/[1-9][0-9]{0,7}$")]
            public string PreviousReferenceNo { get; set; }
            [RegularExpression(@"^[a-z]{3}_\d{4}$")]
            public string PreviousUserName { get; set; }
            [RegularExpression(@"^[a-z]{3}_\d{4}$")]
            public string NewUserName { get; set; }
            [RegularExpression("^0[1-4]$")]
            public string PreviousDeptCode { get; set; }
            [Required]
            [RegularExpression("^0[1-4]$")]
            public string NewDeptCode { get; set; }
            [DataType(DataType.Date)]
            public System.DateTime UserDateTime { get; set; }
            public string IPAddress { get; set; }
            [RegularExpression(@"^2\d{3}\-(?!00)[0-9]{2}$")]
            public string FinancialYear { get; set; }
        }
    }
}