using System.ComponentModel.DataAnnotations;

namespace CommercialAgriEnterprise.Models
{
    [MetadataType(typeof(BLOBlockMappingMetaData))]
    public partial class BLOBlockMapping
    {
        internal sealed class BLOBlockMappingMetaData
        {
            [Required]
            [RegularExpression(@"^[a-z]{3}_\d{4}$")]
            public string UserID { get; set; }
        }
    }
}