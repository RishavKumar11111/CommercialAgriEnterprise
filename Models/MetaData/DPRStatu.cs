using System.ComponentModel.DataAnnotations;

namespace CommercialAgriEnterprise.Models.MetaData
{
    [MetadataType(typeof(DPRStatuMetaData))]
    public class DPRStatu
    {
        internal sealed class DPRStatuMetaData
        {
            [Required]
            [RegularExpression(@"^[A-Z]{3}\/[A-Z]{3}\/2\d{3}\-(?!00)[0-9]{2}\/[1-9][0-9]{0,7}$")]
            public string ReferenceNo { get; set; }
            [Required]
            [RegularExpression("^(0|1)$")]
            public int Status { get; set; }
        }
    }
}