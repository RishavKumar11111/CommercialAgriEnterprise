using System.ComponentModel.DataAnnotations;

namespace CommercialAgriEnterprise.Models
{
    [MetadataType(typeof(BLODetailEntryMetaData))]
    public partial class BLODetailEntry
    {
        internal sealed class BLODetailEntryMetaData
        {
            [Required]
            [RegularExpression(@"^[a-zA-Z\s]+$")]
            public string Name { get; set; }
            [Required]
            [RegularExpression(@"^[6-9]\d{9}$")]
            public string MobileNo { get; set; }
            [DataType(DataType.EmailAddress)]
            public string EmailID { get; set; }
            [Required]
            public byte[] Signature { get; set; }
        }
    }
}