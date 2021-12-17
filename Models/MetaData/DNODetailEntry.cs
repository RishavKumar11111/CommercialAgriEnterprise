using System.ComponentModel.DataAnnotations;

namespace CommercialAgriEnterprise.Models
{
    [MetadataType(typeof(DNODetailEntryMetaData))]
    public partial class DNODetailEntry
    {
        internal sealed class DNODetailEntryMetaData
        {
            [Required]
            [RegularExpression(@"^[a-zA-Z\s]+$")]
            public string Name { get; set; }
            [Required]
            [RegularExpression(@"^[6-9]\d{9}$")]
            public string MobileNo { get; set; }
            [Required]
            public byte[] Signature { get; set; }
        }
    }
}